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
    class SimpleBullet : Bullet
    {
        public SimpleBullet(Rectangle _startPosition, Vector2 _direction, Texture2D texture, EBullet type) : base(SpellManager.Instance.bulletInformation[(int)type],
            _startPosition, _direction, texture) //Rectangel damit wir gleich die texture skaliert haben
        {
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
