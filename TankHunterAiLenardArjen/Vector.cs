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
        public float X { get; }
        public float Y { get; }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public  Vector2 ToVector2()
        {
            Vector2 vector = new Vector2(X, Y);
            return vector;
        }

        public Vector ToVector(Vector2 v)
        {
            Vector vector = new Vector(v.X, v.Y);
            return vector;
        }
    }
}
