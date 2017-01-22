using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Summoneds
{
    public abstract class Summoned
    {

#region Variable

        protected Texture2D creatureTexture;
        protected AnimatedSprite animatedSprite;
        //protected Rectangle myBounds;

        //selected
        protected Texture2D selectedTexture;
        protected bool isSelected;
        protected TimeSpan hitTimer;

        //Movement
        protected Vector2 pos;
        protected Vector2 moveDestination;

        //Life
        protected Texture2D lifeTexture;
        protected bool isAlive;
        protected bool hit;
        protected float currentHealth;

        //Attributes
        protected float maxHealth;
        protected float speed;
        protected float attackSpeed;
        protected int width;
        protected int height;
        protected float damage;
        protected float defense;

        //Offset
        protected float offset = 0.5f;
        protected int offsetHeight = 10; //Gibt die Height der LB Texture an!

        #endregion

        #region properties

        public Vector2 Pos { get { return pos; } }
        public bool IsSelect { get { return IsSelect; } set { isSelected = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

#endregion

#region Constructor

        public Summoned(Attributes info) 
        {
            // --- Attributes --- (Hole mir alle Informationen von SummonedsInformation für das jeweilige Monster)

            speed = info.Speed;
            width = info.Width;
            height = info.Height;
            maxHealth = info.MaxHealth;
            damage = info.Damage;
            defense = info.Defense;

        // --- Life ---
        currentHealth = maxHealth;
            isAlive = true;
            isSelected = false;
            hitTimer = TimeSpan.Zero;

        }

#endregion

#region Methoden

        protected virtual void Initialize(){}

        public virtual void UnloadContent(){}
        public virtual void Update(GameTime gameTime){

            Move(gameTime);

            if (hit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                hitTimer += gameTime.ElapsedGameTime;

                if (hitTimer > TimeSpan.FromSeconds(1))
                {//1 Sekunde ist um, LB verschwindet

                    hitTimer = TimeSpan.Zero;
                    hit = false;
                }
            }
        }

        protected void Move(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (isSelected && aMouse.RightButton == ButtonState.Pressed)
            {
                moveDestination = new Vector2((int)xIn.MousePosition.X, (int)xIn.MousePosition.Y);
            }
            Vector2 dif = moveDestination - pos; //VerbindungsVektor

            if (dif.Length() < 3f)
            {//Ziel angekommen?

                moveDestination = pos;
                dif = Vector2.Zero;
                return;
            }
            Vector2 motion = Vector2.Normalize(dif);

            if (motion != Vector2.Zero)
            {//Soll ich mich bewegen?

                if (motion.X > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkRight;

                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkLeft;

                if (Math.Abs(motion.X / motion.Y) < offset)
                {
                    if (motion.Y > 0)
                        animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                    else
                        animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
                }
            }
            //Movement calculated
            if (motion != Vector2.Zero)
            {
                //motion.Normalize();
                motion *= (speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                Vector2 newPosition = animatedSprite.Position + motion; // the position we are moving to is valid?

                if (MapStuff.Instance.map.Walkable(newPosition)
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, 0))
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(0, height))
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(width, height)))
                {//Ist dort keine Collision?

                    animatedSprite.Position = newPosition;
                    pos = newPosition;
                    animatedSprite.IsAnimating = true;
                }
                else
                {//Collision vorhanden

                    animatedSprite.ResetAnimation();
                    animatedSprite.IsAnimating = false;
                }

                //ToDo: PATHFINDER

            }
            else
            {
                animatedSprite.ResetAnimation();
                animatedSprite.IsAnimating = false;
            }

            animatedSprite.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch);
            //LB
            if (isSelected || hit)
            {
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                if (isSelected)
                    spriteBatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);

            }
        }

        public virtual void CalculateHealth(float value)
        {
            currentHealth += value;
            if (currentHealth > 100)
                currentHealth = 100;

            if (currentHealth < 0)
                currentHealth = 0;

        }
        #endregion
    }
}
