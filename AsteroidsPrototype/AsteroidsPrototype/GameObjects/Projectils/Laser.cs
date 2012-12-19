using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.Utilities;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe représentant un laser comme movingObject, tiré par le ship
    /// </summary>
    public class Laser : MovingObject
    {
        public new int MAX_SPEED = 100;
        public const float LASER_SPEED = 30;
        protected float _ttl;
       //public const TypeObjet TYPE_OBJ = TypeObjet.Laser;

#region "accesseur"

        override public bool IsAlive
        {
            get
            {
                return _ttl > 0;
            }
            set
            {
                if (!value)
                {
                    _ttl = 0;
                }
            }
        }

#endregion
        public override int getMaxSpeed()
        {
            return MAX_SPEED;
        }
        public Laser(MovingObject parent, bool keepSpeed = true, float pttl = 400)
        {
            _lstToAdd = new List<MovingObject>();
            _sprite = SpriteManager.Laser;// SpriteManager.Laser;
            _scale = SpriteManager.scaleLaser;
            _ttl = pttl;

            _speed = (keepSpeed ? parent.Speed : Vector2.Zero) +  new Microsoft.Xna.Framework.Vector2((float)Math.Cos(parent.RotationInRadians) * LASER_SPEED,
                (float)Math.Sin(parent.RotationInRadians) * LASER_SPEED);
            _position = parent.Position;
            _rotation = parent.RotationInDegrees;

            _origin.X = _sprite.Width / 2;
            _origin.Y = _sprite.Height / 2;
            _shield.isWeapon = true;
            MAX_SPEED = 100;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _ttl -= gameTime.ElapsedGameTime.Milliseconds;
        }

        /// <summary>
        /// Code a éffectuer quand un laser touche une cible
        /// </summary>
        public virtual void TouchCible()
        {
            IsAlive = false;
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
