using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    public class MovePlayerRightCommand : Command
    {
        Player player;

        public MovePlayerRightCommand(Player player)
        {
            this.player = player;
        }

        public void Execute(int timeElapsed)
        {
            player.MoveRight(timeElapsed);
        }
    }
}
