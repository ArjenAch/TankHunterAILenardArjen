using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen
{
    public class SteeringBehavioursController
    {
        public bool Seek { get; set; }
        private Vector target;


        public Vector Calculate(Vehicle vehicle)
        {
            Vector steeringForce = new Vector(0, 0);

            if (Seek == true)
            {
                steeringForce += SeekTarget(vehicle);
            }

            return steeringForce;
        }

        public void SetTarget(Vector target)
        {
            this.target = target;
        }

        #region behaviours
        public Vector SeekTarget(Vehicle vehicle)
        {
            Vector desiredVelocity = Vector.Normalize((target - vehicle.Position) * vehicle.MaxSpeed);

            return (desiredVelocity - vehicle.Velocity);
        }

        #endregion
    }
}
