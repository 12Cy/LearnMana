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
            triggerTimer = 0;
            triggerMax = spellInfo.triggerTime;
            channelTimer = 0;
            timer = 0;
        }
        #endregion

        #region Attributes
        protected float timer;
        protected float maxTimer;
        protected float channelTimer;
        protected float channelMax;
        protected float triggerTimer;
        protected float triggerMax;
        protected Texture2D texture;
        protected int manaCost;
        #endregion

        #region Methods
        public abstract void Cast(Vector2 position, Vector2 _direction);
        public virtual void OnChannel(Vector2 positiion, Vector2 _direction)
        {

        }
        public bool CastAble()
        {
            if (timer >= maxTimer && channelTimer >= channelMax)
            {
                return true;
            }

            return false;
        }

        public bool Channel(GameTime gTime, Vector2 position, Vector2 _direction)
        {
            channelTimer += (float)gTime.ElapsedGameTime.TotalSeconds;
            triggerTimer += (float)gTime.ElapsedGameTime.TotalSeconds;
            if (channelTimer >= channelMax) return true;
            if (triggerTimer >= triggerMax)
            {
                triggerTimer = 0;
                OnChannel(position, _direction);
            }
            return false;
        }

        public bool Channel()
        {
            if (channelTimer < channelMax) return true;
            return false;
        }

        public void Update(GameTime gTime)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;
        }

        #endregion
    }
}
