using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;

namespace LearnMonoGame.Spells
{
    abstract class Spell
    {
        #region Constructor
        public Spell(SpellInformation spellInfo)
        {
            maxTimer = spellInfo.time;
            manaCost = spellInfo.mana;
            timer = 0;
        }
        #endregion

        #region Attributes
        protected float timer;
        protected float maxTimer;
        protected Texture2D texture;
        protected float manaCost;
        #endregion

        #region Methods
        public abstract PlayerModifikator Cast(Vector2 bounds, Vector2 _direction);
        public bool CastAble()
        {
            if (timer > maxTimer)
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime gTime)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion
    }
}
