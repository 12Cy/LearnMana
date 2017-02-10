using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Manager;
using LearnMonoGame.Particle;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Tools;
using LearnMonoGame.Bullets;
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Spells.Ice
{
    class SIceTornado : Spell
    {
        public SIceTornado(EAlignment alignment) : base(SpellManager.Instance.spellInformation["SIceTornado"], alignment)
        {

        }

        public override void Cast(Vector2 bounds, Vector2 _direction, Character me)
        {
            if (CastAble(me))
            {
                _BulletManager.Instance.bullets.Add(new AreaBullet(new Rectangle(_direction.ToPoint(),Point.Zero),Vector2.Zero,
                    _CM.GetTexture(_CM.TextureName.tornado),"IceTornado",alignment));
                timer = 0;
                channelTimer = 0;
                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }


        }

        public override string ToString()
        {
            return "IceTornado!";
        }
    }
}
