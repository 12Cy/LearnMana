using LearnMonoGame.Map;
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

