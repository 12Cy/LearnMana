using LearnMonoGame.Manager;
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
        Tile[,,] _tileMap;
        Point _tileSize;
        int width;
        int height;
        int layerHeight;


        public Tilemap(Texture2D[] textures, Texture2D bitMap, Point _tileSize)
        {
            layerHeight = 5;
            this._tileSize = _tileSize;
            this.width = bitMap.Width;
            this.width = bitMap.Height;
            this._tileMap = new Tile[bitMap.Width, bitMap.Height, layerHeight];

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
                        _tileMap[x, y, 0] = new Tile(textures[0], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.Terrain);
                    }
                    else if (colores[y * _tileMap.GetLength(0) + x] == new Color(0, 162, 232, 255))
                    {
                        _tileMap[x, y, 0] = new Tile(textures[2], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.Terrain);
                    }
                    else if (colores[y * _tileMap.GetLength(0) + x] == Color.Red)
                    {
                        _tileMap[x, y, 0] = new Tile(textures[3], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.Terrain);
                    }
                    else if (colores[y * _tileMap.GetLength(0) + x] == new Color(40, 255, 0))
                    {
                        _tileMap[x, y, 0] = new Tile(textures[4], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.Terrain);
                    }
                    else if (colores[y * _tileMap.GetLength(0) + x] == new Color(255, 0, 182))
                    {
                        _tileMap[x, y, 0] = new Tile(textures[5], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.Terrain);
                    }
<<<<<<< HEAD
                    //ManaSource
                    else if (colores[y * _tileMap.GetLength(0) + x] == new Color(222,255,0))//FARBE EINGEBEN)
                    {
                        _tileMap[x, y, 0] = new Tile(textures[6], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.manaSource);
                        _MapStuff.Instance.manaSourceList.Add(new ManaSource(new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y)));
                    }

=======
>>>>>>> afd3b4e8cd5cbd9608af5108c6d9da2a01c0deea
                    else //sonst Stein
                    {
                        _tileMap[x, y,0] = new Tile(textures[1], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y), ETile.stone);
                        _tileMap[x, y, 1] = new Tile(textures[1], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y - 2*_tileSize.Y), ETile.stone);
                        _tileMap[x, y, 2] = new Tile(textures[1], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y - 4 * _tileSize.Y), ETile.stone);
                        _tileMap[x, y, 3] = new Tile(textures[1], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y - 6 * _tileSize.Y), ETile.stone);
                        _tileMap[x, y, 4] = new Tile(textures[1], new Vector2(x * _tileSize.X + (_tileSize.X / 2 * (y % 2)), y * _tileSize.Y - 8 * _tileSize.Y), ETile.stone);

                    }
                }
            }
        }

        public bool Walkable(Vector2 currentPosition)
        {
            return _tileMap[(int)currentPosition.X / _tileSize.X, (int)currentPosition.Y / _tileSize.Y, 0].Walkable();
        }

        public bool ManaSource(Vector2 currentPosition)
        {
            return _tileMap[(int)currentPosition.X / _tileSize.X, (int)currentPosition.Y / _tileSize.Y, 0].ManaSource();
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
                    _tileMap[x, y, 0].Draw(spritebatch);


                    if (_tileMap[x, y, 1] == null)
                        continue;


                    for (int z = 0; z < _tileMap.GetLength(2); z++)
                        _tileMap[x, y, z].Draw(spritebatch);
                }
            }


        }
        public int WidthInPixels
        {
            get { return width * _tileSize.X; }
        }
        public int HeightInPixels
        {
            get { return height * _tileSize.Y; }
        }

    }

}
