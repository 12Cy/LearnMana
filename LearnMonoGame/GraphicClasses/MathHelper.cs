using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMonoGame.GraphicClasses
{
    public class MathHelper
    {
        /// <summary>
        /// Returns the smaller Angle between 2 Vectors in Degrees. 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static double AngleBetweenRad(Vector2 vec1, Vector2 vec2)
        {
            // ((vec1.X * vec2.Y - vec1.Y * vec2.X > 0)? -1 : 1) 
            // -> Figure out the sign for the Angle, based on if the angle is rotated clockwise from the first vector to the second one
            // Math.Acos(Vector2.Dot(vec1, vec2) / (vec1.Length() * vec2.Length()))
            // -> Compute the angle between the 2 Vectors and afterwards normalizing it
            return (((vec1.X * vec2.Y - vec1.Y * vec2.X) > 0) ? -1 : 1) * Math.Acos(Vector2.Dot(vec1, vec2) / (vec1.Length() * vec2.Length()));
        }

        /// <summary>
        /// Return the smaller Angle between 2 Vectors in Radians
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static double AngleBetween(Vector2 vec1, Vector2 vec2)
        {
            return RadianToDegree(AngleBetweenRad(vec1, vec2));
        }

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180;
        }

        public static double RadianToDegree(double angle)
        {
            return 180 * angle / Math.PI;
        }
    }
}
