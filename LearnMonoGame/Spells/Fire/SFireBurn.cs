using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.Summoneds.Enemies;
using Microsoft.Xna.Framework;
using LearnMonoGame.Summoneds;
using LearnMonoGame.PlayerComponents;

namespace LearnMonoGame.Spells.Fire
{
    class SFireBurn : Spell
    {
        public SFireBurn() : base(SpellManager.Instance.spellInformation[ESpell.SFireBurn])
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
                        c.ApplyEffect(SpellManager.Instance.attackInformation[EBullet.FireBurn]);

                        timer = 0;
                        channelTimer = 0;
                    }
                }


                PlayerManager.Instance.MyPlayer.ApplyEffect(new IMove(EMoveType.Attack, EStatus.Normal, _mana: -manaCost));
            }


        }

        public override string ToString()
        {
            return "FeuerBurn!";
        }
    }
}
