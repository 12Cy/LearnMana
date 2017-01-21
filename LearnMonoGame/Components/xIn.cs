using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LearnMonoGame.Manager;

namespace LearnMonoGame.Components
{
    public enum MouseButtons
    {
        Left,
        Right,
        Center
    }

    public class xIn : GameComponent // add it to the list of components without creating a member variable in a class and forget about it
    {
        private static KeyboardState currentKeyboardState = Keyboard.GetState();
        private static KeyboardState previousKeyboardState = Keyboard.GetState();
        private static MouseState currentMouseState = Mouse.GetState();
        private static MouseState previousMouseState = Mouse.GetState();
        private static Vector2 mouseposition = Vector2.Zero;

        public static MouseState MouseState
        {
            get { return currentMouseState; }
        }
        public static KeyboardState KeyboardState
        {
            get { return currentKeyboardState; }
        }
        public static KeyboardState PreviousKeyboardState
        {
            get { return previousKeyboardState; }
        }
        public static MouseState PreviousMouseState
        {
            get { return previousMouseState; }
        }
        /// <MousePosition>
        /// MousePosition wird aktualisiert in der Update(Map mit eingerechnet)
        /// </MousePosition>
        public static Vector2 MousePosition
        {
            get { return mouseposition; }
        }
        
        public xIn(Game game)
        : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            xIn.previousKeyboardState = xIn.currentKeyboardState;
            xIn.currentKeyboardState = Keyboard.GetState();
            xIn.previousMouseState = xIn.currentMouseState;
            xIn.currentMouseState = Mouse.GetState();

            if (MapStuff.Instance.camera != null)
                mouseposition = currentMouseState.Position.ToVector2() * 1f/MapStuff.Instance.camera.Zoom + MapStuff.Instance.camera.Position;
            else
                mouseposition = currentMouseState.Position.ToVector2();

            base.Update(gameTime);
        }
        

        public static void FlushInput()
        {
            currentMouseState = previousMouseState;
            currentKeyboardState = previousKeyboardState;
        }
        /// <CheckKeyReleased>
        /// checks to see if a key that was down last frame is now up.
        /// </CheckKeyReleased>
        public static bool CheckKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }
        

        public static bool CheckMouseReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return (currentMouseState.LeftButton == ButtonState.Released) &&
                    (previousMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.Right:
                    return (currentMouseState.RightButton == ButtonState.Released) &&
                    (previousMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.Center:
                    return (currentMouseState.MiddleButton == ButtonState.Released) &&
                    (previousMouseState.MiddleButton == ButtonState.Pressed);
            }
            return false;
        }
    }
}