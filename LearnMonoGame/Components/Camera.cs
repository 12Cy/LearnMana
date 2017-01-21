using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Components
{
    class Camera
    {
        public Vector2 position;
        float zoom;

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public Vector2 Position
        {
            get { return position ; }
        }
        public Camera()
        {
            position = Vector2.Zero;
        }
        public void Reset()
        {
            position = Vector2.Zero;
        }

        public Matrix GetViewMatrix()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += 5;

            return
                Matrix.CreateTranslation(new Vector3(-(int)position.X, -(int)position.Y, 1)) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *  Matrix.CreateScale(new Vector3(new Vector2(1,1), 1));

            //Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
            //                             Matrix.CreateRotationZ(Rotation) *
            //                             Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            //                             Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
           
        }

    }

}

