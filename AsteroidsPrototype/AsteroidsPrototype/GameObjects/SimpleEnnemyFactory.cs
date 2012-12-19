using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.Utilities;
using AsteroidsPrototype.GameManager;
using AsteroidsPrototype.GameObjects.Weapons;
using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe Factory d'IAs enemis
    /// </summary>
    public static class SimpleEnnemyFactory
    {
        /// <summary>
        /// Créé un IA qui se dirige vers le joueur toutes les x secondes et tir
        /// </summary>
        /// <param name="playerShip">Référense vers le joueur</param>
        /// <param name="speed">vitesse de l'ennemi (3 par défaut)</param>
        /// <param name="delayShoot">millisecondes entre les tirs</param>
        /// <returns></returns>
        public static EnnemyShip GenerateSimpleEnnemi(Ship playerShip, float speed = 3, int delayShoot = 1000)
        {
            ActionFollowPlayer mouvement;
            EnnemyShip enemyShip = new EnnemyShip();

            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(Utilitaire._random.Next(0, GameManager.GameManager._WidthGame), Utilitaire._random.Next(0, GameManager.GameManager._HeightGame));
            enemyShip.Speed = new Vector2(speed, 0);
            enemyShip.RotationInDegrees = Utilitaire._random.Next(0, 360);
            enemyShip.setSpeedWithDirection();
            mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 0, 5000);

            mouvement.Recursive = true;

            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip, 300), delayShoot, (int)(delayShoot)));

            return enemyShip;
        }
        /// <summary>
        /// Créé un IA qui se dirige vers le joueur toutes les x secondes et tir
        /// </summary>
        /// <param name="pX">position en Y initiale</param>
        /// <param name="pY">position en X initiale</param>
        /// <param name="pDegree">degré initial</param>
        /// <param name="pRotateSpeed">vitesse de rotation</param>
        /// <param name="speed">vitesse de l'ennemi (3 par défaut)</param>
        /// <param name="delayShoot">millisecondes entre les tirs</param>
        /// <returns></returns>
        public static EnnemyShip GenerateCirclingEnnemi(float pX, float pY, int pDegree, float pRotateSpeed = 1.5f,float speed = 4, int delayShoot = 1000)
        {
            ActionMouvement mouvement;
            EnnemyShip enemyShip = new EnnemyShip();

            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(pX, pY);
            enemyShip.Speed = new Vector2(speed, 0);
            enemyShip.RotationInDegrees = pDegree;
            enemyShip.setSpeedWithDirection();
            mouvement = new ActionMouvement(enemyShip);
            mouvement.Recursive = true;
            mouvement.setRotation(pRotateSpeed);

            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip, 300), delayShoot, (int)(delayShoot)));

            return enemyShip;
        }
    }
}
