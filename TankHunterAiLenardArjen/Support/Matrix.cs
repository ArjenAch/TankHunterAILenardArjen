using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Support
{
    public class Matrix
    {
        private float[,] matrix;
        private const int size = 3;

        public Matrix(
            float m11 = 1, float m12 = 0, float m13 = 0,
            float m21 = 0, float m22 = 1, float m23 = 0,
            float m31 = 0, float m32 = 0, float m33 = 1)
        {

            matrix = new float[,]
            {
                {m11, m12, m13},
                {m21, m22, m23},
                {m31, m32, m33 }

            };
        }

        public static Matrix Rotate(Vector heading, Vector side)
        {
            Matrix mat = new Matrix(
              heading.X, heading.Y,  0,
              side.X,    side.Y,     0,
              0,         0,          1);
            //and multiply

            return mat;
            
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            float[,] m3 = new float[size, size];
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    for (int i = 0; i < size; i++)
                    {
                        m3[r, c] += m1.matrix[r, i] * m2.matrix[i, c];
                    }
                }
            }
            return new Matrix(m3);
        }

        public Matrix(float[,] m)
        {
            this.matrix = m;
        }


        public Matrix(Vector v)
        {
            matrix = new float[,]
            {
                {v.X, 0, 0, 0 },
                {v.Y, 1, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }
            };
        }

        public static Vector ToVector(Matrix m)
        {
            return new Vector(m.matrix[0, 0], m.matrix[1, 0]);
        }

        public static Vector TransformVector(Matrix m, Vector vector)
        {
            float tempX = (m.matrix[0,0] * vector.X) + (m.matrix[0,1] * vector.Y) + (m.matrix[0,2]);

            float tempY = (m.matrix[1, 0] * vector.X) + (m.matrix[1, 1] * vector.Y) + (m.matrix[1, 2]);

            vector.X = tempX;
            vector.Y = tempY;

            return vector;
        }

        public static Matrix Translate(Vector vector, Matrix tmp)
        {
            tmp.matrix[0, 2] = vector.X;
            tmp.matrix[1, 2] = vector.Y;
            return tmp;
        }



    }
}
