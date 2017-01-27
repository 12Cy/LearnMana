using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Map
{
    enum ETile
    {
        stone,
        Terrain,
        manaSource
    }
    class Tile
    {
        Vector2 _position;
        Texture2D _texture;
        public ETile type;



        public Tile(Texture2D _texture, Vector2 _position, ETile _type)
        {
            this._position = _position;
            this._texture = _texture;
            this.type = _type;
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, _position, Color.White);


        }
        public bool Walkable()
        {
            return type == ETile.Terrain || type == ETile.manaSource;
        }
        public bool ManaSource()
        {
            return type == ETile.manaSource;
        }
    }
}
