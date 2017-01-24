using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame
{
    class Triangle
    {
        public Vector2[] bounds;
        public Vector2 position;
        Texture2D text;

        public Triangle(Vector2[] bounds)
        {
            this.bounds = bounds;
        }

        public Triangle(Vector2[] bounds, Texture2D _texture, Vector2 _position)
        {
            this.bounds = bounds;
            position = _position;

            Color[] clr = new Color[_texture.Width * _texture.Height];

            for(int i = 0; i < _texture.Width; ++i)
                for(int j = 0; j < _texture.Height; ++j)
                {
                    if (intersect(_position + new Vector2(i, j)))
                        clr[i % _texture.Width + j * _texture.Width] = Color.White;
                    else
                        clr[i % _texture.Width + j * _texture.Width] = new Color(0, 0, 0, 0);
                }
        }


        float det(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public bool intersect(Vector2 point)
        {
            Vector2 v0, v1, v2;
            v0 = bounds[0];
            v1 = bounds[1] - bounds[0];
            v2 = bounds[2] - bounds[0];

            float a = (det(point, v2) - det(v0, v2)) / det(v1, v2);
            float b = -(det(point, v1) - det(v0, v1)) / det(v1, v2);

            if (a > 0 && b > 0 && a + b < 1)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < bounds.Length; ++i)
                str += " " + bounds[i];

            return str;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, position, Color.White);
        }
    }
}
