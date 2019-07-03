using System;
using System.Collections.Generic;
using TankHunterAiLenardArjen.BehaviourLogic;

namespace TankHunterAiLenardArjen.Enitities
{
    public class CohesionBehaviour
    {
        private List<MovingEntity> vehicles;
        private SeekBehaviour seek;

        public CohesionBehaviour ()
        {
            seek = new SeekBehaviour();
        }

        public Vector Execute(Vehicle vehicle)
        {
            //first find the center of mass of all the agents
            Vector CenterOfMass = new Vector(0, 0);
            Vector SteeringForce = new Vector(0,0);

            int NeighborCount = 0;

            vehicle.gameWorld.GridLogic.CalculateNeighborsEntities(vehicle, vehicle.Radius);
            vehicles = vehicle.gameWorld.GridLogic.EntitiesInRange;

            //iterate through the neighbors and sum up all the position vectors
            for(int i = 0; i < vehicles.Count; i++)
            {
                //make sure *this* agent isn't included in the calculations and that
                //the agent being examined is close enough and of the same type
                if (vehicles[i] != vehicle && vehicles[i].GetType().Equals(vehicle.GetType()))
                {
                    CenterOfMass += vehicles[i].Position;

                    ++NeighborCount;
                }
            }

            if (NeighborCount > 0)
            {
                //the center of mass is the average of the sum of positions
                CenterOfMass /= NeighborCount;

                //now seek towards that position
                SteeringForce = seek.Execute(vehicle, CenterOfMass);

                //the magnitude of cohesion is usually much larger than separation or
                //allignment so it usually helps to normalize it.
                return SteeringForce.Normalize();
            }

            return SteeringForce;
        }
    }
}