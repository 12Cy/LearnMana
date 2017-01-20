using LearnMonoGame.Components;
using LearnMonoGame.Tools;
using LearnMonoGame.Manager;
using LearnMonoGame.Weapon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LearnMonoGame.PlayerComponents
{
    public interface IBaseFunction
    {
        void Initialize();
        void LoadContent(ContentManager content);
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
    public class Player : IBaseFunction
    {

#region variablen

        Game1 gameRef;
        Texture2D playerTexture;
        AnimatedSprite animatedSprite;
        int size = MapStuff.Instance.size;

        //Select
        Texture2D selectedTexture;
        bool selected;

        //Movement
        float offset = 0.5f;
        float speed = 180f;

        Vector2 pos;
        Vector2 moveDestination;

        //HealthBar
        Texture2D lifeTexture;
        Texture2D manaTexture;
        TimeSpan playerHitTimer = TimeSpan.Zero;

        bool playerhit;
        int offsetHeight = 10;

        //Weapon
        List<Fireball> fireballList = new List<Fireball>();
        Vector2 rangeDestination;
        bool attackMode = false;


        //Life
        float currentHealth;
        float maxHealth = 100;
        float currentMana;
        float maxMana = 100;

#endregion
#region properties

        public Vector2 Pos { get { return pos; } }
        public bool AttackMode { get { return attackMode; } set { attackMode = value; } }
        public void SetSelected(bool value)
        {
            selected = value;
        }

        

#endregion



#region Constructors

        public Player(Game1 _game, Vector2 _position, Texture2D _playerTexture)
        {
            this.gameRef = _game;
            this.pos = _position;
            this.playerTexture = _playerTexture;

        }

#endregion

#region Methods

        public void Initialize()
        {
            moveDestination = pos;
            //Select
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            selected = false;

            attackMode = false;

            //Animation
            animatedSprite = new AnimatedSprite(playerTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.player));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;

            //Life
            currentHealth = maxHealth;
            currentMana = maxMana;

        }

        public void LoadContent(ContentManager content)
        {
            //life
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            manaTexture = _CM.GetTexture(_CM.TextureName.backLife);
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            PlayerMove(gameTime);

            foreach (Fireball aFireball in fireballList)
            {
                aFireball.Update(gameTime);
            }

            if (playerhit)
            {//Wenn der Spieler getroffen wurde, wird der LB angezeigt (für 1 Sekunde)
                playerHitTimer += gameTime.ElapsedGameTime;

                if (playerHitTimer > TimeSpan.FromSeconds(1))
                {//1 Sekunde ist um, LB verschwindet

                    playerHitTimer = TimeSpan.Zero;
                    playerhit = false;
                }
            }

            //ToDo Kampf aktivieren(Fähigkeiten werden hier eingesetzt - Zauber/Beschwörungen)
            if (xIn.CheckKeyReleased(Keys.D1) )
            { 
                moveDestination = pos; //Spieler soll sofort stehen bleiben!
                attackMode = true; //Kann zauber wirken!

            }
            //ToDo Hier alle Zauber rein
            if (xIn.CheckMouseReleased(MouseButtons.Right) && attackMode)
            {
               //Spell: Fireball
                rangeDestination = new Vector2(xIn.MouseState.X, xIn.MouseState.Y) -  new Vector2( pos.X + size/2, pos.Y + size/2);
                ShootFireball();

            }
            DebugShit();

        }

        private void DebugShit()
        {
            KeyboardState newState = Keyboard.GetState();
            if (xIn.CheckKeyReleased(Keys.L))
                CalculateHealth(1);
            if (xIn.CheckKeyReleased(Keys.K))
            {
                CalculateHealth(-1);
                playerhit = true;
            }
            if (newState.IsKeyDown(Keys.I))
                CalculateMana(-1);
            if (newState.IsKeyDown(Keys.O))
                CalculateMana(1);
            if (newState.IsKeyDown(Keys.T))
                playerhit = true;
        }

        private void ShootFireball()
        {
            rangeDestination.Normalize();

            if (rangeDestination.X > 0)
                animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            else
                animatedSprite.CurrentAnimation = AnimationKey.WalkLeft;

            if (Math.Abs(rangeDestination.X / rangeDestination.Y) < offset)
            {
                if(rangeDestination.Y > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
            }

            Fireball aFireball = new Fireball(new Rectangle((int)pos.X + size / 2, (int)pos.Y + size / 2, 20, 17), rangeDestination);
            fireballList.Add(aFireball);
        }

        private void PlayerMove(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (selected && aMouse.RightButton == ButtonState.Pressed && !(attackMode))
            {//Nicht im Angriffsmodus sondern im Movemodus

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

                if(MapStuff.Instance.map.Walkable(newPosition)
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

                //PATHFINDER
                 
            }
            else
            {
                animatedSprite.ResetAnimation();
                animatedSprite.IsAnimating = false;
            }

            animatedSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            animatedSprite.Draw( spritebatch);
            foreach (Fireball aFireball in fireballList)
            {
                aFireball.Draw(spritebatch);

            }

            //LB
            if (selected || playerhit)
            {
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, size, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, (int)(size * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, size, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                                                                    
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5, size, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5 ,(int)(size * ((float)currentMana / maxMana)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5, size, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                spritebatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.White);
            }

        }

        /// <CalculateHealth>
        /// need - or + Value
        /// </CalculateHealth>
        public void CalculateHealth(float value)
        {
            currentHealth += value;
            if (currentHealth > 100)
                currentHealth = 100;

            if (currentHealth < 0)
                currentHealth = 0;

        }
        /// <CalculateMana>
        /// need - or + Value
        /// </CalculateMana>
        public void CalculateMana(float value)
        {
            currentMana += value;
            if (currentMana > 100)
                currentMana = 100;

            if (currentMana < 0)
                currentMana = 0;

        }

#endregion

    }
}
