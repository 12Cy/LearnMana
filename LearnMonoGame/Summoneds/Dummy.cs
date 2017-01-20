using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework.Input;

namespace LearnMonoGame.Summoneds
{
    class Dummy : Summoned
    {
        //Movement
        float offset = 0.5f;
        float speed = 180f;
        int size = 32;
        int offsetHeight = 10;



        public Dummy(Vector2 _position)
        {
            pos = _position;
            creatureTexture = _CM.GetTexture(_CM.TextureName.dummy);
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            Console.WriteLine("TADA");

        }
        public override void Initialize()
        {
            moveDestination = pos;



            //Animation
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.dummy));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;

            //Life
            currentHealth = maxHealth;
            isAlive = true;
            isSelected = false;
            hitTimer = TimeSpan.Zero;



        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void UnloadContent() { }

        public override void Update(GameTime gameTime)
        {
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

        private void Move(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (isSelected && aMouse.RightButton == ButtonState.Pressed)
            {
                moveDestination = new Vector2(aMouse.Position.X, aMouse.Position.Y);
            }

            Vector2 dif = moveDestination - pos; //VerbindungsVektor

            if (dif.Length() < 3f)
            {//Ziel angekommen?

                moveDestination = pos;
                dif = Vector2.Zero;
                return;
            }

            //Vector2 motion = !(dif.X == 0 && dif.Y == 0) ? Vector2.Normalize(dif) : Vector2.Zero;
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
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(size, 0))
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(0, size))
                  && MapStuff.Instance.map.Walkable(newPosition + new Vector2(size, size)))
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


        public override void Draw(SpriteBatch spriteBatch)
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
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size / 4 - 5, size, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size / 4 - 5, (int)(size * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spriteBatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size / 4 - 5, size, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
            }
        }
    }
}

