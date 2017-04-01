using LearnMonoGame.Components;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Summoneds.Enemies.Monster;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LearnMonoGame.Summoneds
{
    public class MonsterManager
    {

        public List<Character> enemyList = new List<Character>();
        public List<Character> mySummoned = new List<Character>();
        public List<Character> selectedList = new List<Character>();

        Queue<Character> spawnChar = new Queue<Character>();

        static MonsterManager instance;

        #region SpawnDespawn

        public void SpawnCharacterInQueue()
        {
            while(spawnChar.Count > 0)
            {
                enemyList.Add(spawnChar.Dequeue());
            }
        }

        public void SpawnCharacterAtMousePosition(string name)
        {
            if (!xIn.CheckMouseReleased(MouseButtons.Left))
                return;
            name = name.ToLower();
            Vector2 position = xIn.MousePosition;
            switch (name)
            {
                case "skelett":
                    spawnChar.Enqueue(new Skelett(position));
                    break;
                case "wolf":
                    spawnChar.Enqueue(new Wolf(position));
                    break;
                case "zombie":
                    spawnChar.Enqueue(new Zombie(position));
                    break;
                default:
                    //Console.WriteLine("Didnt Found Character (" + name + ")");
                    break;
            }
        }

        public void SpawnCharacter(string name, Vector2 position)
        {
            name = name.ToLower();
            switch (name)
            {
                case "skelett":
                    spawnChar.Enqueue(new Skelett(position));
                    break;
                case "wolf":
                    spawnChar.Enqueue(new Wolf(position));
                    break;
                case "zombie":
                    spawnChar.Enqueue(new Zombie(position));
                    break;
                default:
                    //Console.WriteLine("Didnt Found Character (" + name + ")");
                    break;
            }
        }

        public bool DestroyCharacter(string id)
        {
            id = id.ToUpper();
            for (int i = 0; i < enemyList.Count; ++i)
            {
                if (string.Compare(enemyList[i].GetID, id) == 0)
                {
                    enemyList.RemoveAt(i);
                    Console.WriteLine("Destroyed Character " + id);
                    return true;
                }
            }

            for (int i = 0; i < mySummoned.Count; ++i)
            {
                if (string.Compare(enemyList[i].GetID, id) == 0)
                {
                    enemyList.RemoveAt(i);
                    Console.WriteLine("Destroyed Character " + id);
                    return true;
                }
            }
            return false;
        }

        public void DestroyCharacterList(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "enemy":
                    enemyList.Clear();
                    break;
                case "mysummoned":
                    mySummoned.Clear();
                    break;
                case "selected":
                    foreach (Character c in selectedList)
                    {
                        DestroyCharacter(c.GetID);
                    }
                    break;
                default:
                    Console.WriteLine("Found no List/Character of type (" + type + ")");
                    return;
            }
        }

        public void PrintList(string type)
        {
            type = type.ToLower();
            List<Character> printList = null;
            switch (type)
            {
                case "enemy":
                    printList = enemyList;
                    break;
                case "mysummoned":
                    printList = mySummoned;
                    break;
                case "selected":
                    printList = selectedList;
                    break;
                default:
                    Console.WriteLine("Found no List of type (" + type + ")");
                    return;
            }

            foreach (Character c in printList)
            {
                Console.WriteLine(c.GetID + "  " + c.Pos);
            }

        }


        public string GetList(string type)
        {
            type = type.ToLower();
            List<Character> printList = null;
            string str = "";
            switch (type)
            {
                case "enemy":
                    printList = enemyList;
                    break;
                case "mysummoned":
                    printList = mySummoned;
                    break;
                case "selected":
                    printList = selectedList;
                    break;
                default:
                    str = "Found no List of type (" + type + ")";
                    break;
            }

            if (printList == null)
                return "";
            foreach (Character c in printList)
            {
                str += c.GetID + "  " + c.Pos + "\n";
            }

            return str;
        }

        #endregion

        public void GetDestination()
        {
            selectedList = selectedList.OrderBy(a => Guid.NewGuid()).ToList();
            Vector2 mouse = new Vector2(xIn.MousePosition.X, xIn.MousePosition.Y);
            Random random = new Random(Guid.NewGuid().GetHashCode());

            if (selectedList.Count == 1)
            {
                selectedList[0].PosDestination = mouse;
                return;
            }

            int count = (int)Math.Ceiling(Math.Sqrt(selectedList.Count));
            int[,] grid = new int[count, count];
            int k = count / 2;
            int i = 0;

            for (int y = 0; y < count; y++)
            {
                for (int x = 0; x < count; x++)
                {
                    int help = random.Next(1, 3);
                    if (help != 1)
                        help = -1;
                    int help2 = random.Next(1, 3);
                    if (help2 != 1)
                        help2 = -1;

                    int nextValueX = random.Next(5, 10);
                    int nextValueY = random.Next(5, 10);

                    if (i >= selectedList.Count)
                        break;
                    selectedList[i++].PosDestination = new Vector2(mouse.X + (x - k) * 74 + nextValueX * help, mouse.Y + (y - k) * 74 + nextValueY * help2);

                }
            }

        }

        /// <summary>
        /// Überprüft, ob das Rectangle einen Charakter trifft.
        /// Bei Alignment.Enemy wird der Player und seiner Summoners überprüft.
        /// Bei Alignment.Player werden alle Gegner überprüft
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="bounds"></param>
        /// <returns>Gibt Null zurück, wenn keine gefunden wurde.</returns>
        public Character CheckCollisionOne(EAlignment alignment, Rectangle bounds)
        {
            if (alignment == EAlignment.Enemy)
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.HitBox))
                {
                    return PlayerManager.Instance.MyPlayer;
                }

                foreach (Character c in mySummoned)
                {
                    if (c.HitBox.Intersects(bounds))
                    {
                        return c;
                    }
                }
            }
            else if (alignment == EAlignment.Player)
            {
                foreach (Character c in enemyList)
                {
                    if (c.HitBox.Intersects(bounds))
                        return c;
                }
            }
            else
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.HitBox))
                {
                    return PlayerManager.Instance.MyPlayer;
                }

                foreach (Character c in Instance.mySummoned)
                {
                    if (c.HitBox.Intersects(bounds))
                    {
                        return c;
                    }
                }

                foreach (Character c in enemyList)
                {
                    if (c.HitBox.Intersects(bounds))
                        return c;
                }
            }

            return null;
        }
        /// <summary>
        /// Gibt ein Array von allen Charakteren zurück, die mit dem Rectangle kollidieren.
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public List<Character> CheckCollisionAll(EAlignment alignment, Rectangle bounds)
        {
            List<Character> cList = new List<Character>();

            if (alignment == EAlignment.Enemy)
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.HitBox))
                {
                    cList.Add(PlayerManager.Instance.MyPlayer);
                }

                foreach (Character c in mySummoned)
                {
                    if (c.HitBox.Intersects(bounds))
                    {
                        cList.Add(c);
                    }
                }
            }
            else if (alignment == EAlignment.Player)
            {
                foreach (Character c in enemyList)
                {
                    if (c.HitBox.Intersects(bounds))
                        cList.Add(c);
                }
            }
            else
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.HitBox))
                {
                    cList.Add(PlayerManager.Instance.MyPlayer);
                }

                foreach (Character c in Instance.mySummoned)
                {
                    if (c.HitBox.Intersects(bounds))
                    {
                        cList.Add(c);
                    }
                }

                foreach (Character c in enemyList)
                {
                    if (c.HitBox.Intersects(bounds))
                        cList.Add(c);
                }
            }

            return cList;
        }

        public Character CheckNearestCharacter(EAlignment alignment, Rectangle bounds)
        {
            Character helpChar = null;
            float range = float.PositiveInfinity;
            Vector2 origin = bounds.Location.ToVector2() + bounds.Size.ToVector2() / 2;
            if (alignment == EAlignment.Enemy)
            {
                if ((PlayerManager.Instance.MyPlayer.HitBox.Location.ToVector2() - origin).Length() < range)
                {
                    helpChar = PlayerManager.Instance.MyPlayer;
                    range = (PlayerManager.Instance.MyPlayer.HitBox.Location.ToVector2() - origin).Length();
                }

                foreach (Character c in mySummoned)
                {
                    if ((c.HitBox.Location.ToVector2() - origin).Length() < range)
                    {
                        helpChar = c;
                        range = (c.HitBox.Location.ToVector2() - origin).Length();
                    }
                }
            }
            else if (alignment == EAlignment.Player)
            {
                foreach (Character c in enemyList)
                {
                    if ((c.HitBox.Location.ToVector2() - origin).Length() < range)
                    {
                        helpChar = c;
                        range = (c.HitBox.Location.ToVector2() - origin).Length();
                    }
                }
            }
            else
            {
                if ((PlayerManager.Instance.MyPlayer.HitBox.Location.ToVector2() - origin).Length() < range)
                {
                    helpChar = PlayerManager.Instance.MyPlayer;
                    range = (PlayerManager.Instance.MyPlayer.HitBox.Location.ToVector2() - origin).Length();
                }

                foreach (Character c in mySummoned)
                {
                    if ((c.HitBox.Location.ToVector2() - origin).Length() < range)
                    {
                        helpChar = c;
                        range = (c.HitBox.Location.ToVector2() - origin).Length();
                    }
                }

                foreach (Character c in enemyList)
                {
                    if ((c.HitBox.Location.ToVector2() - origin).Length() < range)
                    {
                        helpChar = c;
                        range = (c.HitBox.Location.ToVector2() - origin).Length();
                    }
                }
            }

            return helpChar;
        }

        public float DistanceBetweenTwoCharacters(Character c1, Character c2)
        {
            return (c2.HitBox.Location.ToVector2() - c1.HitBox.Location.ToVector2()).Length();
        }

        public static MonsterManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MonsterManager();

                return instance;
            }
        }

    }
}