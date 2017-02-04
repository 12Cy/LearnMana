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

namespace LearnMonoGame
{
    class EditorState : IGameState
    {
        Vector2 startingPoint;
        int sizeX;
        int sizeY;

        Point textureSize;

        List<Line> lineList;

        public EditorState()
        {
            sizeX = 10;
            sizeY = 3;
            startingPoint = new Vector2(300, 100);
            textureSize = new Point(64, 37);

            lineList = new List<Line>();
            Texture2D whitePixel = _CM.GetTexture(_CM.TextureName.whitePixel);


            for( int y = 0; y < sizeY; y++)
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

            Initialize();
        }


        public void Initialize()
        {
            
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

            spriteBatch.End();
        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {
            
        }

    }
}
