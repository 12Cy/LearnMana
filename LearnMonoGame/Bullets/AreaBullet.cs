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
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Bullets
{
    class AreaBullet : Bullet
    {
        /// <summary>
        /// Erstellt eine Bullet, die einen ganzen Bereich abdeckt.
        /// Beispiel: Tornado, Schutzschild etc.
        /// </summary>
        /// <param name="_startPosition"></param>
        /// <param name="_direction"></param>
        /// <param name="_texture"></param>
        /// <param name="type"></param>
        public AreaBullet( Rectangle _startPosition, Vector2 _direction, Texture2D _texture, string type, EAlignment alignment) : 
            base(SpellManager.Instance.bulletInformation[type], _startPosition, _direction, _texture, SpellManager.Instance.attackInformation[type], alignment)
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
                if(me.Intersects(c.HitBox))
                    c.ApplyEffect(effect);
           }

            foreach (Character c in MonsterManager.Instance.mySummoned)
            {
                if (me.Intersects(c.HitBox))
                    c.ApplyEffect(effect);
            }
            Character k = PlayerManager.Instance.MyPlayer;
                if (me.Intersects(k.HitBox))
                    k.ApplyEffect(effect);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
