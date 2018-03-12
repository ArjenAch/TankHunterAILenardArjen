using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    public class MovePlayerUpCommand : Command
    {
        Player player;

        public MovePlayerUpCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            player.MoveUp();
        }
    }
}
