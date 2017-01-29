using LearnMonoGame.Components;
using LearnMonoGame.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Manager
{
    class _MapStuff
    {
        public GraphicsDevice graphics;
        public Game1 game;
        public Tilemap map;
        public List<ManaSource> manaSourceList = new List<ManaSource>();
        public int size = 64;

        public int x = 1280;
        public int y = 720;
        public Camera camera;

        public Vector2 startPosition = new Vector2(0, 0);


        private _MapStuff()
        {

        }

        static _MapStuff instance;

        public static _MapStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new _MapStuff();

                return instance;
            }
        }
    }
}

