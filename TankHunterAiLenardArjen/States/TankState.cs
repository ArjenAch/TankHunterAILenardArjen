using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.States
{
    public interface ITankState
    {
        // Called directly after a state has changed
        void Enter(Vehicle tank);

        // Called in the update function
        Vector Execute(Vehicle tank);

        // Called before ChangeState is called
        void Exit(Vehicle tank);
    }
}
