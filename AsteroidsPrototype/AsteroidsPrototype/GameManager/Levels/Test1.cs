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
    public class Test1 : ILevel
    {
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
            
            for (int i = 0; i < 50; i++)
            {
                retour.Add(AsteroidFactory.GenerateAsteroid());
                retour.Add(AsteroidFactory.GenerateSmallAsteroid());
            }
            return retour;
        }
        protected override List<MovingObject> GetIAs(Ship playerShip)
        {
              EnnemyShip enemyShip;
            ActionMouvement mouvement;
            ActionShoot shoot;
            List<MovingObject> retour = new List<MovingObject>();
            
            for (int i = 150; i < 250; i++)
            {
                enemyShip = new EnnemyShip(false);
                enemyShip.Position = new Vector2(i * 1 + 500, i * 1 + 500);
                enemyShip.Speed = new Vector2(0, 13);

                mouvement = new ActionMouvement(enemyShip, 0,0);
                mouvement.Recursive = true;
                mouvement.setRotation((float)(i)/200f);
                Weapon wep = new Weapon(enemyShip, 500);
                shoot = new ActionShoot(enemyShip,  wep, 10, 0);
                shoot.ActOnDegree = true;
                shoot.DegreeToAct = i * 360 / 1000;


                enemyShip.AddAction(mouvement);
                enemyShip.AddAction(shoot);
                if (i % 10 == 0)
                {
                enemyShip.AddAction(new ActionShoot(enemyShip, new FrostOrb(enemyShip, 4000), 5000, 0));
                 }
                else if (i % 5 == 0)
                {
                    enemyShip.AddAction(new ActionShoot(enemyShip, new Weapon(enemyShip, 1000), 5000, 0));
                }
                retour.Add(enemyShip);
            }
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
            retour.Add(new Wall(new Vector2(1000, 1000), new Vector2(0, 1000)));
            retour.Add(new Wall(new Vector2(1000, 1000), new Vector2(1000, 00)));
            retour.Add(new Wall(new Vector2(2000, 1000), new Vector2(000, 1000)));
            retour.Add(new Wall(new Vector2(1000, 2000), new Vector2(1000, 000)));


            retour.Add(new Wall(new Vector2(1500, 500), new Vector2(1000, 1000)));
            retour.Add(new Wall(new Vector2(1500, 500), new Vector2(-1000, 1000)));
            retour.Add(new Wall(new Vector2(500, 1500), new Vector2(1000, 1000)));
            retour.Add(new Wall(new Vector2(2500, 1500), new Vector2(-1000, 1000)));
            return retour;
        }            
    }
}
