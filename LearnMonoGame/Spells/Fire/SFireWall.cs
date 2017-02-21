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

namespace LearnMonoGame.Spells.Fire
{
    class SFireWall : Spell
    {
        public SFireWall() : base(SpellManager.Instance.spellInformation["SFireWall"])
        {
        }

        public override void Cast(Vector2 bounds, Vector2 _target)
        {
            if (CastAble())
            {
                _BulletManager.Instance.bullets.Add(new Bullets.FireWall(new Rectangle(bounds.ToPoint(), Point.Zero), _target - bounds, _CM.GetTexture(_CM.TextureName.fireball), "FireWall"));
                timer = 0;
                channelTimer = 0;
                PlayerManager.Instance.MyPlayer.ApplyEffect(new SAbility(EMoveType.Attack, EStatus.Normal, _mana: new[] { -manaCost, -manaCost }));
            }


        }

        public override string ToString()
        {
            return  "FeuerWand!";
        }


    }
}
