using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds.Enemies
{
    enum EElement
    {
        Dark, Earth, Fire, Light, Water, Wind
    }
    public abstract class Enemy
    {

#region Variable

        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;
        protected Rectangle bounds;

        // --- Movement ---
        protected Vector2 pos;
        protected Vector2 moveDestination;

        // --- Life ---
        protected Texture2D lifeTexture;
        protected bool isAlive;
        protected bool hit;
        protected TimeSpan hitTimer;
        protected float currentHealth;

        // --- Attributes ---
        protected float maxHealth;
        protected float speed;
        protected float damage;
        protected float defense;
        protected float attackSpeed;
        protected int width;
        protected int height;

        // --- Offset ---
        protected float offset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        // --- Fight ---
        protected float radius;


        // --- EP ---
        protected int level;
        protected float experience;

        // --- Enum ---
        protected List<IMove> effects;
        protected Dictionary<string, IMove> knownMoves;

#endregion

#region properties

        public Vector2 Pos { get { return pos; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        #endregion

        public List<IMove> Effects
        {
            get { return effects; }
        }

        public Dictionary<string, IMove> KnownMoves
        {
            get { return knownMoves; }
        }
        public Rectangle Bounds { get { return bounds; } }


        #region Constructor

        public Enemy(Attributes info)
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            speed = info.Speed;
            width = info.Width;
            height = info.Height;
            maxHealth = info.MaxHealth;
            defense = info.Defense;
            damage = info.Damage;

            bounds = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            level = 1;
            experience = 0;



            knownMoves = new Dictionary<string, IMove>();
            effects = new List<IMove>();

            // --- Life ---
            currentHealth = maxHealth;
            hit = false;
            hitTimer = TimeSpan.Zero;
            isAlive = true;
        }

        #endregion


        protected virtual void Initialize() { }

        public virtual void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)pos.X, (int)pos.Y, width, height);

            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Duration--;
                if (effects[i].Duration < 1)
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }

            if (hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitTimer += gameTime.ElapsedGameTime;

                if (hitTimer > TimeSpan.FromSeconds(2))
                {//1 Sekunde ist um, LB verschwindet

                    hitTimer = TimeSpan.Zero;
                    hit = false;
                }
            }
        }
        public virtual void CalculateHealth(float value)
        {
            hit = true;
            currentHealth += value;
            if (currentHealth > 100)
                currentHealth = 100;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;
                
            }


        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch);

            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Red);
            spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
        }











        }
}
