using LearnMonoGame.Components;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LearnMonoGame.Summoneds
{
    class MonsterManager
    {

        public List<Character> enemyList = new List<Character>();
        public List<Character> mySummoned = new List<Character>();
        public List<Character> selectedList = new List<Character>();

        static MonsterManager instance;

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

            //selectedList[0].PosDestination = mouse;
            //if (selectedList.Count == 2)
            //{
            //    for (int i = 1; i < selectedList.Count; i++)
            //    {
            //        int help = random.Next(1, 3);
            //        if (help != 1)
            //            help = -1;
            //        int help2 = random.Next(1, 3);
            //        if (help2 != 1)
            //            help2 = -1;
            //
            //        int nextValueX = random.Next(40, 64);
            //        int nextValueY = random.Next(40, 64);
            //
            //
            //
            //        mouse.X +=  help *  nextValueX;
            //        mouse.Y +=  help2 * nextValueY;
            //        selectedList[i].PosDestination = mouse;
            //        
            //    }
            //}
            //else if (selectedList.Count == 3)
            //{
            //    Console.WriteLine("3");
            //}



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
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.Bounds))
                {
                    return PlayerManager.Instance.MyPlayer;
                }

                foreach (Character c in mySummoned)
                {
                    if (c.Bounds.Intersects(bounds))
                    {
                        return c;
                    }
                }
            }
            else if(alignment == EAlignment.Player)
            {
                foreach(Character c in enemyList)
                {
                    if (c.Bounds.Intersects(bounds))
                        return c;
                }
            }
            else
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.Bounds))
                {
                    return PlayerManager.Instance.MyPlayer;
                }

                foreach (Character c in Instance.mySummoned)
                {
                    if (c.Bounds.Intersects(bounds))
                    {
                        return c;
                    }
                }

                foreach (Character c in enemyList)
                {
                    if (c.Bounds.Intersects(bounds))
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
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.Bounds))
                {
                    cList.Add(PlayerManager.Instance.MyPlayer);
                }

                foreach (Character c in mySummoned)
                {
                    if (c.Bounds.Intersects(bounds))
                    {
                        cList.Add(c);
                    }
                }
            }
            else if (alignment == EAlignment.Player)
            {
                foreach (Character c in enemyList)
                {
                    if (c.Bounds.Intersects(bounds))
                        cList.Add(c);
                }
            }
            else
            {
                if (bounds.Intersects(PlayerManager.Instance.MyPlayer.Bounds))
                {
                    cList.Add(PlayerManager.Instance.MyPlayer);
                }

                foreach (Character c in Instance.mySummoned)
                {
                    if (c.Bounds.Intersects(bounds))
                    {
                        cList.Add(c);
                    }
                }

                foreach (Character c in enemyList)
                {
                    if (c.Bounds.Intersects(bounds))
                        cList.Add(c);
                }
            }

            return cList;
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