using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Summoneds.Enemies;

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
        #endregion

        #region Methods
        public void Cast(Vector2 pos, Vector2 _direction, int index)
        {
            if (index >= 0 && index < spell.Count)
                spell[index].Cast(pos, _direction);
        }

        public void AddSpell(Spell sp)
        {
            spell.Add(sp);
        }

        public bool CastChannel(int index, GameTime gTime)
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].Channel(gTime);

            return true;
        }

        public bool CheckChannel(int index)
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].Channel();

            return false;
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
