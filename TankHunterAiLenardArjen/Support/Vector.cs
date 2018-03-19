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

        public Vector Normalize() //http://www.fundza.com/vectors/normalize/
        {
            float length = Length();
            X = X / length;
            Y = Y / length;
            return this;
        }

        public Vector Perp() //https://gamedev.stackexchange.com/questions/70075/how-can-i-find-the-perpendicular-to-a-2d-vector
        {
            Vector v = new Vector(Y, -X);
            return v;
        }

        public float LengthSq()
        {
            return (X * X + Y * Y);
        }

        public float Length()
        {
            float length = (float)Math.Sqrt((Double)(X * X) + (Y * Y));
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

        #region operator overloaders TODO revisit
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

        public static Vector operator *(Vector vector, double t)
        {
            vector.X = vector.X * (float)t;
            vector.Y = vector.Y * (float)t;
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

        public static Vector operator -(Vector vector, int t)
        {
            vector.X = vector.X - t;
            vector.Y = vector.Y - t;
            return vector;
        }

        public static Vector operator /(Vector vector, float t)
        {
            if(t != 0)
            {
                vector.X = vector.X / t;
                vector.Y = vector.Y / t;
            }
            return vector;
        }


        #endregion


    }
}
