using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.Utilities;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Factory d'astéroides
    /// </summary>
    public static class AsteroidFactory
    {
        /// <summary>
        /// Créé un Astéroide
        /// </summary>
        /// <param name="pScattertimes">Nombre de fois qu'un astéroide éclate en plus petits</param>
        /// <param name="pnbToscatter">Nombre de petits astéroides a créé quand le gros éclate</param>
        /// <returns></returns>
        public static Asteroid GenerateAsteroid(int pScattertimes=2,int pnbToscatter = 3)
        {
            Asteroid asteroid = new Asteroid(pScattertimes, pnbToscatter);//pGMng);
            asteroid.RotationInDegrees = Utilitaire._random.Next(0, 360);
            asteroid.XPosition = Utilitaire._random.Next(100, GameManager.GameManager._WidthGame - 100);
            asteroid.YPosition = Utilitaire._random.Next(100, GameManager.GameManager._HeightGame - 100);
            asteroid.Speed = new Vector2(Utilitaire._random.Next(-200, 200) / 100f, Utilitaire._random.Next(-200, 200) / 100f);
            return asteroid;
        }

        /// <summary>
        /// Créé un petit Astéroide (qui n'éclate pas en plus petits)
        /// </summary>
        /// <param name="pScattertimes">Nombre de fois qu'un astéroide éclate en plus petits</param>
        /// <param name="pnbToscatter">Nombre de petits astéroides a créé quand le gros éclate</param>
        /// <returns></returns>
        public static Asteroid GenerateSmallAsteroid(int pScattertimes = 0, int pnbToscatter = 0)
        {
            Asteroid asteroid = new Asteroid(pScattertimes, pnbToscatter);//pGMng);
           // asteroid.Sprite = SpriteManager.SmallAsteroid;
            asteroid.RotationInDegrees = Utilitaire._random.Next(0, 360);
            asteroid.XPosition = Utilitaire._random.Next(100, GameManager.GameManager._WidthGame - 100);
            asteroid.YPosition = Utilitaire._random.Next(100, GameManager.GameManager._HeightGame - 100);
            asteroid.Speed = new Vector2(Utilitaire._random.Next(-200, 200) / 100f, Utilitaire._random.Next(-200, 200) / 100f);
            asteroid.scale /= 4;
            return asteroid;
        }
            }
}
