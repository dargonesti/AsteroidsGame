using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects.Walls
{
/// <summary>
/// Classe héritée de Moving Object décrivant un mur
/// </summary>
    public class Wall : MovingObject
    {
        protected Vector2 _direction = Vector2.Zero;

        /// <summary>
        /// position relative a _position du 2 iem point du mur
        /// </summary>
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// constructeur de base
        /// </summary>
        public Wall()
            : base()
        {
            _sprite = SpriteManager.LargeAsteroid; //TODO mettre une vraie image de mur
            _scale = SpriteManager.scaleRateAsteroid;
            _origin.X = _sprite.Width / 2;
            _origin.Y = _sprite.Height / 2;
            _direction = Vector2.Zero;
            _position = Vector2.Zero; 
            _speed = Vector2.Zero; 
            _acceleration = Vector2.Zero;
        }

        /// <summary>
        /// Constructeur avec la position et la direction
        /// </summary>
        /// <param name="pPos1">Position d'un des bouts du mur</param>
        /// <param name="pDir">Prolonge le mur, du Point pPos1 (position), avec pDir</param>
        public Wall(Vector2 pPos1, Vector2 pDir)
            : base()
        {
            _position = pPos1;
            _direction = pDir;
        }

        /// <summary>
        /// Dessine le mur
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, _position, _position + _direction, Color.White, 10);

            ///Se déssiner aussi de l'autre côté de l'écran si près d'un côté pour effet de continuité
            if (GameManager.GameManager.InfiniteWorld)
            {
                checkSides(spriteBatch);
            }
        }

        protected override void checkSides(SpriteBatch spriteBatch, bool endRecursive = false, bool fromTop = true)
        {
            Vector2 tmpVec;
            //Un draw pour chaque côté
            if (this._position.X < ViewPortManager.Width / 1.5f)
            {//DROITE
                tmpVec = new Vector2(_position.X + GameManager.GameManager._WidthGame, _position.Y);

                XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);
                if (endRecursive)
                {
                    tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame);
                    XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);
                }
                else
                {
                    checkUpDown(spriteBatch, true, false);
                }
            }
            //METTRE ELSEIF si l'espace est > la fenêtre/2 (ou 1.5)
            if (this._position.X > GameManager.GameManager._WidthGame - ViewPortManager.Width / 1.5f)
            {// GAUCHE
                tmpVec = new Vector2(_position.X - GameManager.GameManager._WidthGame, _position.Y);
                XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);

                if (endRecursive)
                {
                    tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame);
                    XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);
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
        protected override void checkUpDown(SpriteBatch spriteBatch, bool endRecursive = false, bool fromLeft = true)
        {
            Vector2 tmpVec;
            //Un draw pour chaque côté
            if (this._position.Y < ViewPortManager.Height / 1.5f)
            {//BAS
                tmpVec = new Vector2(_position.X, _position.Y + GameManager.GameManager._HeightGame);
                XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);

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
                    XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);
                }
                else
                {
                    checkSides(spriteBatch, true, false);
                }
            }
            //METTRE ELSEIF si l'espace est > la fenêtre/2 (ou 1.5)
            if (this._position.Y > GameManager.GameManager._HeightGame - ViewPortManager.Height / 1.5f)
            {
                tmpVec = new Vector2(_position.X, _position.Y - GameManager.GameManager._HeightGame);
                XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);

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
                    XnaDebugDrawer.DebugDrawer.DrawLineSegment(spriteBatch, tmpVec, tmpVec + _direction, Color.White, 10);
                }
                else
                {
                    checkSides(spriteBatch, true, true);
                }
            }
        }
    }
}
