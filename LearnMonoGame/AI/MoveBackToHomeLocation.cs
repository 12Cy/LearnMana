using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;

namespace LearnMonoGame.AI
{
    class MoveBackToHomeLocation : AIScript
    {
        Vector2 homeLocation;


        public MoveBackToHomeLocation(EAlignment alignment, Vector2 _home) : base(alignment)
        {
            homeLocation = _home;
        }

        public override bool DoScript(GameTime gTime, Character c)
        {
            if ((c.Pos - homeLocation).Length() < 2f)
            {

                return true;
            }

            c.PosDestination = homeLocation;
            return false;
            
        }
    }
}
