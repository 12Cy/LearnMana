﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds.Enemies;
using static LearnMonoGame.Summoneds.Enemies.Elements;

namespace LearnMonoGame.Spells.Fire
{
    class SFireball : Spell
    {
        
        public SFireball() : base(SpellManager.Instance.spellInformation[ESpell.SFireBall])
        {
        }

        public override IMove Cast(Vector2 bounds, Vector2 _target)
        {


            if (CastAble())
            {
                BulletManager.Instance.bullets.Add(new Bullets.SimpleBullet(new Rectangle(bounds.ToPoint(), Point.Zero), _target - bounds, _CM.GetTexture(_CM.TextureName.fireball),EBullet.FireBall));
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