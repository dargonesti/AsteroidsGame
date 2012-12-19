using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Factory de OowerUp
    /// </summary>
    public static class PowerupFactory
    {
        public static Powerup GetShotgunPowerup()
        {
            return new ShotgunPowerup();
        }

        public static Powerup GetShotgunPowerup(Vector2 position)
        {
            Powerup powerup = GetShotgunPowerup();
            powerup.Position = position;
            return powerup;
        }

        public static Powerup GetFrostOrbPowerup()
        {
            return new FrostOrbPowerup();
        }

        public static Powerup GetFrostOrbPowerup(Vector2 position)
        {
            Powerup powerup = GetFrostOrbPowerup();
            powerup.Position = position;
            return powerup;
        }

        public static Powerup GetTimeFreezePowerup()
        {
            return new TimeFreezePowerup();
        }

        public static Powerup GetTimeFreezePowerup(Vector2 position)
        {
            Powerup powerup = GetTimeFreezePowerup();
            powerup.Position = position;
            return powerup;
        }

        public static Powerup GetRandomPowerup()
        {
            return new RandomPowerup();
        }

        public static Powerup RandomPowerup(Vector2 position)
        {
            Powerup powerup = GetRandomPowerup();
            powerup.Position = position;
            return powerup;
        }
    }
}
