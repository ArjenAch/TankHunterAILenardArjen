using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen
{
    public class World
    {
        private Player player;
        public CellSpacePartition GridLogic { get; }

        public World(int levelWidth, int levelHeight)
        {
            GridLogic = new CellSpacePartition(300, 300, 50);
            AddTank();
        }

        private void AddTank()
        {
            //Vehicle tank = new Vehicle(this);
            //tank.steering.SetTarget(player.position);
            //tank.steering.Seek = true;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Color[] data;
            Texture2D rectTex;
            //TODO FIX CORNERS maybe break loop?
            GridLogic.CalculateNeighborCells(GridLogic.Grid[14], 100);

            foreach (Cell v in GridLogic.Neighbors)
            {
                data = new Color[50* 50];
                rectTex = new Texture2D(graphics, 50, 50);
                for (int i = 0; i < data.Length; ++i)
                    data[i] = Color.White;

                rectTex.SetData(data);
                var position = v.Position.ToVector2();
                spriteBatch.Begin();
                spriteBatch.Draw(rectTex, position, Color.White);
               // spriteBatch.DrawString(null, v.ID.ToString(), v.Position.ToVector2(), Color.Black);
                spriteBatch.End();

            }

            data = new Color[50 * 50];
            rectTex = new Texture2D(graphics, 50, 50);
            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.White;

            rectTex.SetData(data);
            var pos= (GridLogic.Grid[14].Position.ToVector2());
            spriteBatch.Begin();
            spriteBatch.Draw(rectTex, pos, Color.Red);
            spriteBatch.End();
        }


    }
}
