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

using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe mère abstraite de tous les objets physique du jeu
    /// </summary>
    public abstract class MovingObject 
    {
        #region "Attributes"
        private static List<Laser> EMPTY_LASER_LIST = new List<Laser>();
        protected GameManager.GameManager _gameManager = null;
        public int MAX_SPEED = 10;
        public Vector2 _position;
        protected Vector2 _speed;
        protected Vector2 _acceleration;
        protected Moving.MovingBase _moove;
        protected float _rotation;
        protected Texture2D _sprite;
        protected Vector2 _origin;
        protected float _scale = 1;
        protected const float COEFFICIENT_ACCEL = 0.3f;
        private bool _isAlive = true;
        protected Defense.NoShield _shield;
        protected float _fadeTime = 0;
        protected List<MovingObject> _lstToAdd;
        public int _value =0 ;
        
        #endregion

        #region "Accessors"
        public virtual int getMaxSpeed()
        {
            return MAX_SPEED;
        }

        public float FadeTime
        {
            get { return _fadeTime; }
            set { _fadeTime = value; }
        }
        public Defense.NoShield Shield
        {
            get { return _shield; }
            set { _shield = value; }
        }


        public virtual bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }
        public GameManager.GameManager gameManager
        {
            get { return _gameManager; }
            set { _gameManager = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set 
            { 
               _position = value;
            }
        }

        public virtual float XPosition
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        public float YPosition
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public float RotationInDegrees
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public float RotationInRadians
        {
            get { return (float)MathHelper.ToRadians(_rotation); }

            set { _rotation = MathHelper.ToDegrees(value); }
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float SpeedX
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float SpeedY
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }

        public Vector2 Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value; }
        }


        public Texture2D Sprite
        {
            get { return _sprite; }
            set { 
                _sprite = value;
                _origin.X = _sprite.Width * _scale / 2;
                _origin.Y = _sprite.Height * _scale / 2;
            }
        }

        public Moving.MovingBase Moove
        {
            get { return _moove; }
            set { _moove = value; }
        }


        public float scale
        {
            get { return _scale; }
            set{_scale = value;}
        }
        #endregion

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="inInfinite">Si vrai, passe de 0 a width, 
        /// si faux, passe de 0 a  -1 </param>
        public MovingObject(Boolean inInfinite = true)//GameManager.GameManager pGMng)
        {
            IsAlive = true;
            Position = new Vector2(0, 0);
            Speed = new Vector2(0,0);
            Acceleration = new Vector2(0,0);
            RotationInDegrees = 0;
            Sprite = SpriteManager.DefaultSprite;
            _shield = new Defense.NoShield(this);
            _lstToAdd = new List<MovingObject>();
            _moove = new Moving.MovingBase(this);
        }
        
        /// <summary>
        /// Appel _move.Update()
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //_position.X += (float)Math.Cos(RotationInRadians) * _speed;
            //_position.Y += (float)Math.Sin(RotationInRadians) * _speed;
           this._moove.Update(gameTime);
        }

        /// <summary>
        /// Liste de moving objects a ajouter par le gameManager
        /// </summary>
        /// <returns>liste de moving Objets a ajouter via le gameManager</returns>
        public virtual List<MovingObject> spawnedChilds()
        {
            List<MovingObject> retour = new List<MovingObject>();
            retour.AddRange(_lstToAdd);
        
            _lstToAdd.Clear();

            return retour;
        }

        /// <summary>
        /// change la direction de l'accélération selon RotationInRadians de l'objet
        /// </summary>
        public virtual void changeAngleAccel()
        {
            _acceleration.X = COEFFICIENT_ACCEL * (float)Math.Cos(RotationInRadians);
            _acceleration.Y = COEFFICIENT_ACCEL * (float)Math.Sin(RotationInRadians);
        }

        /// <summary>
        /// change la direction de la vitesse selon RotationInRadians de l'objet
        /// </summary>
        public void setSpeedWithDirection()
        {
            float speed = (float)Math.Sqrt(SpeedX * SpeedX + SpeedY * SpeedY);
            SpeedX = (float)(Math.Cos(RotationInRadians) * speed);
            SpeedY = (float)(Math.Sin(RotationInRadians) * speed);
        }

        /// <summary>
        /// Code pour setter un objet a mort
        /// </summary>
        public virtual void Die()
        {
            IsAlive = false;
        }

        /// <summary>
        /// gère l'affichage de l'objet
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position /*- GameManager.GameManager._CameraPosition*/, null, Color.White, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);

            ///Se déssiner aussi de l'autre côté de l'écran si près d'un côté pour effet de continuité
           if(GameManager.GameManager.InfiniteWorld){
                checkSides(spriteBatch);
            }
        }

        /// <summary>
        /// Prolonge l'affichage de l'objet (si inInfiniteWorld)
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="endRecursive">si faux, r'appel la fonction dans l'autre sens</param>
        /// <param name="fromTop"></param>
        protected virtual void checkSides(SpriteBatch spriteBatch, bool endRecursive = false, bool fromTop = true)
        {
            Vector2 tmpVec;
            //Un draw pour chaque côté
            if (this._position.X < ViewPortManager.Width / 1.5f)
            {//DROITE
                tmpVec = new Vector2(_position.X + GameManager.GameManager._WidthGame, _position.Y);
                spriteBatch.Draw(_sprite, tmpVec/* - GameManager.GameManager._CameraPosition*/, null, Color.Blue, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);

                if (endRecursive)
                {
                    tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame);
                    spriteBatch.Draw(_sprite, tmpVec /*- GameManager.GameManager._CameraPosition*/, null, Color.Blue, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
                                    }
                else
                {
                    checkUpDown(spriteBatch, true, false);
                }
            }
                //METTRE ELSEIF si l'espace est > la fenêtre/2 (ou 1.5)
            if (this._position.X > GameManager.GameManager._WidthGame - ViewPortManager.Width / 1.5f)
            {// GAUCHE
                tmpVec = new Vector2(_position.X - GameManager.GameManager._WidthGame , _position.Y);
                spriteBatch.Draw(_sprite, tmpVec /*- GameManager.GameManager._CameraPosition*/, null, Color.Green, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);

                if (endRecursive)
                {
                    tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame );
                    spriteBatch.Draw(_sprite, tmpVec /*- GameManager.GameManager._CameraPosition*/, null, Color.Green, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
                 }
                else
                {
                    checkUpDown(spriteBatch, true, true);
                }
            }
            else
            {
                checkUpDown(spriteBatch, true);
            }
        }

        /// <summary>
        /// Ajoute un clone an haut où nécéssaire (2 si dans coin)
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="endRecursive"></param>
        /// <param name="fromLeft"></param>
        protected virtual void checkUpDown(SpriteBatch spriteBatch, bool endRecursive = false, bool fromLeft = true)
        {
            Vector2 tmpVec;
            //Un draw pour chaque côté
            if (this._position.Y < ViewPortManager.Height / 1.5f)
            {//BAS
                tmpVec = new Vector2(_position.X, _position.Y + GameManager.GameManager._HeightGame );
                spriteBatch.Draw(_sprite, tmpVec /*- GameManager.GameManager._CameraPosition*/, null, Color.Gray, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);

                //vérifie si doit aussi ajouter un clone dans coin
                if (endRecursive)
                {
                    if (fromLeft)
                    {
                        tmpVec = new Vector2(_position.X - GameManager.GameManager._WidthGame, tmpVec.Y);
                    }
                    else
                    {
                        tmpVec = new Vector2(_position.X + GameManager.GameManager._WidthGame, tmpVec.Y);
                    }
                    spriteBatch.Draw(_sprite, tmpVec/* - GameManager.GameManager._CameraPosition*/, null, Color.Gray, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
                }
                else
                {
                    checkSides(spriteBatch, true, false);
                }
            }
            //METTRE ELSEIF si l'espace est > la fenêtre/2 (ou 1.5)
            if (this._position.Y > GameManager.GameManager._HeightGame - ViewPortManager.Height  / 1.5f)
            {
                tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame);
                spriteBatch.Draw(_sprite, tmpVec/* - GameManager.GameManager._CameraPosition*/, null, Color.Yellow, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);

                if (endRecursive)
                {
                    if (fromLeft)
                    {
                        tmpVec = new Vector2(_position.X - GameManager.GameManager._WidthGame, tmpVec.Y);
                    }
                    else
                    {
                        tmpVec = new Vector2(_position.X + GameManager.GameManager._WidthGame, tmpVec.Y);
                    }
                    spriteBatch.Draw(_sprite, tmpVec /*- GameManager.GameManager._CameraPosition*/, null, Color.Gray, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
                }
                else
                {
                    checkSides(spriteBatch, true, true);
                }
            }
        }
    }
}