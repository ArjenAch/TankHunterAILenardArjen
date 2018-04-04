using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    public class MovePlayerLeftCommand : Command
    {
        Player player;

        public MovePlayerLeftCommand(Player player)
        {
            this.player = player;
        }

        public void Execute(int timeElapsed)
        {
            player.MoveLeft(timeElapsed);
        }
    }
}