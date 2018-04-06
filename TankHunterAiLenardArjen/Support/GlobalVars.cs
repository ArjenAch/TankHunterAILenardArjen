using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Support
{
    public static class GlobalVars
    {
        public static int cellSize = 40;
        public static bool debug = true;
        public static bool playerDebug = true;
        public static double worldWidth = 400;
        public static double worldHeight = 400;

        public static Texture2D DefaultTileTexture;
        public static Texture2D PlayerTexture;

        //Tank
        // Delay before the new steeringforce is calculated (wanderbehaviour)
        public static int BehaviourDelay = 0;

        //Flocking plane
        public static double SeperationWeight = 3.0;
        public static double CohesionWeight = 6.8;
        public static double AllignmentWeight = 5.5;

        public static string DarkSkyWeatherKey= "def0f7cc12d41bb9a782c25894b2068d";
    }
}
