using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework;
using LearnMonoGame.Manager;

namespace LearnMonoGame.Spells.Fire
{
    class SFireWall : Spell
    {
        public SFireWall() : base(SpellManager.Instance.spellInformation[(int)ESpell.SFirewall])
        {
        }

        public override PlayerModifikator Cast(Vector2 bounds, Vector2 _direction)
        {
            if (CastAble())
            {
                BulletManager.Instance.bullets.Add(new Bullets.FlameWall(new Rectangle(bounds.ToPoint(), Point.Zero), _direction, _CM.GetTexture(_CM.TextureName.fireball), EBullet.Firewall));
                timer = 0;
            }

            return new PlayerModifikator(_mana: -manaCost);
        }

        public override string ToString()
        {
            return  "FeuerWand!";
        }


    }
}
