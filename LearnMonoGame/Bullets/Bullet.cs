using LearnMonoGame.Summoneds;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearnMonoGame.Summoneds.Enemies.Elements;

namespace LearnMonoGame.Spells
{
    abstract class Bullet
    {
        protected int MAX_DISTANCE = 750;
        public bool Visible = false;
        public bool alive = true;

        protected Texture2D texture;
        protected Vector2 startPosition;
        protected Vector2 positon;
        protected Rectangle bounds;
        protected EAlignment alignment;

        protected int width;
        protected int height;

        float livetimeTimer;
        float liveTimeTime;
        bool liveTimeCheck = false;

        float triggerTimer;
        float triggerTime;
        bool triggerEvent = false;

        protected float speed = 350; //Default Wert 

        public Rectangle Bounds { get { return bounds; } }

        protected Vector2 direction;

        protected SAbility effect;

        public Bullet(BulletInformation bulletInfo, Rectangle _startPosition, Vector2 _direction, Texture2D _texture, SAbility _effect, EAlignment _alignment)
        {
            alignment = _alignment;


            effect = _effect;
            livetimeTimer = 0;
            liveTimeTime = bulletInfo.lifetime;
            if (liveTimeTime > 0)
                liveTimeCheck = true;

            triggerTimer = 0;
            triggerTime = bulletInfo.triggerTime;
            if (triggerTime == 0)
                triggerEvent = false;
            else
                triggerEvent = true;
            texture = _texture;
            MAX_DISTANCE = bulletInfo.range;
            _startPosition.Size = bulletInfo.size;
            startPosition = new Vector2(_startPosition.X, _startPosition.Y);
            positon = startPosition;
            width = _startPosition.Width;
            height = _startPosition.Height;
            direction = _direction;
            Visible = true; //ToDo: Alive einführen!
            speed = bulletInfo.speed;
        }

        /// <summary>
        /// Wird ausgefüht, wenn die Bullet zerstört wird.
        /// </summary>
        public virtual void OnExplode()
        {

        }

        /// <summary>
        /// Wird ausgeführt, wenn die TriggerTime ausschlägt
        /// </summary>
        public virtual void OnTrigger()
        {

        }

        /// <summary>
        /// Überprüft die TimeEvents.
        /// LifeTime und/oder TriggerTime
        /// </summary>
        /// <param name="gameTime"></param>
        void CheckTimeEvents(GameTime gameTime)
        {
            livetimeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (livetimeTimer > liveTimeTime && liveTimeCheck)
            {
                alive = false;
                livetimeTimer = 0;
            }


            if (triggerEvent)
            {
                triggerTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (triggerTimer > triggerTime)
                {
                    OnTrigger();
                    triggerTimer = 0;
                }
            }
        }

        void Move(GameTime gameTime)
        {
            if (speed == 0 || (direction.X == 0 && direction.Y == 0))
                return;


            //Bewege den Feuerball
            direction.Normalize();
            direction *= (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //ToDO: Animierten Sprite
            positon = positon + direction;
        }

        public virtual void Update(GameTime gameTime)
        {

            if (!alive)
            {
                OnExplode();
                return;
            }


            CheckTimeEvents(gameTime);

            
            if (Vector2.Distance(startPosition, positon) > MAX_DISTANCE)
            {//Ist der Fireball außerhalb der Distance?
                alive = false;
                OnExplode();
                return;
            }

            Move(gameTime);
            Collision();

            //ToDo: Collision mit enemys, Wall etc
            bounds = new Rectangle((int)positon.X, (int)positon.Y, width, height);
        }

        protected virtual void Collision()
        {
            Character c = MonsterManager.Instance.CheckCollisionOne(alignment, bounds);
            if (c == null)
                return;
            c.ApplyEffect(effect);
            alive = false;

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)positon.X, (int)positon.Y, width, height), Color.White);

        }
    }

}
