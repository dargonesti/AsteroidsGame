using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AsteroidsPrototype.GameObjects.Weapons;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Classe de powerUp donnant un shotGun au ship
    /// </summary>
    public class ShotgunPowerup : Powerup 
    {
        public ShotgunPowerup() : base()
        {
            this.Sprite = SpriteManager.ShotgunPowerup;
            this._canRespawn = false;
        }

        public override void ApplyTo(Ship ship)
        {
            ship.AddWeapon(new ShotGun(ship));
        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {

        }
    }
}
