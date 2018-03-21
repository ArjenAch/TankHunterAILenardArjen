using System;
using System.Collections.Generic;

namespace TankHunterAiLenardArjen.Enitities
{
    public class SeparationBehaviour
    {
        
        private List<MovingEntity> vehicles;

        public Vector Execute(Vehicle vehicle)
        {
            Vector steeringForce = new Vector(0, 0);
            vehicle.gameWorld.GridLogic.CalculateNeighborsEntities(vehicle, vehicle.Radius);
            vehicles = vehicle.gameWorld.GridLogic.EntitiesInRange;

            //iterate through the neighbors and sum up all the position vectors
            for (int i = 0; i < vehicles.Count; i++)
            {
                //make sure this agent isn't included in the calculations and that
                //the agent being examined is close enough
                if (vehicles[i] != vehicle && vehicles[i].GetType().Equals(vehicle.GetType()))
                {
                    Vector ToAgent = new Vector(vehicle.Position) - vehicles[i].Position;

                    //scale the force inversely proportional to the agents distance  
                    //from its neighbor.
                    steeringForce += ToAgent.Normalize() / ToAgent.Length();
                }


            }

            return steeringForce;
        }
    }
}