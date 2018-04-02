using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.PlayerInput
{
    class MovePlayerToPoint : Command
    {
        Player player;
        int timeElapsed;
        int timeDelay;

        public MovePlayerToPoint(Player player)
        {
            this.player = player;
            timeElapsed = 0;
            timeDelay = 0;
        }

        public void Execute(int timeElapsed)
        {
            this.timeElapsed += timeElapsed;

            if (this.timeElapsed > timeDelay)
            {
                player.MoveToPoint(timeElapsed);
                this.timeElapsed = 0;
                timeDelay = 150;
            }
        }
    }
}
