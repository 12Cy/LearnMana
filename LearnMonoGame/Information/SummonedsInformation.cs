using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
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
    class SkelettInformation : Attributes
    {
        public SkelettInformation()
        {
            speed = 90;
            maxHealth = 1000;

        }
    }

    class WolfInformation : Attributes
    {
        public WolfInformation()
        {
            speed = 110;

        }
    }
    class DummyInformation : Attributes
    {
        #region properties
        public DummyInformation()
        {
        }

        #endregion
    }
    class SummonedsInformation
    {
        public DummyInformation dummyInformation = new DummyInformation();
        public SkelettInformation skelettInformation = new SkelettInformation();
        public PlayerInformation playerInformation = new PlayerInformation();
        public WolfInformation wolfInformation = new WolfInformation();


        static SummonedsInformation instance;
        public static SummonedsInformation Instance
        {
            get
            {
                if (instance == null)
                    instance = new SummonedsInformation();

                return instance;
            }
        }
    }
}
