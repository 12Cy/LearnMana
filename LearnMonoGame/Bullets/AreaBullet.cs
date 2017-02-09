using LearnMonoGame.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Summoneds;
using LearnMonoGame.PlayerComponents;

namespace LearnMonoGame.Bullets
{
    class AreaBullet : Bullet
    {
        public AreaBullet( Rectangle _startPosition, Vector2 _direction, Texture2D _texture, string type) : base(SpellManager.Instance.bulletInformation[type], _startPosition, _direction, _texture, SpellManager.Instance.attackInformation[type])
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

        protected override void Collision()
        {
        }

        public override void OnTrigger()
        {
            Rectangle me = new Rectangle((int)positon.X - effect.effectArea.Width / 2,
                                         (int)positon.Y - effect.effectArea.Height / 2,
                                         effect.effectArea.Width, effect.effectArea.Height);

            foreach (Character c in MonsterManager.Instance.enemyList)
            {
                if(me.Intersects(c.Bounds))
                    c.ApplyEffect(effect);
           }

            foreach (Character c in MonsterManager.Instance.mySummoned)
            {
                if (me.Intersects(c.Bounds))
                    c.ApplyEffect(effect);
            }
            Character k = PlayerManager.Instance.MyPlayer;
                if (me.Intersects(k.Bounds))
                    k.ApplyEffect(effect);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
