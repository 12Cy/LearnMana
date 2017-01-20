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
        //Offset der Texture
        int offsetWidth = 5;
        int offsetHeight = 1;
        Texture2D dottedLine;
        MouseState mPreviousMouseState;
        int size = MapStuff.Instance.size;
        


        public SelectBar()
        {
            mSelectionBox = new Rectangle(-1, -1, 0, 0); //default Position
            mPreviousMouseState = Mouse.GetState();
            dottedLine = _CM.GetTexture(_CM.TextureName.selectCircle);
        }


        public void Update(Player player)
        {
            MouseState aMouse = Mouse.GetState();
            playerBounds = new Rectangle((int)player.Pos.X, (int)player.Pos.Y, size, size);
            //StartLocation !
            if (aMouse.LeftButton == ButtonState.Pressed && mPreviousMouseState.LeftButton == ButtonState.Released)
                mSelectionBox = new Rectangle(aMouse.X, aMouse.Y, 0, 0);


            //still pressed- re-size where the mouse has currently been moved to.
            if (aMouse.LeftButton == ButtonState.Pressed)
            {
                player.SetSelected(false);
                player.AttackMode = false;
                Console.WriteLine("NOT Select");
                mSelectionBox = new Rectangle(mSelectionBox.X, mSelectionBox.Y, aMouse.X - mSelectionBox.X, aMouse.Y - mSelectionBox.Y);
            }
                
            //Store the previous mouse state
            mPreviousMouseState = aMouse;

        }

        /// <CheckSelected>
        /// Überüft ob der Spieler anvisierit wurde
        /// </CheckSelected>
        public bool CheckSelected(Player player)
        {
            if (xIn.CheckMouseReleased(MouseButtons.Left))    
            {
                if (mSelectionBox.Intersects(playerBounds))//player.Pos.X > mSelectionBox.X && player.Pos.X < mSelectionBox.X + mSelectionBox.Width && player.Pos.Y > mSelectionBox.Y && player.Pos.Y < mSelectionBox.Y + mSelectionBox.Height)
                {
                    player.SetSelected(true);
                    mSelectionBox = new Rectangle(-1, -1, 0, 0); //defaultWert
                    Console.WriteLine("Player Select");
                    return true;
                }

                mSelectionBox = new Rectangle(-1, -1, 0, 0); //wenn der Spieler nicht im Bereich war Reset!
            }
            return false;
        }

        public bool CheckSelected(Summoned obj)
        {
            Rectangle objRectangle = new Rectangle((int)obj.Pos.X, (int)obj.Pos.Y, 32, 32);

            if (xIn.CheckMouseReleased(MouseButtons.Left))
            {
                if (mSelectionBox.Intersects(objRectangle))//player.Pos.X > mSelectionBox.X && player.Pos.X < mSelectionBox.X + mSelectionBox.Width && player.Pos.Y > mSelectionBox.Y && player.Pos.Y < mSelectionBox.Y + mSelectionBox.Height)
                {
                    obj.IsSelect = true;
                    mSelectionBox = new Rectangle(-1, -1, 0, 0); //defaultWert
                    Console.WriteLine("Creatur Select");
                    return true;
                }

                mSelectionBox = new Rectangle(-1, -1, 0, 0); //wenn der Spieler nicht im Bereich war Reset!
            }
            return false;
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
