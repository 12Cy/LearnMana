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

        }
        #endregion

        #region Attributes
        protected float timer;
        protected float maxTimer;
        protected Texture2D texture;
        protected float manaCost;
        #endregion

        #region Methods
        public abstract PlayerModifikator Cast(Vector2 _direction);
        public bool CastAble(Player player)
        {
            return true;
        }

        #endregion
    }
}
