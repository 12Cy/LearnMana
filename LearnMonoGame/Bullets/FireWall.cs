using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Spells;
using LearnMonoGame.Manager;

namespace LearnMonoGame.Bullets
{
    class FireWall : Bullet
    {
        public FireWall(Rectangle _startPosition, Vector2 _direction, Texture2D texture, EBullet type) :
            base(SpellManager.Instance.bulletInformation[type], _startPosition, _direction, texture, SpellManager.Instance.attackInformation[type])
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
            BulletManager.Instance.bullets.Add(new SimpleBullet(new Rectangle(positon.ToPoint(), Point.Zero), direction, _CM.GetTexture(_CM.TextureName.fireball), EBullet.FireBurn));
            base.OnTrigger();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
