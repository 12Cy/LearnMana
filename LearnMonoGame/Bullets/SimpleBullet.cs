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
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Bullets
{
    class SimpleBullet : Bullet
    {
        public SimpleBullet(Rectangle _startPosition, Vector2 _direction, Texture2D texture, string type, EAlignment  alignment) : 
            base(SpellManager.Instance.bulletInformation[type], _startPosition, _direction, texture,SpellManager.Instance.attackInformation[type], alignment) //Rectangel damit wir gleich die texture skaliert haben
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
