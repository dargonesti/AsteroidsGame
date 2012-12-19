using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsPrototype.Utilities;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe gérant l'affichage d'un radar en haut a droite de l'écran
    /// </summary>
    public class Radar : Microsoft.Xna.Framework.DrawableGameComponent 
    {
        //position de la carte sur l'écran
        Vector2 _position;

        //position du joueur
        Ship _ship;
        //Objets à montrer rouge sur le radar
        List<MovingObject> _enemies;

        //distance à la quelle le radar montre un point rouge en pixels
        float _range = 1500;
        //Couleur du radar, 30% opaque
        Color _bgRadar = new Color(0, 250, 0, 30);
        Color _colorEnemies = Color.Red;
        //Taille de la carte
        float _size = 100;

        //Variables pour déssiner

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameManager"></param>
        public Radar(Microsoft.Xna.Framework.Game game, GameManager.GameManager gameManager) :base(game)
        {
            _position = new Vector2(game.GraphicsDevice.Viewport.Width - _size ,  _size );
            _ship = gameManager.ship;
            _enemies = gameManager.enemies;
            XnaDebugDrawer.DebugDrawer.LoadContent(game.GraphicsDevice);
        }
        
        /// <summary>
        /// Gère l'affichage du radar
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tmpVec;
            List<Vector2> tmpClonePosition = new List<Vector2>();

            XnaDebugDrawer.DebugDrawer.DrawCircle(spriteBatch, _position, _size, _bgRadar, 3, 100);
            XnaDebugDrawer.DebugDrawer.DrawCircle(spriteBatch, _position, 3, _bgRadar, 3, 4);

            foreach (MovingObject enemi in _enemies)
            {
                tmpClonePosition.Clear();
                if (Utilitaire.DiffVec2(enemi.Position - _ship.Position) < this._range)
                {
                    tmpClonePosition.Add(enemi.Position);
                }
                if (GameManager.GameManager.InfiniteWorld)
                {
                    tmpVec = new Vector2(enemi.Position.X - GameManager.GameManager._WidthGame, enemi.Position.Y);
                    if (Utilitaire.DiffVec2(tmpVec - _ship.Position) < this._range)
                    {
                        tmpClonePosition.Add(tmpVec);
                    }
                    tmpVec = new Vector2(enemi.Position.X + GameManager.GameManager._WidthGame, enemi.Position.Y);
                    if (Utilitaire.DiffVec2(tmpVec - _ship.Position) < this._range)
                    {
                        tmpClonePosition.Add(tmpVec);
                    }
                    tmpVec = new Vector2(enemi.Position.X, enemi.Position.Y - GameManager.GameManager._HeightGame);
                    if (Utilitaire.DiffVec2(tmpVec - _ship.Position) < this._range)
                    {
                        tmpClonePosition.Add(tmpVec);
                    }
                    tmpVec = new Vector2(enemi.Position.X, enemi.Position.Y + GameManager.GameManager._HeightGame);
                    if (Utilitaire.DiffVec2(tmpVec - _ship.Position) < this._range)
                    {
                        tmpClonePosition.Add(tmpVec);
                    }
                }

                if (tmpClonePosition.Count > 0 )
                {
                    foreach (Vector2 point in tmpClonePosition )
                    {
                        tmpVec = _position + new Vector2(((point.X - _ship.Position.X) / _range) * _size, (((point.Y - _ship.Position.Y) / _range) * _size));
                        XnaDebugDrawer.DebugDrawer.DrawCircle(spriteBatch,tmpVec, 3, _colorEnemies, 3, 4);
                    }
                }
            }
        }
    }
}
