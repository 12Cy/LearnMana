using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.PlayerComponents
{
    class PlayerInformation : Attributes
    {
        public float maxMana;
        public PlayerInformation()
        {
            speed = 180;
            maxHealth = 100;
            attackSpeed = 0.3f;
            damage = 1;
            defense = 1;
            maxMana = 100;

        }

    }
    class PlayerManager
    {

        PlayerInformation playerInformation = new PlayerInformation();
        Player player;
        
        public PlayerInformation PlayerInformation { get { return PlayerInformation; } }
        public Player Player{get{return Player;} set { player = value; } }



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
