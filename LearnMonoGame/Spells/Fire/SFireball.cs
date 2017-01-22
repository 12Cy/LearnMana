using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework;

namespace LearnMonoGame.Spells.Fire
{
    class SFireball : Spell
    {
        
        public SFireball(SpellInformation spellInfo) : base(spellInfo)
        {
        }

        public override PlayerModifikator Cast(Rectangle bounds, Vector2 _direction)
        {
            Manager.BulletManager.Instance.bullets.Add(new Bullets.Fireball(bounds, _direction));
            return new PlayerModifikator(_mana: -manaCost);
        }
    }
}
