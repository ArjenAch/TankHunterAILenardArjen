using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    public class MovePlayerDownCommand : Command
    {
        Player player;

        public MovePlayerDownCommand(Player player)
        {
            this.player = player;
        }

        public void Execute(int timeElapsed)
        {
            player.MoveDown(timeElapsed);
        }
    }
}
