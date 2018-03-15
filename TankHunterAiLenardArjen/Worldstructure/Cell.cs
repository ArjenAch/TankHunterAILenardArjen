using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class Cell // Chapter 3 pg 127
    {
        public int ID { get; }
        public Vector Position { get; set; } // Middle of the cell
        public List<BaseGameEntity> Members { get; set; }
        public List<Edge> Adjecent { get; set; }
        public bool Visited { get; set; }
        private Rectangle destinationSize;

        public Cell (Vector pos, int id)
        {
            Adjecent = new List<Edge>();
            Members = new List<BaseGameEntity>();
            Position = pos;
            ID = id;
            destinationSize = new Rectangle((int)Position.X - (GlobalVars.cellSize / 2), (int)Position.Y - (GlobalVars.cellSize / 2), GlobalVars.cellSize, GlobalVars.cellSize);
            //destinationSize = new Rectangle((int)Position.X , (int)Position.Y , GlobalVars.cellSize, GlobalVars.cellSize);
        }

        public void Render(Texture2D texture, SpriteBatch spriteBatch)
        {   
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationSize, null, Color.White);
            spriteBatch.End();
        }
    }
}
