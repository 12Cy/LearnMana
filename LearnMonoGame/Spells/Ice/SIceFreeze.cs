using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Manager;
using LearnMonoGame.Particle;
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Spells.Ice
{
    class SIceFreeze : Spell
    {
        public SIceFreeze(EAlignment alignment) : base(SpellManager.Instance.spellInformation["SIceFreeze"], alignment)
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _direction, Character me)
        {
            if (CastAble(me))
            {
                Rectangle my = new Rectangle(_direction.ToPoint(), new Point(1, 1));
                Character c = MonsterManager.Instance.CheckCollisionOne(alignment, my);
                if (c == null)
                    return;

                c.ApplyEffect(SpellManager.Instance.attackInformation["IceFreeze"]);
                //_ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.burn), _direction, 1f, c, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.burn));

                timer = 0;
                channelTimer = 0;
                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }
        }

        public override string ToString()
        {
            return "IceFreeze!";
        }
    }
}
