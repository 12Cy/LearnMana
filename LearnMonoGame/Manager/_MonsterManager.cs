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
            Vector2 mouse = new Vector2(xIn.MousePosition.X, xIn.MousePosition.Y);
            Random random = new Random(Guid.NewGuid().GetHashCode());


            int count = (int)Math.Ceiling(Math.Sqrt(selectedList.Count));
            Console.WriteLine(count);
            int[,] grid = new int[count, count];
            int k = count / 2;
            Console.WriteLine(k);
            int i = 0;


                for (int y = 0; y < count; y++)
                {
                    for (int x = 0; x < count; x++)
                    {
                        if (i >= selectedList.Count)
                            break;
                        selectedList[i++].PosDestination = new Vector2(mouse.X + (x - k) * 64, mouse.Y + (y - k) * 64);

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