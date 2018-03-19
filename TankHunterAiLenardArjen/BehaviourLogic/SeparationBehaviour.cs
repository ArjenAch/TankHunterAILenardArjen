using System;
using System.Collections.Generic;

namespace TankHunterAiLenardArjen.Enitities
{
    public class SeparationBehaviour
    {
        Vector steeringForce;
        private List<MovingEntity> vehicles;

        public SeparationBehaviour()
        {
            steeringForce = new Vector(0, 0);
        }

        public Vector Execute(Vehicle vehicle)
        {
            vehicle.gameWorld.GridLogic.CalculateNeighborsEntities(vehicle, vehicle.Radius);
            vehicles = vehicle.gameWorld.GridLogic.EntitiesInRange;

            //iterate through the neighbors and sum up all the position vectors
            for (int i = 0; i < vehicles.Count; i++)
            {
                //make sure this agent isn't included in the calculations and that
                //the agent being examined is close enough
                if (vehicles[i] != vehicle && vehicles[i].GetType().Equals(vehicle.GetType()))
                {
                    Vector ToAgent = vehicle.Position - vehicles[i].Position;

                    //scale the force inversely proportional to the agents distance  
                    //from its neighbor.
                    steeringForce += ToAgent.Normalize() / ToAgent.Length();
                }


            }

            return steeringForce;
        }
    }
}