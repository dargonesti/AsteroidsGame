﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsPrototype.GameObjects.Walls;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects.Moving
{
    /// <summary>
    /// Classe héritée de MovingBase qui fais bouger comme dans un sideScroller (toujuors vers la droite)
    /// </summary>
    public class MovingSideScroller : MovingBase
    {
        public MovingSideScroller(MovingObject pParent)
            : base(pParent)
        {
            _accelFactor = 2f;
            _friction = 0.2f;
        }

        public override void Update(GameTime gameTime)
        {
            float tanRot = (float)Math.Tan(_parent.RotationInRadians);
            float cosRot = (float)Math.Cos(_parent.RotationInRadians);
            float sinRot = (float)Math.Sin(_parent.RotationInRadians);

            _parent.Position += new Vector2(_parent.MAX_SPEED / 2 + _parent.SpeedX, _parent.Speed.Y);

            _inputs = new Vector2(_inputs.X,- _inputs.Y);
            if (GameManager.GameManager.InfiniteWorld)
            {
                if (_parent.Position.X < 0)
                {
                    _parent._position.X = GameManager.GameManager._WidthGame;
                }
                else if (_parent.Position.X > GameManager.GameManager._WidthGame)
                {
                    _parent._position.X = 0;
                }

                if (_parent.Position.Y < 0)
                {
                    _parent._position.Y = GameManager.GameManager._HeightGame;
                }
                else if (_parent.Position.Y > GameManager.GameManager._HeightGame)
                {
                    _parent._position.Y = 0;
                }
            }
            if (_parent.FadeTime > 0)
                _parent.FadeTime -= gameTime.ElapsedGameTime.Milliseconds;

            /////////////inputs
            _accel = _accelFactor;
            _accelVec = _inputs * _accelFactor ;
            

            _parent.Speed += _accelVec;
           
            _parent.Speed *= (float)(1 - _friction);
           


           /* if (_parent.RotationInDegrees - rotationTarget < ROTATION_SPEED * 5)
            {*/
           _parent.RotationInDegrees = 0;
            /*}
            else if (rotationTarget - _parent.RotationInDegrees < 180)
            {
                _parent.RotationInDegrees += ROTATION_SPEED * 4;
            }
            else
            {
                _parent.RotationInDegrees -= ROTATION_SPEED * 4;
            }*/
           // _parent.RotationInRadians = (rotationTarget+_parent.RotationInRadians)/2;
            /*
            if (_accel == 0)
            {
                _parent.Speed *= 1- _friction;
            }*/

           if (Utilitaire.DiffVec2(_parent.Speed) > _parent.getMaxSpeed() * 4)
            {
                _parent.Speed *= _parent.getMaxSpeed() * 4 / Utilitaire.DiffVec2(_parent.Speed);
            }

        }
    }
}
