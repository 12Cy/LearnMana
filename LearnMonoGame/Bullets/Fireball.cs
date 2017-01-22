using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Spells;

namespace LearnMonoGame.Bullets
{
    class Fireball : Bullet
    {
        public Fireball(Rectangle _startPosition, Vector2 _direction) : base(_startPosition, _direction) //Rectangel damit wir gleich die texture skaliert haben
        {
            fireballTexture = _CM.GetTexture(_CM.TextureName.fireball);
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
