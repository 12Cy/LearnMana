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

namespace LearnMonoGame.Spells.Light
{
    class SHolyLight : Spell
    {
        public SHolyLight() : base(SpellManager.Instance.spellInformation[ESpell.SHolyLight])
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                Rectangle my = new Rectangle(_direction.ToPoint(), new Point(1, 1));
                foreach (Character c in MonsterManager.Instance.mySummoned)
                {
                    if (c.Bounds.Intersects(my))
                    {
                        c.ApplyEffect(SpellManager.Instance.attackInformation[EBullet.HolyLight]);
                        _ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.heal), _direction, 2, c, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.heal));
                        timer = 0;
                        channelTimer = 0;
                    }
                }

                if (my.Intersects(PlayerManager.Instance.MyPlayer.Bounds))
                {
                    PlayerManager.Instance.MyPlayer.ApplyEffect(SpellManager.Instance.attackInformation[EBullet.HolyLight]);
                    _ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.heal), _direction, 2, PlayerManager.Instance.MyPlayer, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.heal));
                    timer = 0;
                    channelTimer = 0;
                }

                PlayerManager.Instance.MyPlayer.ApplyEffect(new IMove(EMoveType.Attack, EStatus.Normal, _mana: -manaCost));
            }


        }

        public override string ToString()
        {
            return "HolyLight!";
        }
    }
}
