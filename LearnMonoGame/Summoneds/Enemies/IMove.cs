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

        public Elements(int _dark = 0, int _earth = 0, int _light = 0, int _fire = 0, int _water = 0, int _nature = 0)
        {
            dark = _dark;
            earth = _earth;
            light = _light;
            water = _water;
            nature = _nature;
            fire = _fire;
        }
    }


    public struct IMove
    {
        EMoveType moveType;
        EStatus status;
        Elements elements;
        string name;
        int duration;
        int damage;
        int defense;
        int speed;
        int health;
        int mana;

        public IMove(EMoveType _moveType,EStatus _status, Elements _elements = new Elements(), string _name = "null", int _duration = 0, 
            int _damage = 0, int _defense = 0, int _speed = 0, int _health = 0, int _mana = 0)
        {
            moveType = _moveType;
            status = _status;
            elements = _elements;
            name = _name;
            duration = _duration;
            damage = _damage;
            defense = _defense;
            speed = _speed;
            health = _health;
            mana = _mana;
        }
    }
}
