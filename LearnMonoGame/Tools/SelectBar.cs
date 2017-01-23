using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.PlayerComponents;
using LearnMonoGame.Summoneds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools
{
    class SelectBar
    {
        Rectangle mSelectionBox; //Quadrat das man zeichnet, wichtig mit absoluten Werten rechnen dann (ToDo)
        Rectangle playerBounds;
        int offsetHorizonzal = 8;
        int offsetVertical = 8;

        //Offset der Texture(Größe)
        int offsetWidth = 5;
        int offsetHeight = 1;
        Texture2D dottedLine;

        int size = MapStuff.Instance.size;

        MouseState mPreviousMouseState;

        public SelectBar()
        {
            mSelectionBox = new Rectangle(-1, -1, 0, 0); //default Position
            mPreviousMouseState = Mouse.GetState();
            dottedLine = _CM.GetTexture(_CM.TextureName.selectCircle);
        }


        public void Update()
        {
            MouseState aMouse = Mouse.GetState();
            //StartLocation !
            if (aMouse.LeftButton == ButtonState.Pressed && mPreviousMouseState.LeftButton == ButtonState.Released)
                mSelectionBox = new Rectangle((int)xIn.MousePosition.X, (int)xIn.MousePosition.Y, 0, 0);

                


            //still pressed- re-size where the mouse has currently been moved to.
            if (aMouse.LeftButton == ButtonState.Pressed)
            {
                PlayerManager.Instance.MyPlayer.IsSelect = false;
                PlayerManager.Instance.MyPlayer.AttackMode = false;

                foreach (Character a in MonsterManager.Instance.mySummoned)
                        a.IsSelect = false;
                foreach (Character a in MonsterManager.Instance.enemyList)
                    a.IsSelect = false;

                mSelectionBox = new Rectangle(mSelectionBox.X, mSelectionBox.Y, (int)xIn.MousePosition.X - mSelectionBox.X, (int)xIn.MousePosition.Y - mSelectionBox.Y);
            }
            //Store the previous mouse state
            mPreviousMouseState = aMouse;
          
        }

        /// <CheckSelected>
        /// Überüft ob der Spieler anvisierit wurde
        /// </CheckSelected>
        public void CheckSelected()
        {
            bool isFriend = false;
            //Maus loslassen!
            if (xIn.CheckMouseReleased(MouseButtons.Left))    
            {
                
                if(mSelectionBox.Height < 0 && mSelectionBox.Width < 0)
                    mSelectionBox = new Rectangle(mSelectionBox.X + mSelectionBox.Width, mSelectionBox.Y + mSelectionBox.Height, Math.Abs(mSelectionBox.Width), Math.Abs(mSelectionBox.Height));
                if(mSelectionBox.Height < 0 && mSelectionBox.Width > 0)
                    mSelectionBox = new Rectangle(mSelectionBox.X, mSelectionBox.Y + mSelectionBox.Height, Math.Abs(mSelectionBox.Width), Math.Abs(mSelectionBox.Height));
                if(mSelectionBox.Height > 0 && mSelectionBox.Width < 0)
                    mSelectionBox = new Rectangle(mSelectionBox.X + mSelectionBox.Width, mSelectionBox.Y, Math.Abs(mSelectionBox.Width), Math.Abs(mSelectionBox.Height));

                playerBounds = new Rectangle((int)PlayerManager.Instance.MyPlayer.Pos.X, (int)PlayerManager.Instance.MyPlayer.Pos.Y, PlayerManager.Instance.MyPlayer.Width, PlayerManager.Instance.MyPlayer.Height);
                if (mSelectionBox.Intersects(playerBounds))//player.Pos.X > mSelectionBox.X && player.Pos.X < mSelectionBox.X + mSelectionBox.Width && player.Pos.Y > mSelectionBox.Y && player.Pos.Y < mSelectionBox.Y + mSelectionBox.Height)
                {
                    PlayerManager.Instance.MyPlayer.IsSelect = true;
                    isFriend = true;
                   //mSelectionBox = new Rectangle(-1, -1, 0, 0); //defaultWert
                   //Console.WriteLine("Player Select");

                }
                foreach (Character a in MonsterManager.Instance.mySummoned)
                {
                    Rectangle summonRectangle = new Rectangle((int)a.Pos.X, (int)a.Pos.Y, a.Width, a.Height);
                    if (mSelectionBox.Intersects(summonRectangle))
                    {
                        a.IsSelect = true;
                        isFriend = true;
                    }
                        

                }
                if (!isFriend)
                {
                    foreach (Character a in MonsterManager.Instance.enemyList)
                    {
                        Rectangle summonRectangle = new Rectangle((int)a.Pos.X, (int)a.Pos.Y, a.Width, a.Height);
                        if (mSelectionBox.Intersects(summonRectangle))
                            a.IsSelect = true;

                    }
                }
                mSelectionBox = new Rectangle(-1, -1, 0, 0); //wenn der Spieler nicht im Bereich war Reset!
            }

        }



        public void Draw(SpriteBatch spriteBatch)
        {
            DrawHorizontalLine(mSelectionBox.Y, spriteBatch);
            DrawHorizontalLine(mSelectionBox.Y + mSelectionBox.Height, spriteBatch);

            DrawVerticalLine(mSelectionBox.X, spriteBatch);
            DrawVerticalLine(mSelectionBox.X + mSelectionBox.Width, spriteBatch);
        }


        private void DrawHorizontalLine(int thePositionY, SpriteBatch spriteBatch)
        {

            //Größer als 0, dh die Maus bewegt sich nach rechts vom Startpunkt und die with wird positiv
            if (mSelectionBox.Width > 0)
            {
                //Vom Startpunkt nach Rechts zeichnen mit einem offset
                for (int aCounter = 0; aCounter <= mSelectionBox.Width - offsetHorizonzal; aCounter += offsetHorizonzal)
                {
                    if (mSelectionBox.Width - aCounter >= 0) //Nicht größer als die Breite des Quadrats
                    {
                        spriteBatch.Draw(dottedLine, new Rectangle(mSelectionBox.X + aCounter, thePositionY, offsetWidth, offsetHeight), Color.White);
                    }
                }
            }
            //Nach links (Width ist - )
            else if (mSelectionBox.Width < 0)
            {
                //Vom StartPunkt nach Links
                for (int aCounter = -offsetHorizonzal; aCounter >= mSelectionBox.Width; aCounter -= offsetHorizonzal)
                {
                    if (mSelectionBox.Width - aCounter <= 0)
                        spriteBatch.Draw(dottedLine, new Rectangle(mSelectionBox.X + aCounter, thePositionY, offsetWidth, offsetHeight), Color.White);
                }
            }
        }


        private void DrawVerticalLine(int thePositionX, SpriteBatch spriteBatch)
        {
            if (mSelectionBox.Height > 0)
            {
                for (int aCounter = 0; aCounter <= mSelectionBox.Height; aCounter += offsetVertical)
                {
                    if (mSelectionBox.Height - aCounter >= 0)
                    {
                        spriteBatch.Draw(dottedLine, new Rectangle(thePositionX, mSelectionBox.Y + aCounter, offsetWidth, offsetHeight),
                            new Rectangle(0, 0, dottedLine.Width, dottedLine.Height), Color.White, MathHelper.ToRadians(90), new Vector2(0, 0), SpriteEffects.None, 0);
                    }
                }
            }
            else if (mSelectionBox.Height < 0)
            {
                for (int aCounter = -5; aCounter >= mSelectionBox.Height; aCounter -= offsetVertical)
                {
                    if (mSelectionBox.Height - aCounter <= 0)
                        spriteBatch.Draw(dottedLine, new Rectangle(thePositionX, mSelectionBox.Y + aCounter, offsetWidth, offsetHeight),
                        new Rectangle(0, 0, dottedLine.Width, dottedLine.Height), Color.White, MathHelper.ToRadians(90), new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }
        }
    }
}
