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

namespace LearnMonoGame.Spells.Ice
{
    class SIceFreeze : Spell
    {
        public SIceFreeze() : base(SpellManager.Instance.spellInformation[ESpell.SIceFreeze])
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                Rectangle my = new Rectangle(_direction.ToPoint(), new Point(1, 1));
                foreach (Character c in MonsterManager.Instance.enemyList)
                {
                    if (c.Bounds.Intersects(my))
                    {
                        c.ApplyEffect(SpellManager.Instance.attackInformation[EBullet.IceFreeze]);
                        //_ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.burn), _direction, 1f, c, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.burn));

                        timer = 0;
                        channelTimer = 0;
                        PlayerManager.Instance.MyPlayer.ApplyEffect(new IMove(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
                        return;
                    }
                }



            }


        }

        public override string ToString()
        {
            return "IceFreeze!";
        }
    }
}
