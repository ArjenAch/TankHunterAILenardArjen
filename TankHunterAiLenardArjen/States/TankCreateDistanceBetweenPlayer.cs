using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public class TankCreateDistanceBetweenPlayer : ITankState
    {
        public void Enter(Tank tank)
        {
            // IDEA: tank shows signs of panic
        }

        public void Execute(Tank tank)
        {
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else
            {
                // tank.Flee();
            }
        }

        public void Exit(Tank tank)
        {

        }
    }
}
