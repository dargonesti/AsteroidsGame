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
    public class Level5 : ILevel
    {

        public override String GetName()
        {
            return "We are not alone";
        }
        protected override int GetWidth()
        {
            return 2500;
        }

        protected override int GetHeight()
        {
            return 2500;
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
            
            for (int i = 0; i < 10; i++)
            {
                retour.Add(AsteroidFactory.GenerateAsteroid());
            }
            return retour;
        }
        protected override List<MovingObject> GetIAs(Ship playerShip)
        {
            List<MovingObject> retour = new List<MovingObject>();
            for (int i = 0; i < 5; i++)
            {
                retour.Add(SimpleEnnemyFactory.GenerateSimpleEnnemi(playerShip));
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
          
            return retour;
        }            
    }
}
