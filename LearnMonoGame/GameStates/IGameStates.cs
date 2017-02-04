using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.GameStates
{
    enum EGameState
    {
        None = -1,
        MainmenuState,
        PlayState,
        TitleIntroState,
        EditorState
    }

    interface IGameState
    {

        void Initialize();
 
        

        EGameState Update(GameTime gTime);
        void Draw(SpriteBatch spriteBatch);

        void DrawGUI(SpriteBatch spriteBatch);
        void UnloadContent();



    }
}
