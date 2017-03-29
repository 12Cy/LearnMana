using Microsoft.Xna.Framework;
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

        public TileLayer()
        {

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
            if(str.Contains('}'))
            {
                if(str.Contains(']'))
                    return;
                else
                {
                    parseTileLayer(layerList, strList);
                }
                
            }
        }

        static void parseTileLayer(TileLayer t, List<string> strList)
        {
            string str = strList[0];
            strList.RemoveAt(0);

            str = str.Trim();

            if (str.Contains('}'))
                return;
            else if (str.Contains('{'))
            {
                parseTileLayer(t, strList);
                return;
            }

            string[] split = str.Split(':');



            split[0] = split[0].Trim(' ', '"');
            split[1] = split[1].Trim(' ', '"');

            switch (split[0])
            {
                case "height":
                    split[1] = split[1].Replace(',', ' ');
                    t.size.Y = int.Parse(split[1]);
                    break;
                case "width":
                    split[1] = split[1].Replace(',', ' ');
                    t.size.X = int.Parse(split[1]);
                    break;
                case "x":
                    split[1] = split[1].Replace(',', ' ');
                    t.location.X = int.Parse(split[1]);
                    break;
                case "y":
                    split[1] = split[1].Replace(',', ' ');
                    t.location.Y = int.Parse(split[1]);
                    break;

                case "data":
                    string[] values = split[1].Split(',');
                    List<int> intList = new List<int>();

                    foreach(string s in values)
                    {
                        string help = s.Trim('[', ']');
                        intList.Add(int.Parse(help));
                    }
                    t.data = intList.ToArray();

                    break;

            }
            parseTileLayer(t, strList);
        }

    }
}
