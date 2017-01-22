using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Spells
{

    struct SpellInformation
    {
        public float mana;
        public float time;

        public SpellInformation(float _mana = 0, float _time = 0)
        {
            mana = _mana;
            time = _time;
        }
    }

    struct BulletInformation
    {
        public Point size;
        public float speed;
        public int range;
        public float lifetime; //Seconds
        public float triggerTime;

        public BulletInformation(float _speed = 350, Point _size = new Point(), int _range = 500, int _lifetime = 5, float _triggerTime = 0)
        {
            triggerTime = _triggerTime;
            range = _range;
            speed = _speed;
            size = _size;
            lifetime = _lifetime;
        }

    }
    enum ESpell
    {
        SFireball, //Simple
        SFirewall, //Eine FeuerWand, die an einem Fleck stehen bleibt und Schaden im Bereich zufügt
        SFireburn  //Feuer Debuff, der Schaden über Zeit hinzufügt
    }

    enum EBullet
    {
        Fireball,
        Firewall,
        Fireburn
    }

    class SpellManager
    {
        public BulletInformation[] bulletInformation = 
        {
            new BulletInformation(_speed: 350, _size: new Point(12,12), _range: 700), //Fireball
            new BulletInformation(_speed: 100, _size: new Point(20,20), _range: 300, _triggerTime: 0.2f), //FireWall
            new BulletInformation(_speed: 0, _size: new Point(20,20), _range: 300, _triggerTime: 20f) //FireBurn
        };




        public SpellInformation[] spellInformation =
        {
            new SpellInformation(5,1), //SFireball
            new SpellInformation(20,2) //SFireWall
        };

        static SpellManager instance;
        public static SpellManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SpellManager();

                return instance;
            }
        }
    }


}
