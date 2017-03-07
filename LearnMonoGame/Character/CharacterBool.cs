using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public class CharacterBool
    {
        public CharacterBool()
        {
            isRunning = false;
            isAlive = true;
            hit = false;
            isSelected = false;
            sleep = false;
            paralyse = false;
        }

        public bool paralyse;
        public bool sleep;
        public bool isRunning;
        public bool isAlive;
        public bool hit;
        public bool isSelected;
    }
}
