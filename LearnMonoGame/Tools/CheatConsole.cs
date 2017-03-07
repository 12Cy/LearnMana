using LearnMonoGame.Components;
using LearnMonoGame.Summoneds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools
{
    class CheatConsole
    {

        private enum EStatus
        {
            Idle,
            WaitInput,
            ParseString
        }

        EStatus status;

        public CheatConsole()
        {
            status = EStatus.WaitInput;
        }


        public void Update(GameTime gTime)
        {
            switch (status)
            {
                case EStatus.WaitInput:
                    if (xIn.CheckKeyReleased(Keys.F1))
                    {
                        Console.WriteLine("CheatConsole Activated!");
                        Console.WriteLine("Zum Beenden 'end' in die Console eingeben");
                        Console.WriteLine("");
                        status = EStatus.ParseString;

                    }
                    break;
                case EStatus.ParseString:
                    ParseConsole();
                    break;
            }

        }


        void ParseConsole()
        {
            string line = Console.ReadLine();

            if (line.Length > 0)
            {
                string[] aryLine = line.Split(':');


                for (int i = 0; i < aryLine.Length; ++i)
                {
                    aryLine[i] = aryLine[i].ToLower();
                    aryLine[i] = aryLine[i].Trim();
                }
                try
                {
                    switch (aryLine[0])
                    {
                        #region spawn
                        //spawn : Name : X,Y
                        //spawn : Skelett : 200,200
                        case "spawn":

                            if (aryLine.Length != 3)
                                throw new Exception("Wrong Format (" + line + ")");

                            string[] values = aryLine[2].Split(',');

                            for (int i = 0; i < values.Length; ++i)
                            {
                                values[i] = values[i].ToLower();
                                values[i] = values[i].Trim();
                            }

                            int x = 0;
                            int y = 0;

                            if (!int.TryParse(values[0], out x))
                            {
                                throw new Exception("Spawn Attempted conversion of " + values[0]);
                            }

                            if (!int.TryParse(values[1], out y))
                            {
                                throw new Exception("Spawn Attempted conversion of " + values[1]);
                            }

                            MonsterManager.Instance.SpawnCharacter(aryLine[1], new Vector2(x, y));

                            break;
                        #endregion

                        #region destroy
                            //kill : ID
                            //kill : SK1
                        case "kill":
                            if (aryLine.Length != 2)
                                throw new Exception("Wrong Format (" + line + ")");
                            if (!MonsterManager.Instance.DestroyCharacter(aryLine[1]))
                                MonsterManager.Instance.DestroyCharacterList(aryLine[1]);
                            break;
                        #endregion

                        #region list
                        //list : type
                        case "list":
                            if (aryLine.Length != 2)
                                throw new Exception("Wrong Format (" + line + ")");

                            MonsterManager.Instance.PrintList(aryLine[1]);
                            break;
                        #endregion
                        case "end":
                            status = EStatus.WaitInput;
                            break;
                        default:
                            throw new Exception("No Command found " + line);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Catched Exception " + e.Message);
                }
                Console.WriteLine("");





            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {



        }
    }
}
