using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using AsteroidsPrototype.GameManager;

namespace AsteroidsPrototype.GameObjects.Powerups
{
    /// <summary>
    /// Classe représentant un movingObject de powerUp
    /// </summary>
    public abstract class Powerup : MovingObject
    {
        protected bool _isAppliedToWorld;
        protected bool _canRespawn;
        protected bool _isActive;

        protected double _timeToRespawn;
        protected double _inactivationTime;

        public bool IsAppliedToWorld
        {
            get { return _isAppliedToWorld; }
        }

        public bool CanRespawn
        {
            get { return _canRespawn; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public Powerup() : base()
        {
            _isAppliedToWorld = false;
            _canRespawn = true;
            _isActive = true;
            _timeToRespawn = 0;
        }

        public abstract void ApplyTo(Ship ship);
        public abstract void ApplyTo(GameManager.GameManager gameManager);

        #region "MovingObject Overrides"

        public override void Update(GameTime gameTime)
        {
            if (_isActive)
            {
                _inactivationTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else
            {
                if (gameTime.TotalGameTime.TotalSeconds - _inactivationTime >= _timeToRespawn)
                {
                    _isActive = true;
                }
            }
        }


        #endregion
    }
}
