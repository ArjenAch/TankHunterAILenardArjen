using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 ToVector2()
        {
            Vector2 vector = new Vector2(X, Y);
            return vector;
        }

        public Vector ToVector(Vector2 v)
        {
            Vector vector = new Vector(v.X, v.Y);
            return vector;
        }

        internal static Vector Normalize(Vector velocity) //http://www.fundza.com/vectors/normalize/
        {
            float length = velocity.LengthSq();
            velocity.X = velocity.X / length;
            velocity.Y = velocity.Y / length;
            return velocity;
        }

        //public static Vector operator *()
        //{

        //}

        public Vector Perp() //https://gamedev.stackexchange.com/questions/70075/how-can-i-find-the-perpendicular-to-a-2d-vector
        {
            Vector v = new Vector(Y, -X);
            return v;
        }

        public float LengthSq()
        {
            float length = (float)Math.Sqrt((X * X) + (Y * Y));
            return length;
        }

        public float Dot(Vector v2)
        {
            return X * v2.X + Y * v2.Y;
        }

        public void Truncate(float maxSpeed)
        {
            if (X > maxSpeed)
            {
                X = maxSpeed;
            }
            else if (X < -maxSpeed)
            {
                X = -maxSpeed;
            }

            if (Y > maxSpeed)
            {
                Y = maxSpeed;
            }
            else if (Y < -maxSpeed)
            {
                Y = -maxSpeed;
            }
        }

        #region operator overloaders
        public static Vector operator *(Vector vector, int t)
        {
            vector.X = vector.X * t;
            vector.Y = vector.Y * t;
            return vector;
        }

        public static Vector operator *(Vector vector, float t)
        {
            vector.X = vector.X * t;
            vector.Y = vector.Y * t;
            return vector;
        }

        public static Vector operator +(Vector vector, Vector vector2)
        {
            vector.X = vector.X + vector2.X;
            vector.Y = vector.Y + vector2.Y;
            return vector;
        }

        public static Vector operator -(Vector vector, Vector vector2)
        {
            vector.X = vector.X - vector2.X;
            vector.Y = vector.Y - vector2.Y;
            return vector;
        }

        public static Vector operator -(Vector vector, int x)
        {
            vector.X = vector.X - x;
            vector.Y = vector.Y - x;
            return vector;
        }

        public static Vector operator /(Vector vector, float f)
        {
            vector.X = vector.X / f;
            vector.Y = vector.Y / f;
            return vector;
        }
        #endregion


    }
}
