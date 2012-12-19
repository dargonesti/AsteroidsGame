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
    class Level6:ILevel
    {

        public override String GetName()
        {
            return "Side scroller";
        }
        public override Boolean IsSideScroller()
        {
            return true;
        }

        protected override int GetWidth()
        {
            return 12000;
        }

        protected override int GetHeight()
        {
            return 1000;
        }
        protected override List<Powerup> GetPowerUps()
        {
            List<Powerup> lstRetour = new List<Powerup>();
            ShotgunPowerup shotgun = new ShotgunPowerup();
            shotgun.Position = new Vector2(5000, 500);
            lstRetour.Add(shotgun);

            return lstRetour;
        }

        protected override  Ship GetShip()
        {
            Ship ship;
            ship = new Ship();
            ship.Moove = new GameObjects.Moving.MovingSideScroller(ship);
            ship.XPosition = 200;
            ship.YPosition = GetHeight() / 2;
            return ship;
        }

        protected override List<MovingObject> GetAsteroids()
        {
            List<MovingObject> retour = new List<MovingObject>();
            
            for (int i = 0; i < 10; i++)
            {
                retour.Add(AsteroidFactory.GenerateAsteroid(1,2));
            }
            return retour;
        }
        protected override List<MovingObject> GetIAs(Ship playerShip)
        {
            List<MovingObject> retour = new List<MovingObject>();
            for (int i = 0; i < 30 ; i++)
            {
                retour.Add(SimpleEnnemyFactory.GenerateCirclingEnnemi(i*300+1500,(200 + i*250)% 600 + 200,i * 12));
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
