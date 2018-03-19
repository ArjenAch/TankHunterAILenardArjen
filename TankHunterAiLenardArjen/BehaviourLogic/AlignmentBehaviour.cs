using System;
using System.Collections.Generic;

namespace TankHunterAiLenardArjen.Enitities
{
    public class AlignmentBehaviour
    {
        //This will record the average heading of the neighbors
        Vector AverageHeading;
        private List<MovingEntity> vehicles;

        public AlignmentBehaviour()
        {
            AverageHeading = new Vector(0, 0);
        }

        public Vector Execute(Vehicle vehicle)
        {
            //This count the number of vehicles in the neighborhood
            int NeighborCount = 0;

            vehicle.gameWorld.GridLogic.CalculateNeighborsEntities(vehicle, vehicle.Radius);
            vehicles = vehicle.gameWorld.GridLogic.EntitiesInRange;

            //iterate through the neighbors and sum up all the position vectors
            for (int i = 0; i < vehicles.Count; i++)
            {
                //make sure *this* agent isn't included in the calculations and that
                //the agent being examined is close enough and of the same type
                if (vehicles[i] != vehicle && vehicles[i].GetType().Equals(vehicle.GetType()))
                {
                    AverageHeading += vehicles[i].Heading;

                    ++NeighborCount;
                }
            }
      
            //if the neighborhood contained one or more vehicles, average their
            //heading vectors.
            if (NeighborCount > 0)
            {
                AverageHeading /= NeighborCount;

                AverageHeading -= vehicle.Heading;
            }

            return AverageHeading;
        }
    }
}