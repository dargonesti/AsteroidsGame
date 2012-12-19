using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsPrototype
{
    static class ViewPortManager
    {
        public static int Width;
        public static int Height;

        public static GraphicsDevice graphicsDevice; 

        public static void Load(Viewport viewport)
        {
            Width = viewport.Width;
            Height = viewport.Height;
        }
    }
}
