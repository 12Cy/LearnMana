using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Weapons;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.Spells.Ice
{
    class SIceLance : Spell
    {
        
        public SIceLance(EAlignment alignment) : base(SpellManager.Instance.spellInformation["SIceLance"], alignment)
        {

        }
        public override void Cast(Vector2 bounds, Vector2 _target, Character me)
        {
            if (CastAble(me))
            {
                _BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), _target - bounds, 
                    _CM.GetTexture(_CM.TextureName.iceLance), "IceLance", alignment));
                timer = 0;
                channelTimer = 0;
                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }


        }

        public override string ToString()
        {
            return "IceLance!";
        }
    }
}
