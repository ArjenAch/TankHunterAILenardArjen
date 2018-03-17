﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankHunterAiLenardArjen.Support
{
    public static class GlobalVars
    {
        public static int cellSize = 20;
        public static bool debug = true;
        public static bool playerDebug = true;
        public static double worldWidth = 400;
        public static double worldHeight = 400;

        public static Texture2D DefaultTileTexture;
        public static Texture2D PlayerTexture;
    }
}
