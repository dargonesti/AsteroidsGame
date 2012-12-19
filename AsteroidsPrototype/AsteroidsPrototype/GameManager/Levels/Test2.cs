using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.GameObjects.Weapons;
using AsteroidsPrototype.GameObjects.Walls;
using AsteroidsPrototype.GameObjects;
using Microsoft.Xna.Framework;
using AsteroidsPrototype.GameObjects.Powerups;

namespace AsteroidsPrototype.GameManager.Levels
{
    public class Test2 : ILevel
    {
        protected override Boolean GetWorldMode()
        {
            return false;
        }
        protected override int GetWidth()
        {
            return 4000;
        }

        protected override int GetHeight()
        {
            return 4000;
        }
        protected override List<Powerup> GetPowerUps()
        {
            List<Powerup> lstRetour = new List<Powerup>();
            ShotgunPowerup shotgun = new ShotgunPowerup();
            shotgun.Position = new Vector2(200, 200);
            lstRetour.Add(shotgun);
            return lstRetour;
        }

        protected override  Ship GetShip()
        {
            Ship ship;
            ship = new Ship();
            ship.XPosition = GetWidth() / 2;
            ship.YPosition = GetHeight() / 2;
            return ship;
        }

        protected override List<MovingObject> GetAsteroids()
        {
            List<MovingObject> retour = new List<MovingObject>();
           
            return retour;
        }
        protected override List<MovingObject> GetIAs(Ship playerShip)
        {
            EnnemyShip enemyShip;
            ActionFollowPlayer mouvement;
            List<MovingObject> retour = new List<MovingObject>();
            Weapon wep;
            for (int i = 10;i<100 ; i++)
            {
                enemyShip = new EnnemyShip(false);
                enemyShip.Position = new Vector2(10 + i * 30,100);
                enemyShip.Speed = new Vector2(3, 0);
                if (i % 5 != 0)
                {//nous visent au 5 sec, tirent aux 2 sec
                    mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120, 5000);
                }
                else
                {//suivent toujours
                    mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120);
                }
                mouvement.Recursive = true;
                wep = new Weapon(enemyShip, 500);

                enemyShip.AddAction(mouvement);
                enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip, 2000), 2000, (int)( 2000)));

                retour.Add(enemyShip);
            }
            //Enemis tourrelles*
            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(100, GetHeight()-100);
            enemyShip.Speed = new Vector2(0, 0);
            mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120);
            mouvement.Recursive = true;
            wep = new ShotGun(enemyShip, 1500);
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip), 1000));
            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, wep, 5000));
            retour.Add(enemyShip);
            //tourelle 2
            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(100, 100);
            enemyShip.Speed = new Vector2(0, 0);
            mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120);
            mouvement.Recursive = true;
            wep = new ShotGun(enemyShip, 1500);
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip), 1000));
            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, wep, 5000));
            retour.Add(enemyShip);
            //tourelle 3

            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(GetWidth()-100, 100);
            enemyShip.Speed = new Vector2(0, 0);
            mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120);
            mouvement.Recursive = true;
            wep = new ShotGun(enemyShip, 1500);
            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, wep, 5000));
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip), 1000));
            retour.Add(enemyShip);
            //tourelle 4
            enemyShip = new EnnemyShip(false);
            enemyShip.Position = new Vector2(GetWidth()-100, GetHeight()-100);
            enemyShip.Speed = new Vector2(0, 0);
            mouvement = new ActionFollowPlayer(enemyShip, playerShip, Laser.LASER_SPEED, 120);
            mouvement.Recursive = true;
            wep = new ShotGun(enemyShip, 1500);
            enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip), 1000));
            enemyShip.AddAction(mouvement);
            enemyShip.AddAction(new ActionShoot(enemyShip, wep, 5000));
            retour.Add(enemyShip);


            return retour;
        }


        protected override List<MovingObject> GetAlies()
        {
            List<MovingObject> retour = new List<MovingObject>();

            return retour;
        }
        protected override List<Wall> GetWalls()
        {
            List<Wall> retour = new List<Wall>();

            return retour;
        }            
    }
}
