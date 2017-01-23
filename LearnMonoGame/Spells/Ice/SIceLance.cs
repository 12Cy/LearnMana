using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;

namespace LearnMonoGame.Spells.Ice
{
    class SIceLance : Spell
    {
        
        public SIceLance() : base(SpellManager.Instance.spellInformation[ESpell.SIceLance])
        {

        }
        public override IMove Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), _direction, _CM.GetTexture(_CM.TextureName.iceLance), EBullet.IceLance));
                timer = 0;
                channelTimer = 0;
                return new IMove(EMoveType.Attack, EStatus.Normal, _mana: -manaCost);
            }

            return new IMove();


        }

        public override string ToString()
        {
            return "IceLance!";
        }
    }
}
