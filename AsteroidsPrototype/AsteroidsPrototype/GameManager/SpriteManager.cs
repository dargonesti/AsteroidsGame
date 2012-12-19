using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsPrototype
{
    static class SpriteManager
    {
        public static Texture2D DefaultSprite;
        public static float scaleRateShip = 1;
        public static float scaleRateSlowBall = 0.1f;
        public static float scaleRateShield = 1;
        public static float scaleRateAsteroid = 1;
        public static float scaleRateSmallAsteroid = 1;
        public static Texture2D ShipMainSprite;
        public static Texture2D LargeAsteroid;
        public static Texture2D SmallAsteroid;
        public static Texture2D Laser;
        public static Texture2D Vide;

        public static Texture2D Shield1;
        public static Texture2D Shield2;

        public static Texture2D IconLaser;
        public static Texture2D IconShotGun;
        // public static Texture2D IconChainLightning;
        public static Texture2D IconOrbre;

        public static Texture2D ShotgunPowerup;
        public static Texture2D FrostOrbPowerup;
        public static Texture2D TimeFreezePowerup;
        public static Texture2D RandomPowerup;
        public static Texture2D WeaponsLockPowerUp;
        public static Texture2D ShieldPowerUp;

        public static Texture2D background;

        public static Texture2D colorBackgroundFaded;

        public static float scaleLaser = 1;

        //Particle
        public static Texture2D particleSimple;

        public static Effect particleEffect;


        public static void LoadSpritesFromContent(ContentManager content)
        {
            colorBackgroundFaded = content.Load<Texture2D>("colorBackground");
            DefaultSprite = content.Load<Texture2D>("default sprite");
            ShipMainSprite = content.Load<Texture2D>("ship");
            LargeAsteroid = content.Load<Texture2D>("large asteroid");
            SmallAsteroid = content.Load<Texture2D>("small asteroid");
            Laser = content.Load<Texture2D>("laser");
            Shield1 = content.Load<Texture2D>("shield1");
            Shield2 = content.Load<Texture2D>("shield2");

            background = content.Load<Texture2D>("background");

            Vide = content.Load<Texture2D>("SpriteVide");
            particleSimple = content.Load<Texture2D>("whiteFade");

            IconLaser = content.Load<Texture2D>("IconLaser");
            IconShotGun = content.Load<Texture2D>("IconShotGun");
            IconOrbre = content.Load<Texture2D>("IconOrbre");

            particleEffect = content.Load<Effect>("ParticleEffect");

            SpriteManager.scaleRateShip = 70f / SpriteManager.ShipMainSprite.Width;
            SpriteManager.scaleRateShield = SpriteManager.scaleRateShip * (SpriteManager.ShipMainSprite.Width / SpriteManager.Shield1.Width) * 1.4f;

            SpriteManager.scaleRateAsteroid = 120f / SpriteManager.LargeAsteroid.Width;
            SpriteManager.scaleRateSmallAsteroid = 60f / SpriteManager.SmallAsteroid.Width;
            SpriteManager.scaleLaser = 30f / SpriteManager.SmallAsteroid.Width;


            ShotgunPowerup = content.Load<Texture2D>("ShotgunPowerup");
            FrostOrbPowerup = content.Load<Texture2D>("FrostOrbPowerup");
            TimeFreezePowerup = content.Load<Texture2D>("TimeFreezePowerup");
            RandomPowerup = content.Load<Texture2D>("RandomPowerup");
            WeaponsLockPowerUp = content.Load<Texture2D>("weaponsLockPowerup");
            ShieldPowerUp = content.Load<Texture2D>("shieldPowerup");
        }
    }
}
