using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Support
{
    public static class HelpMethods
    {

        public static Vector ToWorldSpace(Vector targetLocal, Vector heading, Vector side, Vector position)
        {
            //create a transformation matrix
            Matrix matTransform = new Matrix();

            ////rotate
            matTransform = matTransform * Matrix.Rotate(heading, side);

            ////and translate
            matTransform = matTransform * Matrix.Translate(position, matTransform);

            ////now transform the vertices
            return Matrix.TransformVector(matTransform, targetLocal);
        }
    }
}
