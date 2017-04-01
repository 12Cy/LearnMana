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
    class TileAttributes
    {
        public bool walkable;
        public string spawnEntity;
        public bool manaSource;
        public bool playerStartPosition;
        public Vector2 tileDepth;

        public TileAttributes()
        {
            tileDepth = Vector2.Zero;
            walkable = true;
            spawnEntity = "";
            manaSource = false;
            playerStartPosition = false;
        }

        public static void ParseTileAttriutes(TileAttributes t, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes: " + str);

            string[] split = str.Split(':');

            if (split.Length > 1)
            {

                split[0] = split[0].Trim(' ', '"');
                split[0] = split[0].ToLower();
                split[1] = split[1].Trim(' ', '"');

                switch (split[0])
                {
                    case "walkable":
                        split[1] = split[1].Replace(',', ' ');
                        t.walkable = bool.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes.Walkable: " + t.walkable);
                        break;
                    case "spawn":
                        split[1] = split[1].Replace(',', ' ');
                        t.spawnEntity = split[1];
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes.Spawn: " + t.spawnEntity);
                        break;
                    case "manasource":
                        split[1] = split[1].Replace(',', ' ');
                        t.manaSource = bool.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes.ManaSource: " + t.manaSource);
                        break;
                    case "depthoffset":
                        split[1] = split[1].Replace(',', ' ');
                        t.tileDepth.Y = int.Parse(split[1]);                        
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes.TileDepth.Y: " + t.tileDepth.Y);
                        break;
                    case "playerposition":
                        split[1] = split[1].Replace(',', ' ');
                        t.playerStartPosition = bool.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileAttributes.PlayerPos: " + t.playerStartPosition);
                        break;

                }
            }
            else
            {
                if (split[0].Contains('}'))
                    return;
            }

            ParseTileAttriutes(t, strList);

        }
    }
    class TileSet
    {
        int colum;
        int tilecount;
        Point tileSize;
        Point size;
        Texture2D textureTileSet;
        string name;
        int firstItem;

        Dictionary<int, TileAttributes> dictTiles;
        Dictionary<int, Texture2D> dictTextures;

        public TileSet()
        {
            dictTiles = new Dictionary<int, TileAttributes>();
            dictTextures = new Dictionary<int, Texture2D>();
        }

        public void LoadTexture()
        {
            string[] split = this.name.Split('/');
            string name = split[split.Length - 1];
            textureTileSet = _CM.Content.Load<Texture2D>("Map/TileSet/" + name.Split('.')[0]);
        }

        public TileAttributes GetTileTypeFromIndex(int index)
        {
            index -= firstItem;

            if (dictTiles.ContainsKey(index))
                return dictTiles[index];
            else
                return new TileAttributes();
        }

        public Texture2D GetTileTexture(int index)
        {

            index -= firstItem;

            if (index >= tilecount)
                return null;

            Texture2D tile;

            if (index >= 0)
            {
                if (dictTextures.ContainsKey(index))
                    tile = dictTextures[index];
                else
                {
                    Rectangle source = new Rectangle((index % colum) * tileSize.X, (index / colum) * tileSize.Y, tileSize.X, tileSize.Y);

                    tile = new Texture2D(_MapStuff.Instance.graphics, source.Width, source.Height);
                    Color[] color = new Color[source.Width * source.Height];
                    textureTileSet.GetData(0, source, color, 0, color.Length);
                    tile.SetData(color);

                    dictTextures.Add(index, tile);
                }
            }
            else
            {
                if (dictTextures.ContainsKey(-1))
                    tile = dictTextures[index];
                else
                {
                    dictTextures.Add(-1, new Texture2D(_MapStuff.Instance.graphics, tileSize.X, tileSize.Y));
                    tile = dictTextures[-1];
                }
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

        static void ParseTileProperties(TileSet t, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            LogHelper.Instance.Log(Logtarget.ParserLog, "TileProperties: " + str);

            string[] split = str.Split(':');

            split[0] = split[0].Replace(',', ' ');

            split[0] = split[0].Trim(' ', '"');

            if (split[0].Contains('}'))
                return;
            else if (!split[0].Contains('{'))
            {
                int key = int.Parse(split[0]);
                TileAttributes tAtt = new TileAttributes();
                strList.RemoveAt(0);
                TileAttributes.ParseTileAttriutes(tAtt, strList);

                LogHelper.Instance.Log(Logtarget.ParserLog, "Key: " + key + " Walkable: " + tAtt.walkable + "|" + tAtt.spawnEntity);

                t.dictTiles.Add(key, tAtt);


            }

            ParseTileProperties(t, strList);
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
                    case "tilecount":
                        split[1] = split[1].Replace(',', ' ');
                        t.tilecount = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.tilecount: " + t.tilecount);
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
                    case "tileproperties":
                        strList.RemoveAt(0);
                        ParseTileProperties(t, strList);
                        break;
                    case "firstgid":
                        split[1] = split[1].Replace(',', ' ');
                        t.firstItem = int.Parse(split[1]);
                        LogHelper.Instance.Log(Logtarget.ParserLog, "TileSet.firstItem: " + t.firstItem);
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
