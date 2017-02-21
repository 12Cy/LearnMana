using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;

namespace LearnMonoGame.Spells.Ice
{
    class SIceLance : Spell
    {
        
        public SIceLance() : base(SpellManager.Instance.spellInformation["SIceLance"])
        {

        }
        public override void Cast(Vector2 bounds, Vector2 _target)
        {
            if (CastAble())
            {
                _BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), _target - bounds, _CM.GetTexture(_CM.TextureName.iceLance), "IceLance"));
                timer = 0;
                channelTimer = 0;
                PlayerManager.Instance.MyPlayer.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }


        }

        public override string ToString()
        {
            return "IceLance!";
        }
    }
}
