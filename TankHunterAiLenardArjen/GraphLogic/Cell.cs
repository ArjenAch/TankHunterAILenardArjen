using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankHunterAiLenardArjen.Support;
using Priority_Queue;

namespace TankHunterAiLenardArjen.Worldstructure
{
    public class Cell : FastPriorityQueueNode// Chapter 3 pg 127
    {
        public int ID { get; }
        public Vector Position { get; set; } // Middle of the cell
        public List<BaseGameEntity> Members { get; set; }
        public List<Edge> Adjecent { get; set; }
        public bool Visited { get; set; }
        public Color TileColor { get; set; }
        public bool ContainstObstacle { get; set; }
        private Rectangle destinationSize;
        private Rectangle graphLine;
        private float rotation;
        private Vector2 origin;

        public Cell(Vector pos, int id)
        {
            Adjecent = new List<Edge>();
            Members = new List<BaseGameEntity>();
            Position = pos;
            ID = id;
            destinationSize = new Rectangle((int)Position.X - (GlobalVars.cellSize / 2), (int)Position.Y - (GlobalVars.cellSize / 2), GlobalVars.cellSize, GlobalVars.cellSize);
            graphLine = new Rectangle((int)Position.X, (int)Position.Y, GlobalVars.cellSize / 2, 1);
            TileColor = Color.White;
            rotation = 0;
            origin = new Vector2(1, 1);
            Priority = 1;
        }

        private void ChangeOrigin(float rotation)
        {

        }

        private void Render(Texture2D texture, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationSize, null, color);

            //If debug is enabled draw the graph
            if (GlobalVars.debug == true)
            {
               // spriteBatch.Draw(GlobalVars.GraphTexture, graphPoint, TileColor);
                foreach (Edge adj in Adjecent)
                {
                    //TODO dirty fix !(ID > 300 && adj.Cell2.Position.X == 20 || adj.Cell2.Position.Y == 20)
                    //  if (!(ID > 50 && adj.Cell2.Position.X == 20 && adj.Cell2.Position.Y == 20))
                    
                        rotation = (float)Math.Atan2(Position.Y - adj.Cell2.Position.Y, Position.X - adj.Cell2.Position.X);
                        ChangeOrigin(rotation);
                        spriteBatch.Draw(GlobalVars.GraphTexture, graphLine, null, Color.OrangeRed, rotation, origin, SpriteEffects.None, 0);
                }
            }
            spriteBatch.End();
        }

        public void Render(Texture2D texture, SpriteBatch spriteBatch)
        {
            Render(texture, spriteBatch, TileColor);

        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Support.GlobalVars.DefaultTileTexture, destinationSize, TileColor);
            spriteBatch.End();
        }
    }
}
