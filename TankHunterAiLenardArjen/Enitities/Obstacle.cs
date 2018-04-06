using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.Enitities
{
    public class Obstacle : BaseGameEntity
    {
        private Vector2 origin;
        private Texture2D _texture;
        private Rectangle destinationSize;
        private World gameWorld;
        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                origin.X = 0;
                origin.Y = 0;
            }
        }

        public Obstacle(Vector position, World world) : base(position)
        {
            destinationSize = new Rectangle((int)Position.X, (int)Position.Y, (int)(GlobalVars.cellSize), (int)(GlobalVars.cellSize));
            gameWorld = world;
            //Add obstacle to grid
            gameWorld.GridLogic.UpdateEntity(this);
            Bradius = GlobalVars.cellSize;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, destinationSize, null, Color.White, 0, origin, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public override void Update(int timeElapsed)
        {
            throw new NotImplementedException();
        }
    }
}
