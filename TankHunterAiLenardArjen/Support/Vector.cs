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

        public Vector()
        {

        }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector(Vector vector)
        {
            X = vector.X;
            Y = vector.Y;
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
            else  if (X < -maxSpeed)
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

        // Function that keeps the vectors in their cage
        public void WrapAround(double maxX, double maxY)
        {
            if (X > maxX) { X = 0.0f; }
            if (X < 0) { X = (float)maxX; }
            if (Y < 0) { Y = (float)maxY; }
            if (Y > maxY) { Y = 0.0f; }
        }

        #region operator overloaders 
        public static Vector operator *(Vector vector, int t)
        {
            Vector result = new Vector();
            result.X = vector.X * t;
            result.Y = vector.Y * t;
            return result;
        }

        public static Vector operator *(Vector vector, float t)
        {
            Vector result = new Vector();
            result.X = vector.X * t;
            result.Y = vector.Y * t;
            return result;
        }

        public static Vector operator *(Vector vector, double t)
        {
            Vector result = new Vector();
            result.X = vector.X * (float)t;
            result.Y = vector.Y * (float)t;
            return result;
        }

        public static Vector operator +(Vector vector, Vector vector2)
        {
            Vector result = new Vector();
            result.X = vector.X + vector2.X;
            result.Y = vector.Y + vector2.Y;
            return result;
        }

        public static Vector operator -(Vector vector, Vector vector2)
        {
            Vector result = new Vector();
            result.X = vector.X - vector2.X;
            result.Y = vector.Y - vector2.Y;
            return result;
        }

        public static Vector operator -(Vector vector, int t)
        {
            Vector result = new Vector();
            result.X = vector.X - t;
            result.Y = vector.Y - t;
            return result;
        }

        public static Vector operator -(Vector vector, float t)
        {
            Vector result = new Vector();
            result.X = vector.X - t;
            result.Y = vector.Y - t;
            return result;
        }

        public static Vector operator /(Vector vector, float t)
        {
            Vector result = new Vector();
            if (t != 0)
            {
                result.X = vector.X / t;
                result.Y = vector.Y / t;
            }
            return result;
        }


        #endregion


    }
}
