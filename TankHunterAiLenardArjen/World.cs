using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankHunterAiLenardArjen
{
    public class World
    {
        public List<Vector> GridPoints { get; }
        private Player player;

        public World(int levelWidth, int levelHeight)
        {
            //currentLevel = level;
            GridPoints = new List<Vector>();
            GenerateGrid(levelWidth, levelHeight);
            AddTank();
        }

        private void GenerateGrid(int levelWidth, int levelHeight)
        {
            for (int i = 0; i < levelWidth; i += 10)
            {
                for (int j = 0; j < levelHeight; j += 10)
                {
                    GridPoints.Add(new Vector(i, j));
                }
            }
        }

        private void AddTank()
        {
            Vehicle tank = new Vehicle(this);
            tank.steering.SetTarget(player.position);
            tank.steering.Seek = true;
        }

        private void AddPlayer()
        {
            player = new Player();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            Color[] data;
            Texture2D rectTex;

            foreach (Vector v in GridPoints)
            {
                data = new Color[2* 2];
                rectTex = new Texture2D(graphics, 2, 2);
                for (int i = 0; i < data.Length; ++i)
                    data[i] = Color.White;

                rectTex.SetData(data);
                var position = v.ToVector2();
                spriteBatch.Begin();
                spriteBatch.Draw(rectTex, position, Color.White);
                spriteBatch.End();

            }
        }


    }
}
