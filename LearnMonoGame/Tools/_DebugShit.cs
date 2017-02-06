using LearnMonoGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools
{
    class _DebugShit
    {
        List<Rectangle> RecList = new List<Rectangle>();
        static _DebugShit instance;


        Texture2D rectangle;
        Color[] data;
        int widht;
        int height;

        public _DebugShit(int _width, int _height)
        {
            widht = _width;
            height = _height;

            rectangle = new Texture2D(_MapStuff.Instance.graphics, widht, height);
            data = new Color[widht * height];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Chocolate;
            rectangle.SetData(data);
        }
        public void DebugPointBottom(Rectangle rec)
        {
            Rectangle a = new Rectangle(rec.X + rec.Width / 2, rec.Y + rec.Height, rec.Width / 4, rec.Height / 4);
            RecList.Add(rec);
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            foreach(Rectangle a in RecList)
                spriteBatch.Draw(rectangle, new Vector2(a.X, a.Y), Color.White);

        }
        public void Update(GameTime gTime)
        {

        }


      
    }
}
