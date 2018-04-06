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

// Weather Api
using DarkSkyApi;
using DarkSkyApi.Models;

namespace TankHunterAiLenardArjen
{
    public class World
    {
        public CellSpacePartition GridLogic { get; }
        public Texture2D TileTexture { get; set; }
        public Forecast Weather { get; set; }

        public World(int levelWidth, int levelHeight)
        {
            //GetWeather();
            GridLogic = new CellSpacePartition(levelWidth, levelWidth, GlobalVars.cellSize);
        }

        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
        }

        public void Update(int timeElapsed)
        {
        }

        public async void GetWeather()
        {
            var client = new DarkSkyService(GlobalVars.DarkSkyWeatherKey);

            // 52.499836, 6.079915 are Windesheim Coordinates
            Weather = await client.GetWeatherDataAsync(52.499836, 6.079915);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GridLogic.RenderAllCells(TileTexture, spriteBatch);
        }
    }
}
