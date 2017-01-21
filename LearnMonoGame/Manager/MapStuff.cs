using LearnMonoGame.Components;
using LearnMonoGame.Map;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    class MapStuff
    {

        public Tilemap map;
        public int size = 32;

        public int x = 1280;
        public int y = 720;
        public Camera camera;

        public Vector2 startPosition = new Vector2(0, 0);


        private MapStuff()
        {

        }

        static MapStuff instance;

        public static MapStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new MapStuff();

                return instance;
            }
        }
    }
}

