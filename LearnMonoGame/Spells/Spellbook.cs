using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.Spells
{
    enum ESpellType
    {
        sp_fireball,
        su_dummy,
    }

    public enum ESpellStatus
    {
        NoTarget,
        FoundTarget,
        Channel
    }

    public class Spellbook
    {
        public Spellbook()
        {
            maxSpell = 10;
            spell = new List<Spell>();
            index = 0;
        }

        #region Atrributes


        protected ESpellStatus status;
        protected int maxSpell;
        protected List<Spell> spell;

        int index;
        #endregion


        public ESpellStatus Status { get { return status; } }

        #region Methods
        public bool Cast(GameTime gTime, Vector2 pos, Vector2 _direction, Character me)
        {
            if (CastChannel(gTime, pos, _direction, me))
            {
                spell[index].Cast(pos, _direction, me);
                status = ESpellStatus.NoTarget;
                return true;
            }
            status = ESpellStatus.Channel;
            return false;
        }

        public void AddSpell(Spell sp)
        {
            spell.Add(sp);
        }
        
        public

        bool CastChannel(GameTime gTime, Vector2 pos, Vector2 _direction, Character me)
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].Channel(gTime, pos, _direction, me);
            return true;
        }

        public void Update(GameTime gTime)
        {
            foreach (Spell sp in spell)
                sp.Update(gTime);
        }

        public void NextSpell()
        {
            if (index + 1 < spell.Count)
                index++;
        }

        public void PrevSpell()
        {
            if (index - 1 >= 0)
                index--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public override string ToString()
        {
            if (index >= 0 && index < spell.Count)
                return spell[index].ToString();
            return "null";
        }
        #endregion

    }
}
