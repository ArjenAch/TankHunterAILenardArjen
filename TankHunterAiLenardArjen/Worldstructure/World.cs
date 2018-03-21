using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankHunterAiLenardArjen.Support;
using TankHunterAiLenardArjen.Worldstructure;

namespace TankHunterAiLenardArjen
{
    public class World
    {
        public CellSpacePartition GridLogic { get; }
        public Texture2D TileTexture { get; set; }

        public World(int levelWidth, int levelHeight)
        {
            GridLogic = new CellSpacePartition(levelWidth, levelWidth, GlobalVars.cellSize);
        }

        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
        }

        public void Update(int timeElapsed)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
            //if (GlobalVars.debug == true)
            //{
            //    GridLogic.CalculateNeighborCells(GridLogic.Grid[40], 40);

            //    foreach (Cell cell in GridLogic.Neighbors)
            //    {
            //        cell.Render(TileDebugNeighborTexture, spriteBatch, graphics);
            //    }
            //    GridLogic.Grid[40].Render(TileDebugCenterTexture, spriteBatch, graphics);
            //}
        }
    }
}
