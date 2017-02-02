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


    public struct SAbility
    {
        public EMoveType moveType;
        public EStatus status;
        public Elements elements;
        public string name;
        public int duration; //Wenn duration = 0 -> Ein SPontanzauber | 4 -> 4 sekunden (BUFF)
        public float trigger;//Wenn trigger = 0 -> Der Zauber triggert nichts | gibt an wie oft
        public int[] damage; //Schaden vom Zauber, sowohl bei Eintritt als auch bei einem Effekt
        public int[] defense; 
        public int[] attackDamage;
        public int[] attackSpeed;
        public int[] speed;
        public int[] health;
        public int[] mana;
        public float[] crit; //Der Schadensmulitplikator des kritischen Schaden. Der Faktor in Crit wird mit dem Schaden Mulitpliziert. Schaden *= Crit;
        public int critChance;
        public bool isAlive;
        public Rectangle effectArea;
        public float delay;
        TimeSpan timeSpan;

        public SAbility(EMoveType _moveType, EStatus _status, Elements _elements = new Elements(), string _name = "null", int _duration = 0,
            int[] _damage = null, int[] _defense = null, int[] _attackDamage = null, int[] _attackSpeed = null, int[] _speed = null, int[] _health = null, int[] _mana = null,
            float[] _crit = null, int _critChance = 0,
            float _trigger = 0, bool _isAlive = true, TimeSpan _timeSpan = new TimeSpan(),
            float _delay = 0, Rectangle _effectArea = new Rectangle())
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
            delay = _delay;
            critChance = _critChance;

            if (_crit == null)
                crit = new[] { 1f, 1f };
            else
                crit = _crit;

            if (_damage == null)
                damage = new[] { 0, 0 };
            else
                damage = _damage;

            if (_attackDamage == null)
                attackDamage = new[] { 0, 0 };
            else
                attackDamage = _attackDamage;

            if (_attackSpeed == null)
                attackSpeed = new[] { 0, 0 };
            else
                attackSpeed = _attackSpeed;

            if (_defense == null)
                defense = new[] { 0, 0 };
            else
                defense = _defense;

            if (_speed == null)
                speed = new[] { 0, 0 };
            else
                speed = _speed;

            if (_health == null)
                health = new[] { 0, 0 };
            else
                health = _health;

            if (_mana == null)
                mana = new[] { 0, 0 };
            else
                mana = _mana;

        }
        public void Update(GameTime gameTime)
        {

            timeSpan += gameTime.ElapsedGameTime;

            if (timeSpan > TimeSpan.FromSeconds(1))
            {
                if (duration == 0)
                    isAlive = false;
                duration--;

            }
        }
        public void SetDelay(GameTime gameTime)
        {
            delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (delay < 0)
                delay = 0;

        }
    }
}
