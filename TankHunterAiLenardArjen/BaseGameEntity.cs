using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public abstract class BaseGameEntity
    {
        public abstract void Update(int timeElapsed);
        public abstract void Render();
        public int ID()
        {
            throw new NotImplementedException();
        }
        public Vector Position()
        {
            throw new NotImplementedException();
        }
        public float Scale()
        {
            throw new NotImplementedException();
        }
        public float Bradius()
        {
            throw new NotImplementedException();
        }

    }
}
