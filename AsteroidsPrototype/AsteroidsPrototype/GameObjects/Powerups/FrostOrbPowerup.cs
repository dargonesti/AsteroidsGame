using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AsteroidsPrototype.GameObjects.Weapons;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// PowerUp de FrostOrb
    /// </summary>
    class FrostOrbPowerup : Powerup
    {
        public FrostOrbPowerup() : base()
        {
            this.Sprite = SpriteManager.FrostOrbPowerup;
            this._canRespawn = false;
        }

        public override void ApplyTo(Ship ship)
        {
            ship.AddWeapon(new FrostOrb(ship));
        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {

        }
    }
}
