using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds.Enemies;
using static LearnMonoGame.Summoneds.Enemies.Elements;

namespace LearnMonoGame.Spells
{
    abstract class Spell
    {
        #region Constructor
        public Spell(SpellInformation spellInfo)
        {
            maxTimer = spellInfo.cooldown;
            manaCost = spellInfo.mana;
            channelMax = spellInfo.channelTime;
            channelTimer = 0;
            timer = 0;
        }
        #endregion

        #region Attributes
        protected float timer;
        protected float maxTimer;
        protected float channelTimer;
        protected float channelMax;
        protected Texture2D texture;
        protected int manaCost;
        #endregion

        #region Methods
        public abstract IMove Cast(Vector2 position, Vector2 _direction);
        public bool CastAble()
        {
            if (timer >= maxTimer && channelTimer >= channelMax)
            {
                return true;
            }

            return false;
        }

        public void Channel(GameTime gTime)
        {
            channelTimer += (float)gTime.ElapsedGameTime.TotalSeconds;
        }

        public void Update(GameTime gTime)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion
    }
}
