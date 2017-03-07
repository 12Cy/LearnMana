using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace LearnMonoGame.Components
{
    public enum MouseButtons
    {
        Left,
        Right,
        Center
    }

    public class xIn // add it to the list of components without creating a member variable in a class and forget about it
    {
        private static KeyboardState currentKeyboardState = Keyboard.GetState();
        private static KeyboardState previousKeyboardState = Keyboard.GetState();
        private static MouseState currentMouseState = Mouse.GetState();
        private static MouseState previousMouseState = Mouse.GetState();
        private static Vector2 mouseposition = Vector2.Zero;

        public static string StrMousePosition() => mouseposition.ToString();

        public static MouseState MouseState => currentMouseState;

        public static KeyboardState KeyboardState => currentKeyboardState;

        public static KeyboardState PreviousKeyboardState => previousKeyboardState;

        public static MouseState PreviousMouseState
        {
            get { return previousMouseState; }
        }
        /// <MousePosition>
        /// MousePosition wird aktualisiert in der Update(Map mit eingerechnet)
        /// </MousePosition>
        public static Vector2 MousePosition => mouseposition;

        public xIn(Game game)
        {
        }

        public static void Update(GameTime gameTime)
        {
            xIn.previousKeyboardState = xIn.currentKeyboardState;
            xIn.currentKeyboardState = Keyboard.GetState();
            xIn.previousMouseState = xIn.currentMouseState;
            xIn.currentMouseState = Mouse.GetState();

            if (_MapStuff.Instance.camera != null)
                mouseposition = currentMouseState.Position.ToVector2() * 1f/_MapStuff.Instance.camera.Zoom + _MapStuff.Instance.camera.Position;
            else
                mouseposition = currentMouseState.Position.ToVector2();
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