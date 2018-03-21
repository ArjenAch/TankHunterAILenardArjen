﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen.BehaviourLogic;
using TankHunterAiLenardArjen.Support;

namespace TankHunterAiLenardArjen.States
{
    public class TankPatrol : ITankState
    {
        private WanderBehaviour wanderBehaviour;
        private Vector steeringForce;
        private int TimeElapsed;

        public TankPatrol()
        {
            wanderBehaviour = new WanderBehaviour(1.2, 2, 40);
            steeringForce = new Vector(2, 2);
            TimeElapsed = GlobalVars.BehaviourDelay;
        }

        public void Enter(Vehicle tank)
        {
            // IDEA: Tank is in a good mood
        }

        public Vector Execute(Vehicle vehicle, int timeElapsed)
        {
            return Execute((Tank)vehicle, timeElapsed);
        }

        public Vector Execute(Tank tank, int timeElapsed)
        {
            TimeElapsed += timeElapsed;
            if (tank.PlayerInAttackZone())
            {
                tank.ChangeState(new TankAttackPlayer());
            }
            else if (tank.PlayerInDangerZone())
            {
                tank.ChangeState(new TankCreateDistanceBetweenPlayer());
            }
            else
            {
                if (TimeElapsed >= GlobalVars.BehaviourDelay)
                {
                    steeringForce = wanderBehaviour.Execute(tank);
                    TimeElapsed = 0;
                }
            }
            return steeringForce;
        }

        public void Exit(Vehicle tank)
        {

        }

        public Color GetColor()
        {
            return Color.Blue;
        }
    }
}
