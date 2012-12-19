using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe du vaisseau du joueur
    /// </summary>
    public class Ship : MovingObject
    {

        #region "attributes"
        public new int MAX_SPEED = 12;
        private const float FRICTION_COEFFICINT = 0.98F;
        private const int ROTATION_SPEED = 4;
        //Liste de Lasers tirés que le gameManager ramasse et vide
        protected Weapons.Weapon _activeWeapon = null;

        protected List<Weapons.Weapon> _lstWeapons = null;
        private bool _shoot = false;
        private Vector2  _accel = Vector2.Zero ;
        private float _rotationPSec = 0;

        private bool _weaponsLocked = false;
        private double _timeOfLock;
        private double _timeToUnlock = 5;

        protected GamePadState _oldGamePad ;
        // public const TypeObjet TYPE_OBJ = TypeObjet.Player;
#endregion

        #region "accessors"
        public override int getMaxSpeed()
        {
            return MAX_SPEED;
        }
         public List<Weapons.Weapon> LstWeapons
         {
             get { return _lstWeapons; }
             set { _lstWeapons = value; }
         }
        public Weapons.Weapon ActiveWeapon
        {
            get { return _activeWeapon; }
            set { _activeWeapon = value; }
        }

        public bool WeaponsLocked
        {
            get { return _weaponsLocked; }
            set { _weaponsLocked = value; }
        }
#endregion

        /// <summary>
        /// Constructeur de base 
        /// </summary>
        public  Ship()
            : base()//pGMng)
        {
            _lstWeapons = new List<Weapons.Weapon>() { new Weapons.Weapon(this)};
            _activeWeapon = LstWeapons.First();
            //_weapon = new Weapons.Weapon( this);
            _moove = new Moving.MovingDirect(this);
            Shield = new Defense.DefenseBase(this);
            _value = TypeScore.Hit;

            _moove.Friction = 0.01f;
            _rotation = -90;
            changeAngleAccel();
            Sprite = SpriteManager.ShipMainSprite;
            _scale = SpriteManager.scaleRateShip;
            _origin.X = _sprite.Width  / 2;
            _origin.Y = _sprite.Height  / 2;
            MAX_SPEED = 12;
        }

        /// <summary>
        /// Modifier le ship selon les inputs du joueur
        /// </summary>
        /// <param name="keyBoardState"></param>
        /// <param name="mouseState"></param>
        /// <param name="gamePadState"></param>
        public virtual void HandlePlayerInput(KeyboardState keyBoardState, MouseState mouseState, GamePadState gamePadState)
        {
            _shoot = (keyBoardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A));
            _moove.sendInput(new Vector2((keyBoardState.IsKeyDown(Keys.Left) ? -1 : (keyBoardState.IsKeyDown(Keys.Right) ? 1 : gamePadState.ThumbSticks.Left.X)),
                (keyBoardState.IsKeyDown(Keys.Down) ? -1 : (keyBoardState.IsKeyDown(Keys.Up) ? 1 : gamePadState.ThumbSticks.Left.Y))), gamePadState.Triggers.Right- gamePadState.Triggers.Left);
          

            //type d'Arme... pas pratique, doit faire un ifElse mais pas le choix avec le système de inputs présent.
            if (keyBoardState.IsKeyDown(Keys.D1) && LstWeapons.IndexOf(_activeWeapon) != 0)
            {
                 _activeWeapon = LstWeapons[0];
            }
            else if (LstWeapons.Count > 1 &&  keyBoardState.IsKeyDown(Keys.D2) && LstWeapons.IndexOf(_activeWeapon) != 1)
            {
                _activeWeapon = LstWeapons[1];
            }
            else if (LstWeapons.Count > 2 && keyBoardState.IsKeyDown(Keys.D3) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[2];
            }
            else if (LstWeapons.Count > 3 && keyBoardState.IsKeyDown(Keys.D4) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[3];
            }
            else if (LstWeapons.Count > 4 && keyBoardState.IsKeyDown(Keys.D5) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[4];
            }
            else if (LstWeapons.Count > 5 && keyBoardState.IsKeyDown(Keys.D6) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[5];
            }
            else if (LstWeapons.Count > 6 && keyBoardState.IsKeyDown(Keys.D7) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[6];
            }
            else if (LstWeapons.Count > 7 && keyBoardState.IsKeyDown(Keys.D8) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[7];
            }
            else if (LstWeapons.Count > 8 && keyBoardState.IsKeyDown(Keys.D9) && LstWeapons.IndexOf(_activeWeapon) != 2)
            {
                _activeWeapon = LstWeapons[8];
            }

            // changement d'armes avec x et y pour manette
            if (gamePadState.Buttons.X == ButtonState.Pressed && _oldGamePad.Buttons.X != ButtonState.Pressed)
            {
                _activeWeapon = LstWeapons[(LstWeapons.IndexOf(_activeWeapon) + 1) % LstWeapons.Count];
            }
            else if (gamePadState.Buttons.Y == ButtonState.Pressed && _oldGamePad.Buttons.Y != ButtonState.Pressed)
            {
                if (LstWeapons.IndexOf(_activeWeapon) <= 0)
                {
                    _activeWeapon = LstWeapons[LstWeapons.Count - 1];
                }
                else
                {
                    _activeWeapon = LstWeapons[LstWeapons.IndexOf(_activeWeapon) - 1];
                }
            }
            _oldGamePad = gamePadState;
        }

        /// <summary>
        /// Update le vaisseau
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_weaponsLocked)
            {
                _timeOfLock = gameTime.TotalGameTime.TotalSeconds;
            }
            else
            {
                if (gameTime.TotalGameTime.TotalSeconds - _timeOfLock >= _timeToUnlock)
                {
                    _weaponsLocked = false;
                }
            }


            if (_shoot)
            {
                if (!_weaponsLocked)
                {
                    _activeWeapon.CheckShoot(ref _lstToAdd);
                }
            }
        }

        /// <summary>
        /// Redéfini pour ne pas se réafficher si espace < écran
        /// </summary>
        /// <param name="spriteBatch"></param>
       override public void Draw(SpriteBatch spriteBatch)
       {
           spriteBatch.Draw(_sprite, _position/* - GameManager.GameManager._CameraPosition*/, null, Color.White * (_fadeTime <= 0 ? 1 : 0.5f), RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
           Shield.Draw(spriteBatch, _origin, _scale);
       }

        /// <summary>
        /// Ajoute une arme à la liste du ship
        /// </summary>
        /// <param name="weapon"></param>
       public void AddWeapon(Weapons.Weapon weapon)
       {
           bool canAdd = true;
           foreach(Weapons.Weapon currentWeapon in LstWeapons)
           {
               if (weapon.GetType() == currentWeapon.GetType())
               {
                   canAdd = false;
               }
           }

           if (canAdd)
           {
               LstWeapons.Add(weapon);
           }
       }
    }
}
