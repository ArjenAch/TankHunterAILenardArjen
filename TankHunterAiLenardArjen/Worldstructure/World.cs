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

        public World(int levelWidth, int levelHeight, Player player)
        {
            GridLogic = new CellSpacePartition(600, 600, GlobalVars.cellSize);
            this.player = player;
        }

        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
            player.Render(spriteBatch);
        }

        public void Update(int timeElapsed)
        {
            player.Update(timeElapsed);
            GridLogic.UpdateEntity(player);
        }
    }
}
