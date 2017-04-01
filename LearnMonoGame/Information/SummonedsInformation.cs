using LearnMonoGame.Tools.Logger;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    class SummonedsInformation
    {

        public Dictionary<string, Attributes> characterInformation = new Dictionary<string, Attributes>();

        public void LoadInformation()
        {
            ParseEnemyInformation("Assets/Character_Information.txt");

        }

        void ParseEnemyInformation(string filePath)
        {
            characterInformation.Clear();
            string[] str = File.ReadAllLines(filePath);

            string name = "null";
            float speed = 180;
            int maxHealth = 100;
            float attackSpeed = 0.3f;
            Point size = new Point(64, 64);
            float defense = 1;
            float maxMana = 100;
            int attackRange = 0;

            for (int line = 0; line < str.Length; ++line)
            {
                str[line] = str[line].Trim();
                //Zeichen bzw. Zeilen die übersprungen werden sollen. Beispiel KommentarZeilen [Feuer]
                if (str[line].Length == 0 || str[line][0] == '[')
                    continue;

                if (str[line][0] == '-')
                {

                    LogHelper.Instance.Log(Logtarget.ParserLog, "Create CharacterInformations " + name);
                    characterInformation.Add(name, new Attributes(speed, maxHealth, attackSpeed, defense, maxMana, size, _attackRange: attackRange));
                    name = "null";
                    speed = 180;
                    maxHealth = 100;
                    attackSpeed = 0.3f;
                    size = new Point(64, 64);
                    defense = 1;
                    maxMana = 100;
                    attackRange = 0;
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
                            name = aryValues[0];
                            break;
                        case "speed":
                            speed = float.Parse(aryValues[0]);
                            break;
                        case "maxHealth":
                            maxHealth = int.Parse(aryValues[0]);
                            break;
                        case "attackSpeed":
                            attackSpeed = float.Parse(aryValues[0]);
                            break;
                        case "defense":
                            defense = float.Parse(aryValues[0]);
                            break;
                        case "maxMana":
                            maxMana = float.Parse(aryValues[0]);
                            break;
                        case "size":
                            size = new Point(int.Parse(aryValues[0]), int.Parse(aryValues[1]));
                            break;
                        case "attackRange":
                            attackRange = int.Parse(aryValues[0]);
                            break;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("FormatException On " + str[line]);
                }
            }
        }


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
