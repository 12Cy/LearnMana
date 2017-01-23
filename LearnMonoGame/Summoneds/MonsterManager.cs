using LearnMonoGame.Summoneds.Enemies;
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


        static MonsterManager instance;
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