using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds.Enemies;
using static LearnMonoGame.Summoneds.Enemies.Elements;
using LearnMonoGame.Weapons;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.Spells.Fire
{
    class SFireWall : Spell
    {
        public SFireWall(EAlignment alignment) : base(SpellManager.Instance.spellInformation["SFireWall"],alignment)
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _target, Character me)
        {
            if (CastAble(me, bounds, _target))
            {
                _BulletManager.Instance.bullets.Add(new Bullets.FireWall(new Rectangle(bounds.ToPoint(), Point.Zero), _target - bounds, 
                    _CM.GetTexture(_CM.TextureName.fireball), "FireWall", alignment));
                timer = 0;
                channelTimer = 0;
                me.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }


        }

        public override string ToString()
        {
            return  "FeuerWand!";
        }


    }
}
