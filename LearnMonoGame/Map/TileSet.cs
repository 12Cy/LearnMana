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
    class TileSet
    {
        int colum;
        int tilecount;
        Point tileSize;
        Point size;
        Texture2D textureTileSet;
        string name;

        public TileSet()
        {

        }

        void LoadTexture()
        {
            string[] split = this.name.Split('/');
            string name = split[split.Length-1];
            textureTileSet = _CM.Content.Load<Texture2D>("Map/TileSet/" + name.Split('.')[0]);
        }

        public Texture2D GetTileTexture(int index)
        {
            LoadTexture();

            index--;

            Texture2D tile;

            if (index >= 0)
            {

                Rectangle source = new Rectangle((index % colum) * tileSize.X, (index / colum) * tileSize.Y, tileSize.X, tileSize.Y);

                tile = new Texture2D(_MapStuff.Instance.graphics, source.Width, source.Height);
                Color[] color = new Color[source.Width * source.Height];
                textureTileSet.GetData(0, source, color, 0, color.Length);
                tile.SetData(color);
            }
            else
            {
                tile = new Texture2D(_MapStuff.Instance.graphics, tileSize.X, tileSize.Y);
            }
            return tile;
        }

        public static void ParseTileSet(List<TileSet> tileList, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            if (str.Contains('{'))
            {
                TileSet t = new TileSet();
                ParseTileSet(t, strList);
                tileList.Add(t);
            }
            else if (str.Contains('}'))
            {
                if (str.Contains(']'))
                    return;
            }
            ParseTileSet(tileList, strList);
        }

        static void ParseTileSet(TileSet t, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet: " + str);

            string[] split = str.Split(':');

            if (split.Length > 1)
            {

                split[0] = split[0].Trim(' ', '"');
                split[1] = split[1].Trim(' ', '"');

                switch (split[0])
                {
                    case "columns":
                        split[1] = split[1].Replace(',', ' ');
                        t.colum = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "Column: " + t.colum);
                        break;
                    case "imageheight":
                        split[1] = split[1].Replace(',', ' ');
                        t.size.Y = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.size.Y: " + t.size.Y);
                        break;
                    case "imagewidth":
                        split[1] = split[1].Replace(',', ' ');
                        t.size.X = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.size.X: " + t.size.X);
                        break;
                    case "tileheight":
                        split[1] = split[1].Replace(',', ' ');
                        t.tileSize.Y = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.tileSize.Y: " + t.tileSize.Y);
                        break;
                    case "tilewidth":
                        split[1] = split[1].Replace(',', ' ');
                        t.tileSize.X = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.tilewidth: " + t.tileSize.X);
                        break;
                    case "image":
                        split[1] = split[1].Replace(',', ' ');
                        t.name = split[1];
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.Name: " + t.name);
                        break;

                }
            }
            else
            {
                split[0] = split[0].Replace(',', ' ');
                if (split[0].Contains('}') && split[0].Contains(']'))
                {
                    strList.Insert(0, str);
                    return;
                }
                else if (split[0].Contains('}'))
                    return;
                else if (split[0].Contains('{'))
                {
                    ParseTileSet(t, strList);
                }
            }

            ParseTileSet(t, strList);
        }
    }
}
