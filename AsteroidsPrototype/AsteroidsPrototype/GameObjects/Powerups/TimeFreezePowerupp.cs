using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Classe de powerUp qui fige le temps pour les enemis
    /// </summary>
    class TimeFreezePowerup : Powerup
    {
        public TimeFreezePowerup() : base()
        {
            this.Sprite = SpriteManager.TimeFreezePowerup;
            this._isAppliedToWorld = true;
            this._timeToRespawn = 10;
        }

        public override void ApplyTo(Ship ship)
        {

        }

        public override void ApplyTo(GameManager.GameManager gameManager)
        {
            gameManager.TimeFreeze = true;
        }
    }
}
