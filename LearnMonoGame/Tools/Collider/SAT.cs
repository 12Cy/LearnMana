using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.Tools.Collider
{
    public static class SAT
    {
        public static bool AreColliding(Rectangle r1, Rectangle r2)
        {
            /* Using clockwise labelling
            * 
            *       A * * * * B
            *       *         *
            *       *         *
            *       *         *
            *       D * * * * C
            *   
            *                
            * 
            */
            Vector2 r1A = new Vector2(r1.X, r1.Y);
            Vector2 r1B = new Vector2(r1.X + r1.Width, r1.Y);
            Vector2 r1C = new Vector2(r1.X + r1.Width, r1.Y + r1.Height);
            Vector2 r1D = new Vector2(r1.X, r1.Y + r1.Height);

            Vector2 r1AB = new Vector2(r1B.X - r1A.X, r1B.Y - r1A.Y);
            Vector2 r1BC = new Vector2(r1C.X - r1B.X, r1C.Y - r1B.Y);
            Vector2 r1CD = new Vector2(r1D.X - r1C.X, r1D.Y - r1C.Y);
            Vector2 r1DA = new Vector2(r1A.X - r1D.X, r1A.Y - r1D.Y);

            Vector2 r1AB_normalized = VerticalVector(r1AB);
            Vector2 r1BC_normalized = VerticalVector(r1BC);
            Vector2 r1CD_normalized = VerticalVector(r1CD);
            Vector2 r1DA_normalized = VerticalVector(r1DA);

            r1AB_normalized.Normalize();
            r1BC_normalized.Normalize();
            r1CD_normalized.Normalize();
            r1DA_normalized.Normalize();
            //Console.WriteLine(r1A);

            Vector2[] r1Points = { r1A, r1B, r1C, r1D };
            Vector2[] axes1 = { r1AB_normalized, r1BC_normalized, r1CD_normalized, r1DA_normalized };

            //Second Rectangle

            Vector2 r2A = new Vector2(r2.X, r2.Y);
            Vector2 r2B = new Vector2(r2.X + r2.Width, r2.Y);
            Vector2 r2C = new Vector2(r2.X + r2.Width, r2.Y + r2.Height);
            Vector2 r2D = new Vector2(r2.X, r2.Y + r2.Height);

            Vector2 r2AB = new Vector2(r2B.X - r2A.X, r2B.Y - r2A.Y);
            Vector2 r2BC = new Vector2(r2C.X - r2B.X, r2C.Y - r2B.Y);
            Vector2 r2CD = new Vector2(r2D.X - r2C.X, r2D.Y - r2C.Y);
            Vector2 r2DA = new Vector2(r2A.X - r2D.X, r2A.Y - r2D.Y);

            Vector2 r2AB_normalized = VerticalVector(r2AB);
            Vector2 r2BC_normalized = VerticalVector(r2BC);
            Vector2 r2CD_normalized = VerticalVector(r2CD);
            Vector2 r2DA_normalized = VerticalVector(r2DA);

            r2AB_normalized.Normalize();
            r2BC_normalized.Normalize();
            r2CD_normalized.Normalize();
            r2DA_normalized.Normalize();

            Vector2[] r2Points = { r2A, r2B, r2C, r2D };
            Vector2[] axes2 = { r2AB_normalized, r2BC_normalized, r2CD_normalized, r2DA_normalized };

            float r1min = float.NegativeInfinity;
            float r1Max = float.PositiveInfinity;

            float r2min = float.NegativeInfinity;
            float r2Max = float.PositiveInfinity;

            for (int i = 0; i < axes1.Length; i++) //wir betrachten nur rechtecke
            {

                r1min = Vector2.Dot(r1Points[i], axes1[i]);
                r1Max = r1min;
                r2min = Vector2.Dot(r2Points[i], axes1[i]);
                r2Max = r2min;

                for (int p = 1; p < axes1.Length; p++)
                {
                    float value = Vector2.Dot(r1Points[p], axes1[i]);
                    float value2 = Vector2.Dot(r2Points[p], axes1[i]);

                    if (value > r1Max)
                        r1Max = value;

                    if (value < r1min)
                        r1min = value;

                    if (value2 > r2Max)
                        r2Max = value2;
                    if (value2 < r2min)
                        r2min = value2;
                }
                if (!NennMichBitteNochUm(r1min, r1Max, r2min, r2Max))
                    return false;

            }
            return true;
        }

        private static bool NennMichBitteNochUm(float r1min, float r1Max, float r2min, float r2Max)
        {
            if (r1min > r2Max || r1Max < r2min)
                return false;
            else
                return true;
        }

        static Vector2 VerticalVector(Vector2 v)
        {
            return new Vector2(v.Y, -v.X); //äußeren vektor
        }



    }
}
