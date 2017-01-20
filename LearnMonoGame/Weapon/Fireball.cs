using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Weapon
{
    class Fireball
    {
        const int MAX_DISTANCE = 750;
        public bool Visible = false;

        Texture2D fireballTexture;
        Vector2 startPosition;
        Vector2 positon;

        int width;
        int height;

        float speed = 350; //Default Wert 


        Vector2 direction;
        public Fireball(Rectangle _startPosition, Vector2 _direction) //Rectangel damit wir gleich die texture skaliert haben
        {

            startPosition = new Vector2(_startPosition.X, _startPosition.Y);
            positon = startPosition;
            width = _startPosition.Width;
            height = _startPosition.Height;
            direction = _direction;
            Visible = true; //ToDo: Alive einführen!

            fireballTexture = _CM.GetTexture(_CM.TextureName.fireball);
        }

        public Fireball(Rectangle theStartPosition, float theSpeed, Vector2 theDirection)
            : this(theStartPosition, theDirection)
        {
            speed = theSpeed;
        }

        public void Update(GameTime gameTime)
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


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(fireballTexture, new Rectangle((int)positon.X, (int)positon.Y, width, height), Color.White);

        }

    }
}
