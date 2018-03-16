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
        void Enter(Tank tank);

        // Called in the update function
        Vector Execute(Tank tank);

        // Called before ChangeState is called
        void Exit(Tank tank);
    }
}
