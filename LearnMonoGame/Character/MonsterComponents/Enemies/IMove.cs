using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    public enum EMoveType 
    {
        Attack, Heal, Effect, Status
    }

    public enum EStatus //welchen Status habe ich nach meinem Move
    {
        Normal, Sleep, Paralysis
    }

    public struct Elements
    {
        int dark;
        int earth;
        int light;
        int fire;
        int water;
        int nature;
        int ice;

        public Elements(int _dark = 0, int _earth = 0, int _light = 0, int _fire = 0, int _water = 0, int _nature = 0, int _ice = 0)
        {
            dark = _dark;
            earth = _earth;
            light = _light;
            water = _water;
            nature = _nature;
            fire = _fire;
            ice = _ice;
        }
    }


    public struct IMove
    {
        public EMoveType moveType;
        public EStatus status;
        public Elements elements;
        public string name;
        public int duration; //Wenn duration = 0 -> Ein SPontanzauber | 4 -> 4 sekunden (BUFF)
        public float trigger;//Wenn trigger = 0 -> Der Zauber triggert nichts | gibt an wie oft
        public int damage;
        public int defense;
        public int attackDamage;
        public float attackSpeed;
        public int speed;
        public int health;
        public int mana;
        public bool isAlive;
		public Rectangle effectArea;
        public float delay;
        TimeSpan timeSpan;



        public IMove(EMoveType _moveType,EStatus _status, Elements _elements = new Elements(), string _name = "null", int _duration = 0, 
            int _damage = 0, int _defense = 0,int _attackDamage = 0, float _attackSpeed = 0, int _speed = 0, int _health = 0, int _mana = 0, float _trigger = 0, bool _isAlive = true, TimeSpan _timeSpan = new TimeSpan(),
            float _delay = 0,Rectangle _effectArea = new Rectangle())
        {
            effectArea = _effectArea;
            timeSpan = _timeSpan;
            isAlive = _isAlive;
            trigger = _trigger;
            moveType = _moveType;
            status = _status;
            elements = _elements;
            name = _name;
            duration = _duration;
            damage = _damage;
            attackSpeed = _attackSpeed;
            attackDamage = _attackDamage;
            defense = _defense;
            speed = _speed;
            health = _health;
            mana = _mana;
            delay = _delay;
        }
        public void Update(GameTime gameTime)
        {

            timeSpan += gameTime.ElapsedGameTime;

            if (timeSpan > TimeSpan.FromSeconds(1))
            {
                if (duration == 0)
                    isAlive = false;
                Console.WriteLine(duration + "hihI");
                duration--;
                Console.WriteLine(duration + "hihI");
            }
        }
        public void SetDelay(GameTime gameTime)
        {
            Console.WriteLine(delay);
            delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (delay < 0)
                delay = 0;

        }
    }
}
