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
    class MainMenuState : IGameState
    {


        public MainMenuState()
        {
            Console.WriteLine("MAINMENUSTATE");
        }


        public void Initialize()
        {

        }

        public void LoadContent(ContentManager content)
        {
          
        }

        public void UnloadContent()
        {
            
        }

        public EGameState Update(GameTime gTime)
        {

            if (xIn.KeyboardState.IsKeyDown(Keys.D))
            {
                return EGameState.PlayState;
            }


            //Console.WriteLine("Breite: " + mSelectionBox.Width + " - Höhe: " + mSelectionBox.Height);



            return EGameState.MainmenuState;
        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }


        

    }
}
