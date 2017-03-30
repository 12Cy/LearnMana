using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Map
{
    class Tile
    {
        Vector2 _position;
        Texture2D _texture;
        public TileAttributes type;



        public Tile(Texture2D _texture, Vector2 _position, TileAttributes _type)
        {
            this._position = _position;
            this._texture = _texture;
            this.type = _type;

            MonsterManager.Instance.SpawnCharacter(type.spawnEntity, _position);
        }


        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, position: _position, color: Color.White,layerDepth: _MapStuff.Instance.map.GetLayerDepth(_position + type.tileDepth));
        }
        public bool Walkable()
        {
            return type.walkable;
        }
        public bool ManaSource()
        {
            return type.manaSource;
        }
    }
}
