using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using LearnMonoGame.Summoneds;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Manager;
using LearnMonoGame.Particle;
using LearnMonoGame.Tools;
using LearnMonoGame.Weapons;

namespace LearnMonoGame.Spells.Light
{
    class SHolyLight : Spell
    {
        public SHolyLight(EAlignment alignment) : base(SpellManager.Instance.spellInformation["SHolyLight"], alignment)
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _direction, Character me)
        {
            if (CastAble(me, bounds, _direction))
            {
                Rectangle my = new Rectangle(_direction.ToPoint(), new Point(1, 1));
                Character c = MonsterManager.Instance.CheckCollisionOne(alignment, my);
                if (c == null)
                    return;

                c.ApplyEffect(SpellManager.Instance.attackInformation["HolyLight"]);
                _ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.heal), _direction, 2, c, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.heal));
                timer = 0;
                channelTimer = 0;

                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }

        }

        public override string ToString()
        {
            return "HolyLight!";
        }
    }
}
