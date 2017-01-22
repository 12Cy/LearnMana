using LearnMonoGame.Components;
using LearnMonoGame.Tools;
using LearnMonoGame.Manager;
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
using LearnMonoGame.Summoneds;
using LearnMonoGame.Spells;
using LearnMonoGame.Spells.Fire;

namespace LearnMonoGame.PlayerComponents
{
    //Um dem Player verschiedene Modifikation (+3 Rüstung, +1 ManaRegeneration) fasse ich solche Eigenschaften
    //in einem Struct zusammen, die der Player eigenständig verarbeiten kann
    //Wenn health = 0 -> Der Spieler bekommt 0 Heilung
    //Wenn maxHealth = 5 -> Der Spieler kommt auf seine maxHealth + 5 aufaddiert
    //Wenn alle Attribute auf 0 -> Es passiert garnichts (z.B. ich trage ein Stoffhemd - vielleicht noch Rüstung +1 :P)

    public struct PlayerModifikator
    {
        float speed;

        //health = spontante Heilung, healthMax = maximale Gesundheit, healthReg = Gesundheits-Regeneration
        float health, healthMax, healthReg;
        float mana, manaMax, manaReg;

        public PlayerModifikator(float _healthMax = 0, float _healthReg = 0, float _health = 0
            , float _mana = 0, float _manaMax = 0, float _manaReg = 0
            , float _speed = 0)
        {
            speed = _speed;


            mana = _mana;
            manaMax = _manaMax;
            manaReg = _manaReg;


            health = _health;
            healthMax = _healthMax;
            healthReg = _healthReg;
        }
        
    }


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
        Texture2D damageselectedTexture;
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
        
        int fireBallCost = 15;
        Vector2 rangeDestination;
        bool attackMode = false;


        //Life
        float currentHealth;
        float maxHealth = 100;
        float currentMana;
        float maxMana = 100;


        //Spellbook
        Spellbook spellBook;

        int currentSpell;



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
            spellBook = new Spellbook();
            spellBook.AddSpell(new SFireball());
            spellBook.AddSpell(new SFireWall());
            currentSpell = 0;
            Initialize();
        }

#endregion

#region Methods

        public void Initialize()
        {
            moveDestination = pos;
            //Select
            
            selected = false;

            attackMode = false;

            //Animation
            animatedSprite = new AnimatedSprite(playerTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.player));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;

            //Life
            currentHealth = maxHealth;
            currentMana = maxMana;

            //life
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            manaTexture = _CM.GetTexture(_CM.TextureName.backLife);

            //Select
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            damageselectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);



        }

        public void LoadContent(ContentManager content)
        {


        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (xIn.CheckKeyReleased(Keys.NumPad4))
                currentSpell = 0;
            if (xIn.CheckKeyReleased(Keys.NumPad5))
                currentSpell = 1;

            spellBook.Update(gameTime);


            PlayerMove(gameTime);

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
            //Vielleicht dann doch nicht ALLE. :D
            if (xIn.CheckMouseReleased(MouseButtons.Right) && attackMode)
            {
                CastSpell(0);

            }
            if (xIn.CheckKeyReleased(Keys.D2) && attackMode)
            {
                //Spell: Summon Dummy
                Dummy a = new Dummy(new Vector2((int)xIn.MousePosition.X, (int)xIn.MousePosition.Y));
                MonsterManager.Instance.mySummoned.Add(a);
            }
            DebugShit();

        }

        private void DebugShit()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.L))
                CalculateHealth(1);
            if (newState.IsKeyDown(Keys.K))
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

        private void CastSpell(int index)
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

            spellBook.Cast(pos, xIn.MousePosition - pos, currentSpell);
        }

        private void PlayerMove(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (selected && aMouse.RightButton == ButtonState.Pressed && !(attackMode))
            {//Nicht im Angriffsmodus sondern im Movemodus

                moveDestination = new Vector2((int)xIn.MousePosition.X, (int)xIn.MousePosition.Y);
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

                //ToDo: PATHFINDER
                 
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

            //LB
            if (selected || playerhit)
            {
                MouseState amouse = Mouse.GetState();
                spritebatch.DrawString(_CM.GetFont(_CM.FontName.Arial), spellBook.ToString(currentSpell), xIn.MousePosition, Color.Red);
                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, size, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, (int)(size * ((float)currentMana / maxMana)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - size /4 - 5, size, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                                                                    
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5, size, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5 ,(int)(size * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - size/2 -5, size, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                if(playerhit)
                    spritebatch.Draw(damageselectedTexture, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.White);
                else
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
