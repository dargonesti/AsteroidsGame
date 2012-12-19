using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsPrototype.GameObjects
{
    static class SpriteManager
    {
        public static Texture2D DefaultSprite;
        public static float scaleRateShip = 1;
        public static float scaleRateAsteroid = 1;
        public static float scaleRateSmallAsteroid = 1;
        public static Texture2D ShipMainSprite;
        public static Texture2D LargeAsteroid;
        public static Texture2D SmallAsteroid;
        public static Texture2D Laser;
        public static float scaleLaser = 1;

        public static void LoadSpritesFromContent(ContentManager content){
            DefaultSprite = content.Load<Texture2D>("default sprite");
            ShipMainSprite = content.Load<Texture2D>("ship");
            LargeAsteroid = content.Load<Texture2D>("large asteroid");
            SmallAsteroid = content.Load<Texture2D>("small asteroid");
            Laser = content.Load<Texture2D>("laser");

            SpriteManager.scaleRateShip = 70f / SpriteManager.ShipMainSprite.Width;
            SpriteManager.scaleRateAsteroid = 120f / SpriteManager.LargeAsteroid.Width;
            SpriteManager.scaleRateSmallAsteroid = 60f / SpriteManager.SmallAsteroid.Width;
            SpriteManager.scaleLaser = 30f / SpriteManager.SmallAsteroid.Width;
         }
    }
}
