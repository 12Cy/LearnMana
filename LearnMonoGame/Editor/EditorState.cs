using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMonoGame.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.GraphicClasses;
using LearnMonoGame.Manager;
using Microsoft.Xna.Framework.Input;

namespace LearnMonoGame
{
    class EditorState : IGameState
    {
        Vector2 startingPoint;
        int sizeX;
        int sizeY;

        Point textureSize;

        SpriteFont font;

        List<Line> lineList;

        public EditorState()
        {
            sizeX = 12;
            sizeY = 4;
            startingPoint = new Vector2(300, 100);
            textureSize = new Point(64, 37);

            lineList = new List<Line>();
            Texture2D whitePixel = _CM.GetTexture(_CM.TextureName.whitePixel);


            if(sizeX > sizeY)
            {
                // start with lines on the top and bottom
                if (sizeY % 2 == 0)
                {
                    for (int x = 0; x < sizeX; x++)
                    {

                        if (x % 2 == 1)
                        {
                            Vector2 startY = new Vector2(startingPoint.X + x * textureSize.X * 0.5f + textureSize.X * 0.5f, startingPoint.Y + textureSize.Y * 0.5f+ sizeY * textureSize.Y);
                            // lines starting at the bottom
                            lineList.Add(new Line(startY, new Vector2(startY.X + textureSize.X * sizeY + 0.5f * textureSize.X, startingPoint.Y), whitePixel));
                            lineList.Add(new Line(startY, new Vector2(startY.X - (textureSize.X * sizeY + 0.5f * textureSize.X), startingPoint.Y), whitePixel));
                        }
                        else
                        {
                            Vector2 startX = new Vector2(startingPoint.X + textureSize.X * 0.5f + x * textureSize.X * 0.5f, startingPoint.Y);
                            // lines starting at the top
                            lineList.Add(new Line(startX, new Vector2(startX.X + textureSize.X * sizeY + 0.5f * textureSize.X, startX.Y + textureSize.Y * sizeY + 0.5f * textureSize.Y), whitePixel));   // to the right
                            lineList.Add(new Line(startX, new Vector2(startX.X - ((sizeY < x / 2) ? textureSize.X * sizeY + 0.5f * textureSize.X : ((sizeY * 2 == x)? x : (x + 1)) * textureSize.X * 0.5f), (startX.Y + ((sizeY < x / 2) ? textureSize.Y * sizeY + 0.5f * textureSize.Y: x * 0.5f * textureSize.Y + ((sizeY * 2 == x)? 0: 0.5f * textureSize.Y)))), whitePixel)); // to the left
                        }
                    }
                }
                else
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        for (int x = 0; x < sizeX; x++)
                        {

                            if (x % 2 == 1)
                            {

                            }
                            else
                            {
                                Vector2 start = new Vector2(startingPoint.X + x * textureSize.X * 0.5f, startingPoint.Y);
                                //int rightHelp = ()? sizeY : sizeY - ()
                                lineList.Add(new Line(start, new Vector2(start.X + textureSize.X * sizeY + 0.5f * textureSize.X, start.Y + textureSize.Y * sizeY + 0.5f * textureSize.Y), whitePixel));   // to the right
                                lineList.Add(new Line(start, new Vector2(start.X - (textureSize.X * sizeY + 0.5f * textureSize.X), (start.Y + textureSize.Y * sizeY + 0.5f * textureSize.Y)), whitePixel));   // to the left
                            }
                        }
                    }
                }


            }
            else
            {
                // start with lines on the left and right
                for (int y = 0; y < sizeY; y++)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        
                        if (x % 2 == 0)
                        {

                        }
                        else
                        {
                            Vector2 start = new Vector2(startingPoint.X + x * textureSize.X * 0.5f, startingPoint.Y);
                            //int rightHelp = ()? sizeY : sizeY - ()
                            lineList.Add(new Line(start, new Vector2(start.X + textureSize.X * sizeY + 0.5f * textureSize.X, start.Y + textureSize.Y * sizeY + 0.5f * textureSize.Y), whitePixel));   // to the right
                            lineList.Add(new Line(start, new Vector2(start.X - (textureSize.X * sizeY + 0.5f * textureSize.X), (start.Y + textureSize.Y * sizeY + 0.5f * textureSize.Y)), whitePixel));   // to the left
                        }
                    }
                }
            }

            Initialize();
        }


        public void Initialize()
        {
            font = _CM.GetFont(_CM.FontName.Arial);
        }

        public void UnloadContent()
        {
            
        }

        public EGameState Update(GameTime gTime)
        {
            return EGameState.EditorState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (Line line in lineList)
            {
                line.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, Mouse.GetState().Position.ToString(), Mouse.GetState().Position.ToVector2(), Color.Red);

            spriteBatch.End();
        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {
            
        }

    }
}
