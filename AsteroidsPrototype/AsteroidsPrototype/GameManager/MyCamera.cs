using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameManager
{
    /// <summary>
    /// Gestion d'une matrice pour l'affichage du monde en 2D
    /// </summary>
    class MyCamera
    {
        protected float          _zoom; // Camera Zoom
        public Matrix            _transform; // Matrix Transform
        public Vector2          _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        private Vector2 _shakePos;
        private Vector2 _shakeTarget = Vector2.Zero;
        private int _timeShaking = 250;// temps que la cam a été shaké dans cette dir là
        private int TIME_TO_SHAKE = 550;//max temps pour brasser la caméra
        private int _timeToShake = 250;//tps random pour shaker la cam
        private int _LengthShake = 3;
        private GameObjects.Ship _ship;
        public Boolean _sideScroller = false;
        public float _speed = 1;

        #region "accessors"
        public GameObjects.Ship Ship
        {
            get { return _ship; }
            set { _ship = value; }
        }
        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        #endregion
        public MyCamera()
        {
            _zoom = 0.7f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }
        public void ApplyShake()
        {/*
            float forceToShake;
            Vector2 dirForce;//sin((PI*x*2) -0.5 * PI) + 1 => force
            dirForce = _shakeTarget; //sin((PI*x) -0.5 * PI)* 10(10 := _timeToShake) => position
            forceToShake = (float)(Math.Sin(Math.PI * (_timeShaking / _timeToShake - 0.5)) * 1.5f)+1 ;
            _shakePos = (_shakePos  + (dirForce * forceToShake * (5 + _ship.Speed.Length()) / 2000))*0.99f;
            //_shakePos = (_shakePos + _shakeTarget * (5 + _ship.Speed.Length()) / 2000) * 0.99f;*/
            if (_sideScroller)
            {
                Pos = new Vector2(Pos.X + Ship.Moove.MaxSpeed /2, GameManager._HeightGame/2);
            }
            else
            {
                Pos = _ship.Position/* + _ship.Speed * 6 + _shakePos*/;
            }
        }
        public Matrix get_transformation(GameTime gameTime)
        {
            _transform =  
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(ViewPortManager.Width * 0.5f,
                                             ViewPortManager.Height * 0.5f, 0));
            if (!_sideScroller)
            {
                _timeShaking += gameTime.ElapsedGameTime.Milliseconds;
                if (_timeShaking > _timeToShake)
                {
                    _timeToShake = Utilitaire._random.Next(100, TIME_TO_SHAKE);
                    _timeShaking = 0;
                    _shakeTarget = new Vector2(Utilitaire._random.Next(-_LengthShake * 100, _LengthShake * 100), Utilitaire._random.Next(-_LengthShake * 100, _LengthShake * 100));
                }
            }
            return _transform;
        }
    }
}
