using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.States
{
    public class TankCreateDistanceBetweenPlayer : ITankState
    {
        private FleeBehavior Flee { get; set; }


        public void Enter(Vehicle tank)
        {
            Flee = new FleeBehavior();
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
            else if (tank.PlayerIsOutOfSeight())
            {
                tank.ChangeState(new TankPatrol(tank));
            }
            else
            {
                foreach (MovingEntity entity in tank.gameWorld.GridLogic.EntitiesInRange)
                {
                    if(entity is Player)
                    {
                        return Flee.Execute(tank, entity.Position);
                    }
                }
            }

            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {

        }

        public Color GetColor()
        {
            return Color.Green;
        }
    }
}
