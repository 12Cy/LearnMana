using LearnMonoGame.Summoneds.Enemies;
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
        public int mana;
        public float cooldown;
        public float channelTime;

        public SpellInformation(int _mana = 0, float _time = 0, float _channelTime = 0)
        {
            mana = _mana;
            cooldown = _time;
            channelTime = _channelTime;
        }
    }

    struct BulletInformation
    {
        public Point size;
        public float speed;
        public int range;
        public float lifetime; //Seconds
        public float triggerTime;
        public IMove attackInformation;

        public BulletInformation(IMove _modifikator = new IMove(), float _speed = 350, Point _size = new Point(), int _range = 500, int _lifetime = 5, float _triggerTime = 0)
        {
            attackInformation = _modifikator;
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
        public Dictionary<EBullet, BulletInformation> bulletInformation = new Dictionary<EBullet, Spells.BulletInformation>();
        public Dictionary<EBullet, IMove> attackInformation = new Dictionary<EBullet, IMove>();
        public Dictionary<ESpell, SpellInformation> spellInformation = new Dictionary<ESpell, SpellInformation>();

        public void LoadInformation()
        {
            bulletInformation.Add(EBullet.Fireball, new BulletInformation(_speed: 350, _size: new Point(12, 12), _range: 700));
            bulletInformation.Add(EBullet.Firewall, new BulletInformation(_speed: 100, _size: new Point(20, 20), _range: 300, _triggerTime: 0.2f));
            bulletInformation.Add(EBullet.Fireburn, new BulletInformation(_speed: 0, _size: new Point(20, 20), _range: 300));

            attackInformation.Add(EBullet.Fireball, new IMove(EMoveType.Attack, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: 10));
            attackInformation.Add(EBullet.Firewall, new IMove(EMoveType.Status, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: 3, _duration: 5));
            attackInformation.Add(EBullet.Fireburn, new IMove(EMoveType.Status, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: 1, _duration: 3));

            spellInformation.Add(ESpell.SFireball, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SFirewall, new SpellInformation(10, 1,1));
            spellInformation.Add(ESpell.SFireburn, new SpellInformation(10, 1));
        }

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
