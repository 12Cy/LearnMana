using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;

namespace LearnMonoGame.Spells
{
    enum ESpellType
    {
        sp_fireball,
        su_dummy,
    }
    abstract class Spellbook
    {
        public Spellbook(SpellbookInformation spellInfo)
        {

        }

        #region Atrributes
        protected Spell[] spell;
        #endregion

        #region Methods
        public virtual PlayerModifikator Cast(Rectangle bounds, Vector2 _direction, byte index)
        {
            if (index >= 0 && index < spell.Length)
                return spell[index].Cast(bounds, _direction);

            return new PlayerModifikator();
        }
        #endregion

    }
}
