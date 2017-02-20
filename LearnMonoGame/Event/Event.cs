using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Event
{
    /// <summary>
    /// Dialoge, Cutscenes, etc.
    /// </summary>
    public abstract class Event
    {
        public abstract bool Update(GameTime gTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
