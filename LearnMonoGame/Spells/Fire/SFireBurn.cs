﻿using System;
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

namespace LearnMonoGame.Spells.Fire
{
    class SFireBurn : Spell
    {
        public SFireBurn() : base(SpellManager.Instance.spellInformation["SFireBurn"])
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                Rectangle my = new Rectangle(_direction.ToPoint(), new Point(1, 1));
                foreach(Character c in MonsterManager.Instance.enemyList)
                {
                    if (c.Bounds.Intersects(my))
                    {
                        c.ApplyEffect(SpellManager.Instance.attackInformation["FireBurn"]);
                        _ParticleManager.Instance.particles.Add(new SimpleParticle(_CM.GetTexture(_CM.TextureName.burn), _direction, 1f, c, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.effects), AnimationKey.burn));

                        timer = 0;
                        channelTimer = 0;
                        PlayerManager.Instance.MyPlayer.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
                    }
                }



            }


        }

        public override string ToString()
        {
            return "FeuerBurn!";
        }
    }
}
