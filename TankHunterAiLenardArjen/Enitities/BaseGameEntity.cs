using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankHunterAiLenardArjen.Worldstructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankHunterAiLenardArjen
{
    public abstract class BaseGameEntity //Chapter 3 pg 89
    {
        public abstract void Update(int timeElapsed);
        public abstract void Render(SpriteBatch spriteBatch);
        public int ID { get; }
        public Vector Position { get; set; }
        public float Scale { get; }
        public float Bradius { get; }
        public Cell InCell { get; set; }
        

        public BaseGameEntity(Vector position)
        {
            Position = position;
            InCell = new Cell(new Vector(Position.X, Position.Y), 0);
        }

    }
}
