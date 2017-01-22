using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    class Hot : IMove
    {
        private string name;
        private ETarget target;
        private EMoveType moveType;
        private EMoveElement moveElement;
        private EStatus status;
        private int duration;
        private int attack;
        private int defense;
        private int speed;
        private int health;

        #region Property Region

        public string Name
        {
            get { return name; }
        }

        public ETarget Target
        {
            get { return target; }
        }

        public EMoveType MoveType
        {
            get { return moveType; }
        }

        public EMoveElement MoveElement
        {
            get { return moveElement; }
        }

        public EStatus Status
        {
            get { return status; }
        }



        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public int Attack
        {
            get { return attack; }
        }

        public int Defense
        {
            get { return defense; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public int Health
        {
            get { return health; }
        }

        #endregion
        public Hot()
        {
            name = "Hot";
            target = ETarget.Self;
            moveType = EMoveType.Buff;
            moveElement = EMoveElement.None;
            status = EStatus.Normal;
            duration = 5;
            health = 7;

        }
        public object Clone()
        {
            Hot hot = new Hot();
            return hot;
        }
    }
}
