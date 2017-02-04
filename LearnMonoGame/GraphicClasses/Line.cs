using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.GraphicClasses
{
    public class Line
    {
        Vector2 startingPoint;
        Vector2 endPoint;
        float thickness;

        Sprite sprite;

        public Line(Vector2 _startingPoint, Vector2 _endPoint, Texture2D _whitePixel, float _thickness = 1f)
            : this(_startingPoint, _endPoint, _whitePixel, _thickness, Color.White)
        {

        }

        public Line(Vector2 _startingPoint, Vector2 _endPoint, Texture2D _whitePixel, float _thickness, Color _color)
        {
            startingPoint = _startingPoint;
            endPoint = _endPoint;
            thickness = _thickness;

            float angle = (float)MathHelper.AngleBetween((startingPoint - endPoint), new Vector2(1, 0));
            //angle = (startingPoint.Y > endPoint.Y) ? angle : 360 - angle;
            int length = (int)(startingPoint - endPoint).Length();

            sprite = new Sprite(_whitePixel, new Rectangle((int)((startingPoint.X + endPoint.X) * 0.5f), (int)((startingPoint.Y + endPoint.Y) * 0.5f), length, (int)_thickness), _whitePixel.Bounds, _color, angle, new Vector2(0.5f, 0.5f));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
