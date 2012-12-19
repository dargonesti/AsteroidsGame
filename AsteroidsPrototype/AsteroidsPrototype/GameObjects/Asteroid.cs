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
    /// Classe héritée de moving Object définissant un astéroide
    /// </summary>
    public class Asteroid : MovingObject
    {
        protected int _scatterTimes;
        protected int _nbToScatter;

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="pNbScattertimes">Nombre de fois qu'un astéroide éclate en plus petits</param>
        /// <param name="pNbToScatter">Nombre de petits asétroides à créé quand le gros éclate</param>
        public Asteroid(int  pNbScattertimes= 2, int pNbToScatter = 3)
            : base()//GameManager.GameManager pGMng) : base(pGMng)
        {
            _sprite = SpriteManager.LargeAsteroid;
            _scale = SpriteManager.scaleRateAsteroid;
            _origin.X = _sprite.Width / 2;
            _origin.Y = _sprite.Height / 2;
            _scatterTimes = pNbScattertimes;
            _nbToScatter = pNbToScatter;
            _value = TypeScore.Asteroid;
        }

        /// <summary>
        /// Code pour mettre un astéroide à mort
        /// </summary>
       override public void Die()
        {
            IsAlive = false;
           lock(this)
           {
            _lstToAdd = new List<MovingObject>();
            Asteroid astTmp;
            if (this._scatterTimes> 0)
            {
                for (int i = 0; i < _nbToScatter; i++)
                {
                    astTmp = AsteroidFactory.GenerateAsteroid(_scatterTimes -1,_nbToScatter );
                    _lstToAdd.Add(astTmp);
                    astTmp.Position = this.Position;
                    astTmp.scale = this.scale / 2;
                }
            }
           }
        }

    }
}
