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
using LearnMonoGame.Spells.Ice;
using LearnMonoGame.Spells.Fire;
using LearnMonoGame.Spells;
using LearnMonoGame.Spells.Light;

namespace LearnMonoGame.PlayerComponents
{
    //Um dem Player verschiedene Modifikation (+3 Rüstung, +1 ManaRegeneration) fasse ich solche Eigenschaften
    //in einem Struct zusammen, die der Player eigenständig verarbeiten kann
    //Wenn health = 0 -> Der Spieler bekommt 0 Heilung
    //Wenn maxHealth = 5 -> Der Spieler kommt auf seine maxHealth + 5 aufaddiert
    //Wenn alle Attribute auf 0 -> Es passiert garnichts (z.B. ich trage ein Stoffhemd - vielleicht noch Rüstung +1 :P)


    class Player : Character
    {

        #region variablen

        //HealthBar
        Texture2D manaTexture;

        //Weapon

        int fireBallCost = 15;
        Vector2 rangeDestination;
        bool attackMode = false;

        bool channelMode = false;


        //Spellbook
        Spellbook spellBook;
        int currentSpell;



        #endregion
        #region properties


        public bool AttackMode { get { return attackMode; } set { attackMode = value; } }
        public Spellbook Spellbook { get { return spellBook; } }
        public int CurrentSpell { get { return currentSpell; } }

        #endregion



        #region Constructors

        public Player(Vector2 _position, Texture2D _playerTexture) : base(SummonedsInformation.Instance.playerInformation)
        {
            this.pos = new Vector2(_position.X, _position.Y);

            this.creatureTexture = _playerTexture;
            spellBook = new Spellbook();
            spellBook.AddSpell(new SFireball());
            spellBook.AddSpell(new SFireWall());
            spellBook.AddSpell(new SIceLance());
            spellBook.AddSpell(new SFireBurn());
            spellBook.AddSpell(new SHolyLight());
            spellBook.AddSpell(new SIceTornado());
            Spellbook.AddSpell(new SFireInferno());
            spellBook.AddSpell(new SIceFreeze());
            currentSpell = 0;
            Initialize();
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            moveDestination = pos;

            characterTyp = ECharacterTyp.player;
            element = EElement.none;

            //Attributes
            maxMana = SummonedsInformation.Instance.playerInformation.maxMana;

            attackMode = false;

            //Animation
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.player));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;



            //Life & Mana
            currentMana = maxMana;

            //life
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            manaTexture = _CM.GetTexture(_CM.TextureName.backLife);

            //Select
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            damageselectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);



        }


        public override void Update(GameTime gameTime)
        {
            spellBook.Update(gameTime);
            moveDestinationAnimation.Update(gameTime);


            if (channelMode)
            {
                if (spellBook.CastChannel(currentSpell, gameTime, pos, xIn.MousePosition))
                {
                    spellBook.Cast(pos, xIn.MousePosition, currentSpell);
                    attackMode = false;
                    channelMode = false;
                }
                return;
            }

            if (xIn.CheckKeyReleased(Keys.D3))
                currentSpell++;
            if (xIn.CheckKeyReleased(Keys.D4))
                currentSpell--;


            PlayerMove(gameTime);


            //ToDo Kampf aktivieren(Fähigkeiten werden hier eingesetzt - Zauber/Beschwörungen)
            if (xIn.CheckKeyReleased(Keys.D1))
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
            base.Update(gameTime);

        }

        private void DebugShit()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.L))
                CalculateHealth(1);
            if (newState.IsKeyDown(Keys.K))
            {
                CalculateHealth(-1);
                hit = true;
            }
            if (newState.IsKeyDown(Keys.I))
                CalculateMana(-1);
            if (newState.IsKeyDown(Keys.O))
                CalculateMana(1);
            if (newState.IsKeyDown(Keys.T))
                hit = true;
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
                if (rangeDestination.Y > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
            }
            if (spellBook.CheckChannel(currentSpell))
                channelMode = true;
            else
            {
                spellBook.Cast(pos, xIn.MousePosition, currentSpell);
            }
        }

        private void PlayerMove(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (IsSelect && aMouse.RightButton == ButtonState.Pressed && !(attackMode))
            {//Nicht im Angriffsmodus sondern im Movemodus

                //Setzt den ORIGIN! (!!!)
                
                moveDestination = new Vector2((int)PosDestination.X - width / 2, (int)posDestination.Y - height);
                //moveDestinationAnimation.ResetAnimation();
                isRunning = true;
                moveDestinationAnimation.IsAnimating = true;
                moveDestinationAnimation.Position = new Vector2(moveDestination.X + 16, moveDestination.Y +  48);
            }

            Vector2 dif = moveDestination - pos; //VerbindungsVektor
            Move(gameTime, dif);

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            animatedSprite.Draw(spritebatch);
            if(isRunning)
                 moveDestinationAnimation.Draw(spritebatch);

            //LB
            if (isSelected || hit)
            {
                MouseState amouse = Mouse.GetState();

                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 3 + 9, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 3 + 9, (int)(width * ((float)currentMana / maxMana)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 3 + 9, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);

                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);
                if (hit)
                    spritebatch.Draw(damageselectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
                else
                    spritebatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);

            }

        }





    }
    #endregion


}
