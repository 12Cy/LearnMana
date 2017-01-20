using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LearnMonoGame.Components;
using Microsoft.Xna.Framework.Input;
using LearnMonoGame.Manager;

namespace LearnMonoGame.GameStates
{
    class TitleIntroState : IGameState
    {


        TimeSpan elapsed;
        Vector2 position;
        SpriteFont font; 
        string message;

        public TitleIntroState()
        {
            Console.WriteLine("TITLEINTROSTATE");
        }


        public void Initialize()
        {
            font = _CM.GetFont(_CM.FontName.Arial);
            message = "PRESS SPACE TO CONTINUE";
            elapsed = TimeSpan.Zero;

            Vector2 size = font.MeasureString(message);
            position = new Vector2((Game1.ScreenRectangle.Width - size.X) / 2,
                        Game1.ScreenRectangle.Bottom - 50 - font.LineSpacing);
        }

        public EGameState Update(GameTime gTime)
        {
            if (xIn.KeyboardState.IsKeyDown(Keys.Space))
            {
                return EGameState.MainmenuState;
            }

            elapsed += gTime.ElapsedGameTime;





            return EGameState.TitleIntroState;
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void UnloadContent()
        {

        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Color color = new Color(142f, 39f, 39f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));
            spriteBatch.DrawString(font, message, position, color);


        }

    }
}
