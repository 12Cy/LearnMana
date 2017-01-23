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
        public SFireWall() : base(SpellManager.Instance.spellInformation[ESpell.SFireWall])
        {
        }

        public override IMove Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                BulletManager.Instance.bullets.Add(new Bullets.FireWall(new Rectangle(bounds.ToPoint(), Point.Zero), _direction, _CM.GetTexture(_CM.TextureName.fireball), EBullet.FireWall));
                timer = 0;
                channelTimer = 0;
                return new IMove(EMoveType.Attack,EStatus.Normal, _mana: -manaCost);
            }

            return new IMove();


        }

        public override string ToString()
        {
            return  "FeuerWand!";
        }


    }
}
