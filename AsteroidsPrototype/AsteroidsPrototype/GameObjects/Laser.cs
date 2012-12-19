using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects
{
    class Laser : MovingObject
    {
        public const float LASER_SPEED = 12;
        private float ttl = 3000;

        public bool isDead
        {
        get{
        return ttl <= 0;
    }
    }
        
        public Laser(MovingObject parent)
        {
            _speed = parent.Speed +  new Microsoft.Xna.Framework.Vector2((float)Math.Cos(parent.RotationInRadians) * LASER_SPEED,
                (float)Math.Sin(parent.RotationInRadians) * LASER_SPEED);
            _position = parent.Position;
            _rotation = parent.RotationInDegrees;

            _sprite = SpriteManager.Laser;
            _scale = SpriteManager.scaleLaser;
            _origin.X = _sprite.Width / 2;
            _origin.Y = _sprite.Height / 2;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ttl -= gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
