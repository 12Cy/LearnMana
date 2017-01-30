﻿using System;
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
            speed = 120;
            maxHealth = 100;
            attackSpeed = 0.3f;
            damage = 1;
            defense = 1;
            maxMana = 100;
            width = 64;
            height = 64;

        }
    }
    class SkelettInformation : Attributes
    {
        public SkelettInformation()
        {
            speed = 90;
            maxHealth = 100;
            width = 64;
            height = 64;

        }
    }

    class WolfInformation : Attributes
    {
        public WolfInformation()
        {
            speed = 90;

        }
    }
    class DummyInformation : Attributes
    {
        #region properties
        public DummyInformation()
        {
            speed = 120;
            width = 64;
            height = 64;
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
