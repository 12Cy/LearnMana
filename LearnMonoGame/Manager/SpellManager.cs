using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnMonoGame.Summoneds.Enemies.Elements;

namespace LearnMonoGame.Spells
{

    struct SpellInformation
    {
        public int mana;
        public float cooldown;
        public float channelTime;
        public float triggerTime;

        public SpellInformation(int _mana = 0, float _time = 0, float _channelTime = 0, float _triggerTime = 0) // mana Abkingzeit, ChannelTime?
        {
            triggerTime = _triggerTime;
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
        SFireBall, //Simple
        SFireWall, //Eine FeuerWand, die an einem Fleck stehen bleibt und Schaden im Bereich zufügt
        SFireBurn,  //Feuer Debuff, der Schaden über Zeit hinzufügt
        SIceLance,
        SIceTornado,
        SDarkImpact,
        SHolyLight,
        SFireInferno,
        SIceFreeze
    }

    enum EBullet
    {
        FireBall,
        FireWall,
        FireBurn,
        IceLance,
        IceTornado,
        DarkImpact,
        HolyLight,
        IceFreeze
    }

    class SpellManager
    {
        public Dictionary<EBullet, BulletInformation> bulletInformation = new Dictionary<EBullet, Spells.BulletInformation>();
        public Dictionary<EBullet, IMove> attackInformation = new Dictionary<EBullet, IMove>();
        public Dictionary<ESpell, SpellInformation> spellInformation = new Dictionary<ESpell, SpellInformation>();
        public Random rnd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

        public void LoadInformation()
        {
            bulletInformation.Add(EBullet.FireBall, new BulletInformation(_speed: 350, _size: new Point(12, 12), _range: 700));
            bulletInformation.Add(EBullet.FireWall, new BulletInformation(_speed: 100, _size: new Point(20, 20), _range: 300, _triggerTime: 0.2f));
            bulletInformation.Add(EBullet.FireBurn, new BulletInformation(_speed: 0, _size: new Point(20, 20), _range: 300));

            bulletInformation.Add(EBullet.IceLance, new BulletInformation(_speed: 500, _size: new Point(16, 19), _range: 800));
            bulletInformation.Add(EBullet.IceTornado, new BulletInformation(_speed: 30, _size: new Point(32, 35), _range: 800, _triggerTime: 0.5f));

            //Duration ist die BUFF dauer! / Delay dauert der Animation der Texture -> Danach passiert der Effekt! 
            attackInformation.Add(EBullet.FireBall, new IMove(EMoveType.Attack, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: new[]{ 6, 10 }, _crit: new[] { 2f, 3f }, _critChance: 15));
            attackInformation.Add(EBullet.FireWall, new IMove(EMoveType.Effect, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: new[] { 2, 3 }, _duration: 5));
            attackInformation.Add(EBullet.FireBurn, new IMove(EMoveType.Effect, EStatus.Normal, new Elements(_fire: 50), _name: "FireBurn", _damage: new[] { 2, 3 }, _duration: 2, _delay: 1, _trigger: 0.1f, _crit: new[] { 2f, 3f }, _critChance: 10));

            attackInformation.Add(EBullet.IceFreeze, new IMove(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 80), _name: "IceFreeze",  _duration: 4, _speed: new[] { -40, -60 }, _attackSpeed: new[] { 6, 10 }));
            attackInformation.Add(EBullet.IceLance, new IMove(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 50), _name: "IceLance", _damage: new[] { 6, 10 }));
            attackInformation.Add(EBullet.IceTornado, new IMove(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 70), _name: "IceTornado",
    _damage: new[] { 1, 1 }, _speed: new[] { -50, -70 }, _duration: 1, _trigger: 0.2f, _effectArea: new Rectangle(0, 0, 32, 32)));

            attackInformation.Add(EBullet.HolyLight, new IMove(EMoveType.Heal, EStatus.Normal, new Elements(_light: 100), _name: "HolyLight", _health: new[] { 10, 50 }, _delay: 2f));



            //Spellinformation timer = Abklingzeit
            spellInformation.Add(ESpell.SFireBall, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SFireWall, new SpellInformation(10, 1,1f));
            spellInformation.Add(ESpell.SFireBurn, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SFireInferno, new SpellInformation(1, 3, 2, 0.1f));


            spellInformation.Add(ESpell.SIceLance, new SpellInformation(10, 0.3f));
            spellInformation.Add(ESpell.SIceTornado, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SIceFreeze, new SpellInformation(10, 2));


            spellInformation.Add(ESpell.SHolyLight, new SpellInformation(10, 1));
            
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
