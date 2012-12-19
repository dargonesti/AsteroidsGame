using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Classe donnant un bouclier au Ship
    /// </summary>
    class ShieldPowerup : Powerup
    {
        public ShieldPowerup()
        {
            this._timeToRespawn = 10;
            this.Sprite = SpriteManager.ShieldPowerUp;
        }

        public override void ApplyTo(Ship ship)
        {
            ship.Shield = new Defense.DefenseBase(ship);
        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {
            
        }
    }
}
