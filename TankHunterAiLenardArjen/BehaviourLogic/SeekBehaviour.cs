using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.BehaviourLogic
{
    public class SeekBehaviour
    {
        public Vector Execute(Vehicle vehicle, Vector target)
        {
            Vector desiredVelocity = Vector.Normalize((target - vehicle.Position) * vehicle.MaxSpeed);

            return (desiredVelocity - vehicle.Velocity);
        }
    }
}
