using LearnMonoGame.Manager;
using LearnMonoGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Components
{
    public class Camera
    {
        int offsetPercent = 20;
        public Vector2 position;
        float zoom;

        public Rectangle Bounds { get { return new Rectangle((int)position.X, (int)position.Y, _MapStuff.Instance.x, _MapStuff.Instance.y); } }

        public string StrBounds() => Bounds.ToString() + " Zoom: " + zoom;

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
        public void ResetZoom()
        {
            zoom = 1;
        }

        public Matrix GetViewMatrix()
        {
            float difX = (float)offsetPercent / 100 * _MapStuff.Instance.x;
            float difY = (float)offsetPercent / 100 * _MapStuff.Instance.y;

            Vector2 aMouse = xIn.MousePosition;

            BoolClass.MouseInsideWindow = Bounds.Contains(aMouse);

            if (BoolClass.MouseInsideWindow)
            {
                if (aMouse.X < Bounds.X + difX)
                    position.X -= 5;
                if (aMouse.X > Bounds.Width + Bounds.X - difX)
                    position.X += 5;
                if (aMouse.Y < Bounds.Y + difY)
                    position.Y -= 5;
                if (aMouse.Y > Bounds.Height + Bounds.Y - difY)
                    position.Y += 5;
            }
            return
                Matrix.CreateTranslation(new Vector3(-(int)position.X, -(int)position.Y, 1)) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *  Matrix.CreateScale(new Vector3(new Vector2(1,1), 1));

            //Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
            //                             Matrix.CreateRotationZ(Rotation) *
            //                             Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
            //                             Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
           
        }

    }

}

