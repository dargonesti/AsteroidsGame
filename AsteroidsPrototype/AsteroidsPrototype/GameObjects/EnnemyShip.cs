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
using AsteroidsPrototype.GameManager;
using AsteroidsPrototype.GameObjects.Weapons;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe héritée de Ship d'IA ennemie
    /// </summary>
    public class EnnemyShip : Ship
    {
        ActionsIA _ia = new ActionsIA();
        /// <summary>
        /// Constructeur de base 
        /// </summary>
        /// <param name="giveBasicWeapon"></param>
        public  EnnemyShip( bool giveBasicWeapon = true):base()
        {
            _activeWeapon = new Weapon(this);
            if(giveBasicWeapon)
            _ia.AddAction(new ActionShoot(this, _activeWeapon, 1000, 3000));
            _value = TypeScore.EnemiShip;
        }

        /// <summary>
        /// Ajoute une action à l'IA
        /// </summary>
        /// <param name="action"></param>
        public virtual void AddAction(AsteroidsPrototype.GameManager.Action  action)
        {
            _ia.AddAction(action);
        }

        /// <summary>
        /// Update l'IA
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //Copié collé de moving object en attente d'une meilleure solution
            _position += _speed;

            if (_position.X < 0)
            {
                _position.X = GameManager.GameManager._WidthGame;
            }
            else if (_position.X > GameManager.GameManager._WidthGame)
            {
                _position.X = 0;
            }

            if (_position.Y < 0)
            {
                _position.Y = GameManager.GameManager._HeightGame;
            }
            else if (_position.Y > GameManager.GameManager._HeightGame)
            {
                _position.Y = 0;
            }
            if (_fadeTime > 0)
                _fadeTime -= gameTime.ElapsedGameTime.Milliseconds;

            //SECTION NON COPIÉ_COLLEe
            _ia.Update(gameTime);
          /*
            _rotation += _rotationPSec;
            changeAngleAccel();*/
        }

        /// <summary>
        /// fais tirer le ship
        /// </summary>
        /// <param name="arme"></param>
        public void Shoot(Weapon arme)
        {
            arme.CheckShoot(ref _lstToAdd);
        }

/// <summary>
/// On override l'Handle des Inputs pour les ignorer, ce n'est pas le joueur
/// </summary>
/// <param name="keyBoardState"></param>
/// <param name="mouseState"></param>
/// <param name="gamePadState"></param>
        override public void HandlePlayerInput(KeyboardState keyBoardState, MouseState mouseState, GamePadState gamePadState)
        {
        }

        /// <summary>
        /// Redéfini pour ne pas se réafficher si espace < écran
        /// </summary>
        /// <param name="spriteBatch"></param>
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, _position , null, Color.Red, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
            Shield.Draw(spriteBatch, _origin, _scale);
           
            ///Se déssiner aussi de l'autre côté de l'écran si près d'un côté pour effet de continuité
            if (GameManager.GameManager.InfiniteWorld)
            {
                checkSides(spriteBatch);
            }
        }
    }
}
