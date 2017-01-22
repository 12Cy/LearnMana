using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Spells
{
    abstract class Bullet
    {
        protected const int MAX_DISTANCE = 750;
        public bool Visible = false;

        protected Texture2D fireballTexture;
        protected Vector2 startPosition;
        protected Vector2 positon;
        protected Rectangle bounds;

        protected int width;
        protected int height;

        protected float speed = 350; //Default Wert 

        public Rectangle Bounds { get { return bounds; } }

        protected Vector2 direction;

        public Bullet(Rectangle _startPosition, Vector2 _direction)
        {

            startPosition = new Vector2(_startPosition.X, _startPosition.Y);
            positon = startPosition;
            width = _startPosition.Width;
            height = _startPosition.Height;
            direction = _direction;
            Visible = true; //ToDo: Alive einführen!
        }

        public virtual void Update(GameTime gameTime)
        {
            //ToDo: Feuerball zerstoeren
            if (Vector2.Distance(startPosition, positon) > MAX_DISTANCE)
            {//Ist der Fireball außerhalb der Distance?
                Visible = false;
                return;
            }


            //Bewege den Feuerball
            direction.Normalize();
            direction *= (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            //ToDO: Animierten Sprite
            positon = positon + direction;

            //ToDo: Collision mit enemys, Wall etc
            bounds = new Rectangle((int)positon.X, (int)positon.Y, width, height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(fireballTexture, new Rectangle((int)positon.X, (int)positon.Y, width, height), Color.White);

        }
    }

}
