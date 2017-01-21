using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public abstract class Summoned
    {

        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;
        //protected Rectangle myBounds;

        //selected
        protected Texture2D selectedTexture;
        protected bool isSelected;
        protected bool isAlive;
        protected TimeSpan hitTimer;

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination;

        //Life
        protected Texture2D lifeTexture;

        protected bool hit;
        protected float currentHealth;

        //Attributes
        protected float maxHealth;
        protected float speed;
        protected float attackSpeed;
        protected int textureSize;

        public Summoned(Attributes info)
        {
            //Attributes
            speed = info.Speed;
            textureSize = info.TextureSize;
            maxHealth = info.MaxHealth;

            Initialize();
        }

        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return IsSelect; } set { isSelected = value; } }

        public Summoned() { }
        protected abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
