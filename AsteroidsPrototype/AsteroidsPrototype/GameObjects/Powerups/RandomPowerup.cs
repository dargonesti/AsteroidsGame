using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Powerups
{
   /// <summary>
   /// Classe de powerUp ayant un effet random
   /// </summary>
    class RandomPowerup : Powerup
    {
        private Random random;
        private Powerup powerup;

        public RandomPowerup()
            : base()
        {
            random = new Random();
            GenerateRandomPowerUp();
            this.Sprite = SpriteManager.RandomPowerup;
            this._canRespawn = false;
        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {
            powerup.ApplyTo(gameManager);
        }

        public override void ApplyTo(Ship ship)
        {
            powerup.ApplyTo(ship);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //GenerateRandomPowerUp();
        }

        private void GenerateRandomPowerUp()
        {
            int value = random.Next(1, 6);
            switch (value)
            {
                case 1:
                    powerup = new ShotgunPowerup();
                    break;
                case 2:
                    powerup = new FrostOrbPowerup();
                    break;
                case 3:
                    powerup = new TimeFreezePowerup();
                    break;
                case 4:
                    powerup = new WeaponsLockPowerup();
                    break;
                case 5:
                    powerup = new ShieldPowerup();
                    break;
            }
            this._isAppliedToWorld = powerup.IsAppliedToWorld;
        }
    }
}
