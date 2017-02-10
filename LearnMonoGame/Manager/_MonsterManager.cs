using LearnMonoGame.Components;
using LearnMonoGame.Summoneds.Enemies;
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
                    int help = random.Next(0, 2) * 2 - 1;
                    int help2 = random.Next(0, 2) * 2 - 1;

                    int nextValueX = random.Next(5, 10);
                    int nextValueY = random.Next(5, 10);

                    if (i >= selectedList.Count)
                        break;
                    selectedList[i++].PosDestination = new Vector2(mouse.X + (x - k) * 74 + nextValueX * help, mouse.Y + (y - k) * 74 + nextValueY * help2);

                }
            }

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