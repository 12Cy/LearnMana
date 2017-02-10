using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnMonoGame.Summoneds.Enemies.Elements;

namespace LearnMonoGame.Spells
{

    public struct SpellInformation
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

    public struct BulletInformation
    {
        public Point size;
        public float speed;
        public int range;
        public float lifetime; //Seconds
        public float triggerTime;
        public SAbility attackInformation;

        public BulletInformation(SAbility _modifikator = new SAbility(), float _speed = 350, Point _size = new Point(), int _range = 500, float _lifetime = 5, float _triggerTime = 0)
        {
            attackInformation = _modifikator;
            triggerTime = _triggerTime;
            range = _range;
            speed = _speed;
            size = _size;
            lifetime = _lifetime;
        }

    }

    public class SpellManager
    {
        public Dictionary<string, BulletInformation> bulletInformation = new Dictionary<string, BulletInformation>();
        public Dictionary<string, SAbility> attackInformation = new Dictionary<string, SAbility>();
        public Dictionary<string, SpellInformation> spellInformation = new Dictionary<string, SpellInformation>();
        public Random rnd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));

        public void LoadInformation()
        {
            /*
            bulletInformation.Add(EBullet.FireBall, new BulletInformation(_speed: 350, _size: new Point(12, 12), _range: 700));
            bulletInformation.Add(EBullet.FireWall, new BulletInformation(_speed: 100, _size: new Point(20, 20), _range: 300, _triggerTime: 0.2f));
            bulletInformation.Add(EBullet.FireBurn, new BulletInformation(_speed: 0, _size: new Point(20, 20), _range: 300));

            bulletInformation.Add(EBullet.IceLance, new BulletInformation(_speed: 500, _size: new Point(16, 19), _range: 800));
            bulletInformation.Add(EBullet.IceTornado, new BulletInformation(_speed: 30, _size: new Point(32, 35), _range: 800, _triggerTime: 0.5f));

            
            //Duration ist die BUFF dauer! / Delay dauert der Animation der Texture -> Danach passiert der Effekt! 
            attackInformation.Add(EBullet.FireBall, new SAbility(EMoveType.Attack, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: new[] { 6, 10 }, _crit: new[] { 2f, 3f }, _critChance: 15));
            attackInformation.Add(EBullet.FireWall, new SAbility(EMoveType.Effect, EStatus.Normal, new Elements(_fire: 50), _name: "Feuerball", _damage: new[] { 2, 3 }, _duration: 5));
            attackInformation.Add(EBullet.FireBurn, new SAbility(EMoveType.Effect, EStatus.Normal, new Elements(_fire: 50), _name: "FireBurn", _damage: new[] { 2, 3 }, _duration: 2, _delay: 1, _trigger: 0.1f, _crit: new[] { 2f, 3f }, _critChance: 10));

            attackInformation.Add(EBullet.IceFreeze, new SAbility(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 80), _name: "IceFreeze", _duration: 4, _speed: new[] { -40, -60 }, _attackSpeed: new[] { 6, 10 }));
            attackInformation.Add(EBullet.IceLance, new SAbility(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 50), _name: "IceLance", _damage: new[] { 6, 10 }));
            attackInformation.Add(EBullet.IceTornado, new SAbility(EMoveType.Effect, EStatus.Normal, new Elements(_ice: 70), _name: "IceTornado",
            _damage: new[] { 1, 1 }, _speed: new[] { -50, -70 }, _duration: 1, _trigger: 0.2f, _effectArea: new Rectangle(0, 0, 32, 32)));

            attackInformation.Add(EBullet.HolyLight, new SAbility(EMoveType.Heal, EStatus.Normal, new Elements(_light: 100), _name: "HolyLight", _health: new[] { 10, 50 }, _delay: 2f));
            */

            ParseBulletInformation("Assets/Bullet_Information.txt");
            ParseAttackInformation("Assets/Attack_Information.txt");
            ParseSpellInformation("Assets/Spell_Information.txt");

            /*
            //Spellinformation timer = Abklingzeit
            spellInformation.Add(ESpell.SFireBall, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SFireWall, new SpellInformation(10, 1,1f));
            spellInformation.Add(ESpell.SFireBurn, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SFireInferno, new SpellInformation(1, 3, 2, 0.1f));


            spellInformation.Add(ESpell.SIceLance, new SpellInformation(10, 0.3f));
            spellInformation.Add(ESpell.SIceTornado, new SpellInformation(10, 1));
            spellInformation.Add(ESpell.SIceFreeze, new SpellInformation(10, 2));


            spellInformation.Add(ESpell.SHolyLight, new SpellInformation(10, 1));
            */
        }


        void ParseBulletInformation(string filePath)
        {
            string[] str = File.ReadAllLines(filePath);

            string name = "null";
            Point size = new Point(12, 12);
            float speed = 350;
            int range = 500;
            float lifetime = 5;
            float triggerTime = 0;

            for (int line = 0; line < str.Length; ++line)
            {
                str[line] = str[line].Trim();
                //Zeichen bzw. Zeilen die übersprungen werden sollen. Beispiel KommentarZeilen [Feuer]
                if (str[line].Length == 0 || str[line][0] == '[')
                    continue;

                if (str[line][0] == '-')
                {
                    Console.WriteLine("Create BulletInformation" + name);
                    bulletInformation.Add(name, new BulletInformation(new SAbility(), speed, size, range, lifetime, triggerTime));
                    name = "null";
                    size = new Point(12, 12);
                    speed = 350;
                    range = 500;
                    lifetime = 5;
                    triggerTime = 0;
                    continue;
                }

                string[] split = str[line].Split(':');

                if (split.Length < 2)
                {
                    Console.WriteLine("SplitString Längee kleiner als 2");
                    continue;
                }

                string[] aryValues = split[1].Split(';');

                for (int i = 0; i < aryValues.Length; ++i)
                    aryValues[i] = aryValues[i].Trim();
                try
                {
                    switch (split[0].Trim())
                    {
                        case "name":
                            name = split[1].Trim();
                            break;
                        case "speed":
                            speed = int.Parse(split[1].Trim());
                            break;
                        case "range":
                            range = int.Parse(split[1].Trim());
                            break;
                        case "size":
                            size = new Point(int.Parse(aryValues[0]), int.Parse(aryValues[1]));
                            break;
                        case "triggerTime":
                            triggerTime = float.Parse(split[1].Trim());
                            break;
                        case "lifeTime":
                            lifetime = float.Parse(split[1].Trim());
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FormatException On " + str[line]);
                }
            }
        }
        void ParseAttackInformation(string filePath)
        {
            string[] str = File.ReadAllLines(filePath);

            string name = "null";
            EMoveType moveType = EMoveType.Attack;
            EStatus status = EStatus.Normal;
            Elements elements = new Elements();
            int duration = 0; //Wenn duration = 0 -> Ein SPontanzauber | 4 -> 4 sekunden (BUFF)
            float trigger = 0;//Wenn trigger = 0 -> Der Zauber triggert nichts | gibt an wie oft
            int[] damage = { 0, 0 }; //Schaden vom Zauber, sowohl bei Eintritt als auch bei einem Effekt
            int[] defense = { 0, 0 };
            int[] attackDamage = { 0, 0 };
            int[] attackSpeed = { 0, 0 };
            int[] speed = { 0, 0 };
            int[] health = { 0, 0 };
            int[] mana = { 0, 0 };
            float[] crit = { 1f, 1f }; //Der Schadensmulitplikator des kritischen Schaden. Der Faktor in Crit wird mit dem Schaden Mulitpliziert. Schaden *= Crit;
            int critChance = 0;
            bool isAlive = true;
            Rectangle effectArea = new Rectangle(0, 0, 0, 0);
            float delay = 0;
            int spellChance = 0;
            string spell = "Null";

            for (int line = 0; line < str.Length; ++line)
            {
                str[line] = str[line].Trim();
                //Zeichen bzw. Zeilen die übersprungen werden sollen. Beispiel KommentarZeilen [Feuer]
                if (str[line].Length == 0 || str[line][0] == '[')
                    continue;

                if (str[line][0] == '-')
                {
                    Console.WriteLine("Create SAbility" + name);
                    attackInformation.Add(name, new SAbility(moveType, status, elements, name, duration, damage, defense, attackDamage, attackSpeed, speed, health,
                        mana, crit, critChance, trigger, isAlive, delay, effectArea,spellChance,spell));

                    name = "null";
                    moveType = EMoveType.Attack;
                    status = EStatus.Normal;
                    elements = new Elements();
                    duration = 0; //Wenn duration = 0 -> Ein SPontanzauber | 4 -> 4 sekunden (BUFF)
                    trigger = 0;//Wenn trigger = 0 -> Der Zauber triggert nichts | gibt an wie oft
                    damage = new[] { 0, 0 }; //Schaden vom Zauber, sowohl bei Eintritt als auch bei einem Effekt
                    defense = new[] { 0, 0 };
                    attackDamage = new[] { 0, 0 };
                    attackSpeed = new[] { 0, 0 };
                    speed = new[] { 0, 0 };
                    health = new[] { 0, 0 };
                    mana = new[] { 0, 0 };
                    crit = new[] { 1f, 1f }; //Der Schadensmulitplikator des kritischen Schaden. Der Faktor in Crit wird mit dem Schaden Mulitpliziert. Schaden *= Crit;
                    critChance = 0;
                    spellChance = 0;
                    spell = "Null";
                    isAlive = true;
                    effectArea = new Rectangle(0, 0, 0, 0);
                    delay = 0;
                    continue;
                }

                string[] split = str[line].Split(':');

                if (split.Length < 2)
                {
                    Console.WriteLine("SplitString Längee kleiner als 2");
                    continue;
                }

                string[] aryValues = split[1].Split(';');

                for (int i = 0; i < aryValues.Length; ++i)
                {
                    aryValues[i] = aryValues[i].Trim();
                }

                try
                {

                    switch (split[0].Trim())
                    {
                        case "spell":
                            spell = aryValues[0];
                            break;
                        case "spellChance":
                            spellChance = int.Parse(aryValues[0]);
                            break;
                        case "name":
                            name = aryValues[0];
                            break;
                        case "trigger":
                            trigger = float.Parse(aryValues[0]);
                            break;
                        case "effectArea":
                            effectArea = new Rectangle(0, 0, int.Parse(aryValues[0]), int.Parse(aryValues[1]));
                            break;
                        case "status":
                            switch (aryValues[0])
                            {
                                case "attack":
                                    status = EStatus.Normal;
                                    break;
                                case "sleep":
                                    status = EStatus.Sleep;
                                    break;
                                case "paralysis":
                                    status = EStatus.Paralysis;
                                    break;
                            }
                            break;
                        case "moveType":
                            switch (aryValues[0])
                            {
                                case "attack":
                                    moveType = EMoveType.Attack;
                                    break;
                                case "heal":
                                    moveType = EMoveType.Heal;
                                    break;
                                case "effect":
                                    moveType = EMoveType.Effect;
                                    break;
                                case "status":
                                    moveType = EMoveType.Status;
                                    break;
                            }
                            break;
                        case "delay":
                            delay = float.Parse(aryValues[0]);
                            break;
                        case "duration":
                            duration = int.Parse(aryValues[0]);
                            break;
                        case "critChance":
                            critChance = int.Parse(aryValues[0]);
                            break;
                        case "mana":
                            mana = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "damage":
                            damage = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "speed":
                            speed = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "attackDamage":
                            attackDamage = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "attackSpeed":
                            attackSpeed = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "health":
                            health = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "defense":
                            defense = new[] { int.Parse(aryValues[0]), int.Parse(aryValues[1]) };
                            break;
                        case "crit":
                            crit = new[] { float.Parse(aryValues[0]), float.Parse(aryValues[1]) };
                            break;
                        case "element":
                            elements = new Elements(int.Parse(aryValues[0]), int.Parse(aryValues[1]), int.Parse(aryValues[2]), int.Parse(aryValues[3]), int.Parse(aryValues[4]), int.Parse(aryValues[5]), int.Parse(aryValues[6]));
                            break;
                        default:
                            Console.WriteLine("Keinen Match Gefunden bei Wort " + split[0].Trim());
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FormatException On " + str[line]);
                }
            }
        }
        void ParseSpellInformation(string filePath)
        {
            string[] str = File.ReadAllLines(filePath);

            string name = "null";
            int mana = 0;
            float cooldown = 0;
            float channelTime = 0;
            float triggerTime = 0;

            for (int line = 0; line < str.Length; ++line)
            {
                str[line] = str[line].Trim();
                //Zeichen bzw. Zeilen die übersprungen werden sollen. Beispiel KommentarZeilen [Feuer]
                if (str[line].Length == 0 || str[line][0] == '[')
                    continue;

                if (str[line][0] == '-')
                {
                    Console.WriteLine("Create SpellInformation " + name);
                    spellInformation.Add(name, new SpellInformation(mana, cooldown, channelTime, triggerTime));
                    name = "null";
                    mana = 0;
                    cooldown = 0;
                    channelTime = 0;
                    triggerTime = 0;
                    continue;
                }

                string[] split = str[line].Split(':');

                if (split.Length < 2)
                {
                    Console.WriteLine("SplitString Längee kleiner als 2");
                    continue;
                }
                try
                {
                    switch (split[0].Trim())
                    {
                        case "name":
                            name = split[1].Trim();
                            break;
                        case "mana":
                            mana = int.Parse(split[1].Trim());
                            break;
                        case "cooldown":
                            cooldown = float.Parse(split[1].Trim());
                            break;
                        case "channelTime":
                            channelTime = float.Parse(split[1].Trim());
                            break;
                        case "triggerTime":
                            triggerTime = float.Parse(split[1].Trim());
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FormatException On " + str[line]);
                }
            }
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
