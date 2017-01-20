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

        //selected
        protected Texture2D selectedTexture;
        protected bool isSelected;
        protected bool isAlive;
        protected TimeSpan hitTimer;

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination;

        protected Texture2D lifeTexture;

        protected bool hit;
        protected float currentHealth;
        protected float maxHealth = 100;

        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return IsSelect; } set { isSelected = value; } }

        public Summoned() { }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
