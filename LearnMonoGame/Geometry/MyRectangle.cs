using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearnMonoGame
{
    class MyRectangle
    {

        Texture2D texture;
        Texture2D debug;
        float angle;
        Vector2 position;
        Vector2 size;

        public Vector2[] bounds;
        public Rectangle rect;
        Triangle[] triangles;

        public MyRectangle(Texture2D[] texture, Vector2 pos, float ang)
        {
            bounds = new Vector2[4];
            triangles = new Triangle[6];
            angle = ang;
            position = pos;
            debug = Manager._CM.GetTexture(Manager._CM.TextureName.debug);
            size = new Vector2(40, 40);
            init();
        }

        public void Update(Vector2 pos, Vector2 size, float ang)
        {
            angle = ang;
            position = pos;
            this.size = size;
            init();
        }

        public static Vector2 rotate(Vector2 vec, float angle)
        {
            return new Vector2(vec.X * (float)Math.Cos(angle) + vec.Y * -(float)Math.Sin(angle), vec.X * (float)Math.Sin(angle) + vec.Y * (float)Math.Cos(angle));
        }

        void init()
        {
            float degree = MathHelper.ToDegrees(angle);
            Vector2 offset = Vector2.Zero;

            int i = 0;

            i = (int)MathHelper.ToDegrees(angle) / 90;

            bounds[i % 4] = position - offset;
            bounds[(i + 1) % 4] = position + rotate(new Vector2(size.X, 0), angle) - offset;
            bounds[(i + 3) % 4] = position + rotate(new Vector2(0, size.Y), angle) - offset;
            bounds[(i + 2) % 4] = position + rotate(new Vector2(size.X, size.Y), angle) - offset;

            int x = (int)Math.Min(Math.Min(Math.Min(bounds[0].X, bounds[1].X), bounds[2].X), bounds[3].X);
            int y = (int)Math.Min(Math.Min(Math.Min(bounds[0].Y, bounds[1].Y), bounds[2].Y), bounds[3].Y);

            Vector2 loc = new Vector2(x, y);
            Vector2 sizeb = new Vector2((int)Math.Max(Math.Max(Math.Max(bounds[0].X, bounds[1].X), bounds[2].X), bounds[3].X),
                (int)Math.Max(Math.Max(Math.Max(bounds[0].Y, bounds[1].Y), bounds[2].Y), bounds[3].Y));

            Point sizeReal = (sizeb - loc).ToPoint();

            rect = new Rectangle(loc.ToPoint(), sizeReal);

            triangles[0] = new Triangle(new[] { bounds[0], bounds[1], rect.Location.ToVector2() + new Vector2(rect.Size.X, 0) });
            triangles[1] = new Triangle(new[] { bounds[1], bounds[2], rect.Location.ToVector2() + new Vector2(rect.Size.X, rect.Size.Y) });
            triangles[2] = new Triangle(new[] { bounds[2], bounds[3], rect.Location.ToVector2() + new Vector2(0, rect.Size.Y) });
            triangles[3] = new Triangle(new[] { bounds[3], bounds[0], rect.Location.ToVector2() + new Vector2(0, 0) });

            triangles[4] = new Triangle(new[] { bounds[0], bounds[1], bounds[2] });
            triangles[5] = new Triangle(new[] { bounds[0], bounds[3], bounds[2] });


        }

        public void Update(GameTime gTime)
        {

        }

        public bool intersect(Vector2[] edge, Triangle[] triangles, Rectangle rect)
        {

            foreach (Vector2 v in edge)
            {
                if (!rect.Intersects(new Rectangle(v.ToPoint(), new Point(1, 1))))
                {
                    continue;
                }
                if (triangles[4].intersect(v) || triangles[5].intersect(v))
                    return true;
            }

            int k = 0;
            foreach (Vector2 v in edge)
            {
                if (!rect.Intersects(new Rectangle(v.ToPoint(), new Point(1, 1))))
                {
                    continue;
                }


                for (int i = 0; i < 4; ++i)
                {
                    if (triangles[i].intersect(v))
                        ++k;
                }

                if (k >= 2)
                    return true;
            }
            return false;
        }

        public bool intersect(MyRectangle other)
        {
            if (!rect.Intersects(other.rect))
                return false;
            else
            {
                return intersect(other.bounds, triangles, rect) || intersect(bounds, other.triangles, other.rect);
            }

        }

        public bool intersect(Vector2 v)
        {
            if (triangles[4].intersect(v) || triangles[5].intersect(v))
                return true;

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, texture.Bounds, Color.White, angle, Vector2.Zero, 1, SpriteEffects.None, 0);

            /*
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 3; ++j)
                    spriteBatch.Draw(debug, triangles[i].bounds[j], Color.White);
            */
            
            for (int i = 0; i < 4; ++i)
                spriteBatch.Draw(debug, bounds[i], Color.White);
                


        }
    }
}
