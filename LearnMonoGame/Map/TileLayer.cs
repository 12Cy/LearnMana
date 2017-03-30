using LearnMonoGame.Manager;
using LearnMonoGame.Tools.Logger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Map
{
    class TileLayer
    {
        Point size;
        Point location;
        int[] data;
        Tile[] tiles;
        Texture2D layerTexture;
        bool visible;

        public TileLayer()
        {

        }

        public bool Walkable(Vector2 currentPosition)
        {
            try
            {
                return tiles[((int)currentPosition.Y / _MapStuff.Instance.map._tileSize.Y) * _MapStuff.Instance.map.mapSize.X + ((int)currentPosition.X / _MapStuff.Instance.map._tileSize.X)].Walkable();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ManaSource(Vector2 currentPosition)
        {
            try
            {
                return tiles[((int)currentPosition.Y / _MapStuff.Instance.map._tileSize.Y) * _MapStuff.Instance.map.mapSize.X + ((int)currentPosition.X / _MapStuff.Instance.map._tileSize.X)].Walkable();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DrawTileTexture(SpriteBatch spriteBatch)
        {
            if (visible)
                spriteBatch.Draw(layerTexture, location.ToVector2(), layerDepth:0f);
        }

        public void DrawTiles(SpriteBatch spriteBatch)
        {

            if (visible)
                foreach (Tile t in tiles)
                    t.Draw(spriteBatch);

        }


        public void CreateTiles(List<TileSet> tileSets, Tilemap tileMap)
        {
            tiles = new Tile[data.Length];
            layerTexture = new Texture2D(_MapStuff.Instance.graphics, tileMap.mapSize.X * tileMap._tileSize.X, tileMap.mapSize.Y * tileMap._tileSize.Y);

            tileSets[0].LoadTexture();

            for (int i = 0; i < data.Length; ++i)
            {
                Texture2D tileTexture = tileSets[0].GetTileTexture(data[i]);
                tiles[i] = new Tile(tileTexture, new Vector2((i % tileMap.mapSize.X) * tileMap._tileSize.X, (i / tileMap.mapSize.Y) * tileMap._tileSize.Y), tileSets[0].GetTileTypeFromIndex(data[i]));

                if (data[i] == 0)
                    continue;

                Rectangle dest = new Rectangle((i % tileMap.mapSize.X) * tileMap._tileSize.X, (i / tileMap.mapSize.Y) * tileMap._tileSize.Y, tileMap._tileSize.X, tileMap._tileSize.Y);

                Color[] clr = new Color[tileMap._tileSize.X * tileMap._tileSize.Y];

                tileTexture.GetData(clr);

                layerTexture.SetData(0, dest, clr, 0, clr.Length);

            }
        }

        public static void parseTileLayer(List<TileLayer> layerList, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            if (str.Contains('{'))
            {
                TileLayer t = new TileLayer();
                parseTileLayer(t, strList);
                layerList.Add(t);
            }
            else if (str.Contains('}') && str.Contains(']'))
                return;

            parseTileLayer(layerList, strList);

        }


        static void parseTileLayer(TileLayer t, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            LogHelper.Instance.Log(Logtarget.ParserLog, "Layer: " + str);

            string[] split = str.Split(':');

            if (split.Length > 1)
            {


                split[0] = split[0].Trim(' ', '"');
                split[1] = split[1].Trim(' ', '"');

                switch (split[0])
                {
                    case "visible":
                        split[1] = split[1].Replace(',', ' ');
                        t.visible = bool.Parse(split[1]);

                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerSize.Visible: " + t.visible);
                        break;
                    case "height":
                        split[1] = split[1].Replace(',', ' ');
                        t.size.Y = int.Parse(split[1]);

                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerSize.Y: " + t.size.Y);
                        break;
                    case "width":
                        split[1] = split[1].Replace(',', ' ');
                        t.size.X = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerSize.X: " + t.size.X);
                        break;
                    case "x":
                        split[1] = split[1].Replace(',', ' ');
                        t.location.X = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerLocation.X: " + t.location.X);
                        break;
                    case "y":
                        split[1] = split[1].Replace(',', ' ');
                        t.location.Y = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerLocation.Y: " + t.location.Y);
                        break;

                    case "data":
                        string[] values = split[1].Split(',');
                        List<int> intList = new List<int>();

                        foreach (string s in values)
                        {
                            string help = s.Trim('[', ']');
                            int i = 0;
                            if (int.TryParse(help, out i))
                            {
                                intList.Add(i);
                            }

                        }

                        LogHelper.Instance.Log(Logtarget.ParserLog, "LayerData: " + t.data);

                        t.data = intList.ToArray();
                        break;

                }
            }
            else
            {
                split[0] = split[0].Replace(',', ' ');
                if (split[0].Contains('}'))
                {
                    strList.Insert(0, str);
                    return;
                }
            }
            parseTileLayer(t, strList);
        }

    }
}
