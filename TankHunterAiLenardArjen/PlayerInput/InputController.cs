using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    public class InputController
    {
        Player player;
        Command UpArrowKey;
        Command DownArrowKey;
        Command LeftArrowKey;
        Command RightArrowKey;

        public InputController(Player player)
        {
            this.player = player;
            LeftArrowKey = new MovePlayerLeftCommand(this.player);
            RightArrowKey = new MovePlayerRightCommand(this.player);
            UpArrowKey = new MovePlayerUpCommand(this.player);
            DownArrowKey = new MovePlayerDownCommand(this.player);
        }

        public void Update(int timeElapsed)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                UpArrowKey.Execute(timeElapsed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                DownArrowKey.Execute(timeElapsed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                LeftArrowKey.Execute(timeElapsed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                RightArrowKey.Execute(timeElapsed);
            }
        }
    }
}
