using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework.Graphics;

namespace LearnMonoGame.Spells
{
    enum ESpellType
    {
        sp_fireball,
        su_dummy,
    }
    class Spellbook
    {
        public Spellbook()
        {
            maxSpell = 10;
            spell = new List<Spell>();
        }

        #region Atrributes
        protected int maxSpell;
        protected List<Spell> spell;


        int currentSpell;
        #endregion

        #region Methods
        public PlayerModifikator Cast(Vector2 pos, Vector2 _direction, int index)
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].Cast(pos, _direction);

            return new PlayerModifikator();
        }

        public void AddSpell(Spell sp)
        {
            spell.Add(sp);
        }

        public void Update(GameTime gTime)
        {
            foreach (Spell sp in spell)
                sp.Update(gTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public string ToString(int index)
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].ToString();
            return "null";
        }
        #endregion

    }
}
