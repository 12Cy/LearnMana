using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds.Enemies;

namespace LearnMonoGame.Spells.Fire
{
    class SFireball : Spell
    {
        
        public SFireball() : base(SpellManager.Instance.spellInformation[ESpell.SFireball])
        {
        }

        public override IMove Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), _direction, _CM.GetTexture(_CM.TextureName.fireball),EBullet.Fireball));
                timer = 0;
                channelTimer = 0;
            }

            return new IMove(EMoveType.Attack,EStatus.Normal, _mana: -manaCost);
        }

        public override string ToString()
        {
            return "Feuerball!";
        }
    }
}
