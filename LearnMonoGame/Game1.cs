
using LearnMonoGame.Components;
using LearnMonoGame.GameStates;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds.Enemies;
using LearnMonoGame.Tools.Logger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace LearnMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        IGameState state;
        EGameState currState, prevState;
        Song mainSong;

        




        static Rectangle screenRectangle; // bounds of the game screen




        public static Rectangle ScreenRectangle
        {
            get { return screenRectangle; }
        }
        public Game1()
        {
            LogHelper.Instance.ResetAll();
            graphics = new GraphicsDeviceManager(this);
            

            this.IsMouseVisible = true;

            screenRectangle = new Rectangle(0, 0, _MapStuff.Instance.x, _MapStuff.Instance.y);

            graphics.PreferredBackBufferWidth = _MapStuff.Instance.x;
            graphics.PreferredBackBufferHeight = _MapStuff.Instance.y;


            Content.RootDirectory = "Content";
           
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _MapStuff.Instance.graphics = graphics.GraphicsDevice;
            _MapStuff.Instance.game = this;
            currState = EGameState.TitleIntroState;
            prevState = EGameState.None;
            state = new TitleIntroState();

            // TODO: Add your initialization logic here
            Components.Add(new xIn(this));
            _CM myContentManager = new _CM(Content);

            //MediaPlayer.IsRepeating = true;

            SAbility s = new SAbility();
            s.name = "HAllo";
            System.Console.WriteLine(s.name);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //mainSong = Content.Load<Song>("Music\\DanceAroundMana");

            //MediaPlayer.Play(mainSong);
            //MediaPlayer.Volume = 0f;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleGameStates();
            currState = state.Update(gameTime);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }
      
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here


            state.Draw(spriteBatch);
            state.DrawGUI(spriteBatch);
            base.Draw(gameTime);
            
        }

        void HandleGameStates()
        {
            if (prevState != currState)
            {
                state.UnloadContent();

                switch (currState)
                {

                    case EGameState.MainmenuState:
                        state = new MainMenuState();
                        break;
                    case EGameState.PlayState:
                        state = new PlayState(this);
                        break;

                }

                state.Initialize();
            }

            prevState = currState;
        }
    }
}
