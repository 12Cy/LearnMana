using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Map
{
    public class Tilemap
    {
        Tile[,] _tileMap;
        int _tileSize;
        int width;
        int height;


        public Tilemap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
        {
            this._tileSize = _tileSize;
            this.width = bitMap.Width;
            this.width = bitMap.Height;
            this._tileMap = new Tile[bitMap.Width, bitMap.Height];

            BuildMap(textures, bitMap);
        }
        void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colores = new Color[bitMap.Width * bitMap.Height];

            bitMap.GetData(colores);

            /*
                   TileMap: Die Tiles habene eine gewissen Größe (tileSize).
                   Wir berechnen für jedes Tail den Vector * TailSize um die genaue Position zu bestimmten!
                   Danach prüfen wir die Farben und ordnen dementsprechend Tiles zu
            */

            for (int y = 0; y < _tileMap.GetLength(1); y++) //nur die höhe möchte ich haben
            {
                for (int x = 0; x < _tileMap.GetLength(0); x++)
                {
                    if (colores[y * _tileMap.GetLength(0) + x] == Color.White) //Weiß = Gras auf der TileMap
                    {
                        _tileMap[x, y] = new Tile(textures[0], new Vector2(x * _tileSize, y * _tileSize), ETile.Terrain);
                    }
                    else if (colores[y * _tileMap.GetLength(0) + x] == new Color(0, 162, 232, 255))
                    {
                        _tileMap[x, y] = new Tile(textures[2], new Vector2(x * _tileSize, y * _tileSize), ETile.Terrain);
                    }

                    else //sonst Stein
                    {
                        _tileMap[x, y] = new Tile(textures[1], new Vector2(x * _tileSize, y * _tileSize), ETile.stone);
                    }
                }
            }
        }

        public bool Walkable(Vector2 currentPosition)
        {
            return _tileMap[(int)currentPosition.X / _tileSize, (int)currentPosition.Y / _tileSize].Walkable();
        }

        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spritebatch)
        {



            for (int y = 0; y < _tileMap.GetLength(1); y++) //nur die höhe möchte ich haben
            {

                for (int x = 0; x < _tileMap.GetLength(0); x++)
                {
                    _tileMap[x, y].Draw(spritebatch);
                }
            }


        }
        public int WidthInPixels
        {
            get { return width * _tileSize; }
        }
        public int HeightInPixels
        {
            get { return height * _tileSize; }
        }

    }

}
