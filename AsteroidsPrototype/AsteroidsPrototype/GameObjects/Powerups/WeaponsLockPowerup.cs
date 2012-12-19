using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Classe de powerUp qui donne un malus au joueur quelques secondes
    /// </summary>
    class WeaponsLockPowerup : Powerup
    {
        public WeaponsLockPowerup() : base()
        {
            this.Sprite = SpriteManager.WeaponsLockPowerUp;
            this._timeToRespawn = 5;
        }

        public override void ApplyTo(Ship ship)
        {
            ship.WeaponsLocked = true;
        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {

        }
    }
}
