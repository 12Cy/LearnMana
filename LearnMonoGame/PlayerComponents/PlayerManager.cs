using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.PlayerComponents
{
    class PlayerManager
    {
        public List<Summoned> mySummoned = new List<Summoned>();



        private PlayerManager()
        {

        }
        static PlayerManager instance;
        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlayerManager();

                return instance;
            }
        }
    }
}
