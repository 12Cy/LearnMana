using LearnMonoGame.Summoneds;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.AI
{
    public abstract class AIScript
    {
        protected EAlignment alignment;

        public AIScript(EAlignment alignment)
        {
            this.alignment = alignment;
        }

        public abstract bool DoScript(GameTime gTime, Character c);
    }
}
