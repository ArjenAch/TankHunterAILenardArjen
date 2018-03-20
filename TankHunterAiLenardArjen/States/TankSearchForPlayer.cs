using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankHunterAiLenardArjen.States
{
    public class TankSearchForPlayer : ITankState
    {
        public void Enter(Vehicle tank)
        {
            // IDEA: show question mark on tank
        }

        public Vector Execute(Vehicle vehicle, int timeElapsed)
        {
            return Execute((Tank)vehicle, timeElapsed);
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            Vector steeringForce = new Vector(0, 0);
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerNotSeenAtLastLocation())
            {
                tank.ChangeState(new TankPatrol());
            }
            else
            {
                // tank.A* to last seen location
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {
            // IDEA: show something that player is found or not on tank
        }

        public Color GetColor()
        {
            return Color.Yellow;
        }
    }
}
