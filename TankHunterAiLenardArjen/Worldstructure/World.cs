﻿using System;
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
        private Player player;
        public CellSpacePartition GridLogic { get; }
        public Texture2D TileTexture { get; set; }
        public Texture2D TileDebugNeighborTexture { get; set; }
        public Texture2D TileDebugCenterTexture { get; set; }

        public World(int levelWidth, int levelHeight, Player player)
        {
            GridLogic = new CellSpacePartition(400, 400, GlobalVars.cellSize);
            this.player = player;
            AddTank();
        }

        private void AddTank()
        {
            //Vehicle tank = new Vehicle(this);
            //tank.steering.SetTarget(player.position);
            //tank.steering.Seek = true;
        }

        public void Update(int timeElapsed)
        {
            player.Update(timeElapsed);
            GridLogic.UpdateEntity(player);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch, graphics);
            if (GlobalVars.debug == true)
            {
                GridLogic.CalculateNeighborCells(GridLogic.Grid[50], 40);

                foreach (Cell cell in GridLogic.Neighbors)
                {
                    cell.Render(TileDebugNeighborTexture, spriteBatch, graphics);
                }
                GridLogic.Grid[50].Render(TileDebugCenterTexture, spriteBatch, graphics);
            }
            player.Render(spriteBatch);
        }
    }
}
