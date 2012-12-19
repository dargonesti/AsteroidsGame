using System;
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
    /// Classe appelée pour faire avancer le ship
    /// </summary>
    public class MovingBase
    {
        protected MovingObject _parent;
        protected float _maxSpeed = 10;
        protected float _accelFactor = 0.5f;
        protected float _friction = 0f;
        protected float _accel = 0;
        protected Vector2 _accelVec = Vector2.Zero;
        protected Vector2 _inputs = Vector2.Zero;
        protected Vector2 _normalizedInputs = Vector2.Zero;
        protected const int ROTATION_SPEED = 4;
        protected float _inputsLength = 0;
       
        public float MaxSpeed
        {
        get{
        return _maxSpeed;
    }
            set
            {
                _maxSpeed = value;
            }
    }
        public MovingObject Parent
        {
            get { return _parent; }
        }
        public float Friction
        {
            get { return _friction; }
            set { _friction = value; }
        }
        
        public MovingBase(MovingObject pParent)
        {
            _parent = pParent;
        }

        public void sendInput(Vector2 pInput, float pBreak )
        {
            _inputs = pInput;
            _normalizedInputs = _inputs;
            _normalizedInputs.Normalize();
            _inputsLength = _inputs.Length()- pBreak;
        }
        public virtual void Update(GameTime gameTime)
        {
            float tanRot = (float)Math.Tan(_parent.RotationInRadians);
            float cosRot = (float)Math.Cos(_parent.RotationInRadians);
            float sinRot = (float)Math.Sin(_parent.RotationInRadians);
            _parent.Position += _parent.Speed;

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
            _accel = _accelFactor * _inputs.Y * _inputsLength ;
            _accelVec.X = cosRot * _accel;
            _accelVec.Y = sinRot* _accel;

            if (_accel >= 0)
            {
                _parent.Speed += _accelVec;
            }
            else
            {
                _parent.Speed *= (float)(1 - _friction * ((0.2f-_accel)/0.2f));
            }
            _parent.RotationInDegrees += ROTATION_SPEED * _inputs.X;

            if (_accel == 0)
            {
                _parent.Speed *=1- _friction;
            }

            if (Utilitaire.DiffVec2(_parent.Speed) > _parent.getMaxSpeed())
            {
                _parent.Speed *= _parent.getMaxSpeed() / Utilitaire.DiffVec2(_parent.Speed);
            }

        }
    }
}
