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
using LearnMonoGame.Weapons;
using LearnMonoGame.Summoneds;

namespace LearnMonoGame.Spells
{
    public abstract class Spell
    {
        #region Constructor
        public Spell(SpellInformation spellInfo, EAlignment _alignment)
        {
            alignment = _alignment;
            maxTimer = spellInfo.cooldown;
            manaCost = spellInfo.mana;
            channelMax = spellInfo.channelTime;
            triggerTimer = 0;
            triggerMax = spellInfo.triggerTime;
            channelTimer = 0;
            timer = 0;
            range = spellInfo.range;
        }
        #endregion

        #region Attributes
        protected float range;
        protected float timer;
        protected float maxTimer;
        protected float channelTimer;
        protected float channelMax;
        protected float triggerTimer;
        protected float triggerMax;
        protected Texture2D texture;
        protected int manaCost;
        protected EAlignment alignment;
        private SpellInformation spellInformation;
        #endregion

        #region Methods
        public abstract void Cast(Vector2 position, Vector2 _direction, Character me);
        public virtual void OnChannel(Vector2 positiion, Vector2 _direction, Character me)
        {
            
        }
        public bool CastAble(Character me, Vector2 positiion, Vector2 _direction)
        {

            int _range = 0;
            if(range != 0)
                _range = (int)(me.Bounds.Location.ToVector2() - MonsterManager.Instance.CheckNearestCharacter(alignment, new Rectangle((int)_direction.X, (int)_direction.Y, 1, 1)).Bounds.Location.ToVector2()).Length();
            if (timer >= maxTimer && channelTimer >= channelMax && me.CurrMana >= manaCost && (range == 0 || _range < range))
            {
                return true;
            }

            return false;
        }

        public bool Channel(GameTime gTime, Vector2 position, Vector2 _direction, Character me)
        {
            channelTimer += (float)gTime.ElapsedGameTime.TotalSeconds;
            triggerTimer += (float)gTime.ElapsedGameTime.TotalSeconds;
            if (channelTimer >= channelMax) return true;
            if (triggerTimer >= triggerMax)
            {
                triggerTimer = 0;
                OnChannel(position, _direction, me);
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
