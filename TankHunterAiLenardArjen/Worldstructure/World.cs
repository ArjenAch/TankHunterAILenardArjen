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

        public World()
        {
            GridLogic = new CellSpacePartition(GlobalVars.cellSize);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
        }

        public void Update(int timeElapsed)
        {

        }

    }
}
