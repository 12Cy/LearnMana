using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Spells;
using LearnMonoGame.Manager;
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Bullets
{
    class FireWall : Bullet
    {
        public FireWall(Rectangle _startPosition, Vector2 _direction, Texture2D texture, string type,EAlignment alignment) :
            base(SpellManager.Instance.bulletInformation[type], _startPosition, _direction, texture, SpellManager.Instance.attackInformation[type], alignment)
        {
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
        }

        public override void OnExplode()
        {
            base.OnExplode();
        }

        public override void OnTrigger()
        {
            _BulletManager.Instance.bullets.Add(new SimpleBullet(new Rectangle(positon.ToPoint(), Point.Zero), direction, _CM.GetTexture(_CM.TextureName.fireball), "FireBurn", alignment));
            base.OnTrigger();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
