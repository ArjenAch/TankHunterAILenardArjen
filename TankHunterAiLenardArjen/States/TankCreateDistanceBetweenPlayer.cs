using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.States
{
    public class TankCreateDistanceBetweenPlayer : ITankState
    {
        private FleeBehaviour flee;
        private ObstacleAvoidanceBehaviour avoid;

        public TankCreateDistanceBetweenPlayer ()
        {
            flee = new FleeBehaviour();
            avoid = new ObstacleAvoidanceBehaviour();
        }

        public void Enter(Tank tank)
        {
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
                tank.gameWorld.GridLogic.CalculateNeighborsEntities(tank, Tank.TankIsInDangerDistance); 
                foreach (MovingEntity entity in tank.gameWorld.GridLogic.EntitiesInRange)
                {
                    if(entity is Player)
                    {
                        steeringForce += flee.Execute(tank, entity.Position);
                        steeringForce += avoid.Execute(tank) * GlobalVars.ObstacleAvoidanceWeight;
                    }
                }
            }

            return steeringForce;
        }

        public void Exit(Tank tank)
        {

        }

        public Color GetColor()
        {
            return Color.Green;
        }
    }
}
