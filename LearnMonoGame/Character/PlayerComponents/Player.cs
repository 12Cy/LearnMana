﻿using LearnMonoGame.Components;
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
using LearnMonoGame.Weapons;

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
        Vector2 rangeDestination;
        bool attackMode = false;

        bool channelMode = false;


        #endregion
        #region properties


        public bool AttackMode { get { return attackMode; } set { attackMode = value; } }
        public Spellbook Spellbook { get { return spellBook; } }

        #endregion



        #region Constructors

        public Player(Vector2 _position, Texture2D _playerTexture) : base(SummonedsInformation.Instance.characterInformation["Player"])
        {
            this.pos = new Vector2(_position.X, _position.Y);

            this.creatureTexture = _playerTexture;
            spellBook = new Spellbook();
            spellBook.AddSpell(new SFireball(EAlignment.Player));
            spellBook.AddSpell(new SFireWall(EAlignment.Player));
            spellBook.AddSpell(new SIceLance(EAlignment.Player));
            spellBook.AddSpell(new SFireBurn(EAlignment.Player));
            spellBook.AddSpell(new SHolyLight(EAlignment.Enemy));
            spellBook.AddSpell(new SIceTornado(EAlignment.All));
            Spellbook.AddSpell(new SFireInferno(EAlignment.Player));
            spellBook.AddSpell(new SIceFreeze(EAlignment.Player));
            spellBook.AddSpell(new SIceSleep(EAlignment.Player));
            attributes.Alignment = EAlignment.Player;
            Initialize();
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            moveDestination = pos;

            attributes.Alignment = EAlignment.Player;

            attackMode = false;

            //Animation
            animatedSprite = new AnimatedSprite(creatureTexture, _AnimationManager.GetAnimation(_AnimationManager.AnimationName.player));
            animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            animatedSprite.Position = pos;

            //life
            lifeTexture = _CM.GetTexture(_CM.TextureName.backLife);
            manaTexture = _CM.GetTexture(_CM.TextureName.backLife);

            posDestination = pos;

            //Select
            selectedTexture = _CM.GetTexture(_CM.TextureName.selected);
            damageselectedTexture = _CM.GetTexture(_CM.TextureName.damageselect);

        }


        public override void Update(GameTime gameTime)
        {
            moveDestinationAnimation.Update(gameTime);


            if (spellBook.Status == ESpellStatus.Channel)
            {
                spellBook.Cast(gameTime, pos, xIn.MousePosition, this);
                return;
            }

            if (xIn.CheckKeyReleased(Keys.D3))
                spellBook.NextSpell();
            if (xIn.CheckKeyReleased(Keys.D4))
                spellBook.PrevSpell();


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
                CastSpell(gameTime);

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
                statusClass.hit = true;
            }
            if (newState.IsKeyDown(Keys.I))
                CalculateMana(-1);
            if (newState.IsKeyDown(Keys.O))
                CalculateMana(1);
            if (newState.IsKeyDown(Keys.T))
                statusClass.hit = true;
        }

        private void CastSpell(GameTime gTime)
        {
            rangeDestination.Normalize();

            if (rangeDestination.X > 0)
                animatedSprite.CurrentAnimation = AnimationKey.WalkRight;
            else
                animatedSprite.CurrentAnimation = AnimationKey.WalkLeft;

            if (Math.Abs(rangeDestination.X / rangeDestination.Y) < healthBarOffset)
            {
                if (rangeDestination.Y > 0)
                    animatedSprite.CurrentAnimation = AnimationKey.WalkDown;
                else
                    animatedSprite.CurrentAnimation = AnimationKey.WalkUp;
            }
            if (spellBook.Cast(gTime, pos, xIn.MousePosition, this))
                channelMode = true;
            else
            {
                spellBook.Cast(gTime, pos, xIn.MousePosition, this);
            }
        }

        private void PlayerMove(GameTime gameTime)
        {
            MouseState aMouse = Mouse.GetState();

            if (IsSelect && aMouse.RightButton == ButtonState.Pressed && !(attackMode))
            {//Nicht im Angriffsmodus sondern im Movemodus

                //Setzt den ORIGIN! (!!!)

                moveDestination = new Vector2((int)PosDestination.X - attributes.Width / 2, (int)posDestination.Y - attributes.Height);
                //moveDestinationAnimation.ResetAnimation();
                statusClass.isRunning = true;
                moveDestinationAnimation.IsAnimating = true;
                moveDestinationAnimation.Position = new Vector2(moveDestination.X + 16, moveDestination.Y + 48);
            }

            Vector2 dif = moveDestination - pos; //VerbindungsVektor
            //Move(gameTime, dif);

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            //animatedSprite.Draw(spritebatch);
            //if(isRunning && isSelected)
            //     moveDestinationAnimation.Draw(spritebatch);

            //LB

            if (statusClass.isSelected || statusClass.hit)
            {
                MouseState amouse = Mouse.GetState();

                /// <Lebensbalken>
                /// Wir zeichnen zuerst  eine Background Farbe(1.Schicht), die Füllfarbe(2.Schicht), texture mit der Umrandung(3.Schicht).
                /// Die Texture soll über dem Spieler gezeichnet werden
                /// (DesitinationRectangle) :  size/4 -5 (ca), size = x-Achse soll gleich des Spielers sein, OffsetHeight Höhe des LB
                /// (SourceRectangle) :  Geht vom äußeren Rectangle aus(DesitinationRectangle)
                ///  (2.Schicht) :  nehme die diff und verkleinere so die größe der Schicht.
                /// </Lebensbalken>
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 3 + 9, attributes.Width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 3 + 9, (int)(attributes.Width * ((float)attributes.CurrentMana / attributes.MaxMana)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Gainsboro);
                spritebatch.Draw(manaTexture, new Rectangle((int)pos.X, (int)pos.Y - attributes.Height / 3 + 9, attributes.Width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);

                //spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 45), Color.Gray);
                //spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, (int)(width * ((float)currentHealth / maxHealth)), offsetHeight), new Rectangle(0, 45, lifeTexture.Width, 44), Color.Aquamarine);
                //spritebatch.Draw(lifeTexture, new Rectangle((int)pos.X, (int)pos.Y - height / 4 - 5, width, offsetHeight), new Rectangle(0, 0, lifeTexture.Width, 45), Color.White);

                //if (hit)
                //    spritebatch.Draw(damageselectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);
                //else
                //    spritebatch.Draw(selectedTexture, new Rectangle((int)pos.X, (int)pos.Y, width, height), Color.White);

            }
            base.Draw(spritebatch);

        }





    }
    #endregion


}
