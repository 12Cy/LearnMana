using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.GraphicClasses
{
    public class Sprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Rectangle sourceRectangle;
        public SpriteEffects effect;
        public float layerDepth;
        Vector2 position;
        public Vector2 origin;
        public float rotation;
        public Color color;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                destinationRectangle = new Rectangle(position.ToPoint(), new Point(destinationRectangle.Width, destinationRectangle.Height));
            }
        }

        public Rectangle DestinationRectangle
        {
            get { return destinationRectangle; }
            set
            {
                destinationRectangle = value;
                position = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                destinationRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D _texture, Vector2 _position)
            : this(_texture, new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height)))
        {

        }

        public Sprite(Texture2D _texture, Rectangle _destinationRectangle)
            : this(_texture, _destinationRectangle, new Rectangle(0, 0, _texture.Width, _texture.Height))
        {

        }

        public Sprite(Texture2D _texture, Rectangle _destinationRectangle, Rectangle _sourceRectangle)
            : this(_texture, _destinationRectangle, _sourceRectangle, new Color(255, 255, 255))
        {

        }

        public Sprite(Texture2D _texture, Rectangle _destinationRectangle, Rectangle _sourceRectangle, Color _color, float _rotation = 0f)
            : this(_texture, _destinationRectangle, _sourceRectangle, _color, _rotation, new Vector2(_destinationRectangle.Width / 2, _destinationRectangle.Height / 2))
        {

        }

        public Sprite(Texture2D _texture, Vector2 position, Color _color, float _rotation, Vector2 origin)
            : this(_texture, new Rectangle(position.ToPoint(), new Point(_texture.Width, _texture.Height)), new Rectangle(0, 0, _texture.Width, _texture.Height), _color, _rotation, origin)
        {

        }

        public Sprite(Texture2D _texture, Rectangle _destinationRectangle, Rectangle _sourceRectangle, Color _color, float _rotation, Vector2 _origin)
            : this(_texture, _destinationRectangle, _sourceRectangle, _color, _rotation, _origin, SpriteEffects.None)
        {

        }

        public Sprite(Texture2D _texture, Rectangle _destinationRectangle, Rectangle _sourceRectangle, Color _color, float _rotation, Vector2 _origin, SpriteEffects _effect, float _layerDepth = 0f)
        {
            texture = _texture;
            destinationRectangle = _destinationRectangle;
            sourceRectangle = _sourceRectangle;
            color = _color;
            rotation = _rotation;
            origin = _origin;
            effect = _effect;
            layerDepth = _layerDepth;
        }

        public void AddRotation(float _rotation)
        {
            rotation += _rotation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, (float)MathHelper.DegreeToRadian(rotation), origin, effect, layerDepth);
        }
    }
}
