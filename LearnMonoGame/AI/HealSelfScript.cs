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
    class HealSelfScript : AIScript
    {
        public HealSelfScript(EAlignment alignment) : base(alignment)
        {
        }

        public override bool DoScript(GameTime gTime, Character c)
        {
            if (c.attributes.CurrentHealth < c.attributes.MaxHealth)
            {
                c.spellBook.Cast(gTime, c.HitBox.Location.ToVector2(), c.HitBox.Location.ToVector2(), c);
            }

            return true;
        }
    }
}
