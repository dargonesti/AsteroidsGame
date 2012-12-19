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
    public class Level7 : ILevel
    {

        public override String GetName()
        {
            return "More ennemies";
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
            float w = GetWidth() / 5;
            float h = GetHeight() / 5;
            List<Wall> retour = new List<Wall>();
            retour.Add(new Wall(new Vector2(w, h), new Vector2(w, 0)));
            retour.Add(new Wall(new Vector2(w, h), new Vector2(0, h)));

            retour.Add(new Wall(new Vector2(3 * w, h), new Vector2(w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, h), new Vector2(0, h)));

            retour.Add(new Wall(new Vector2(w, 4 * h), new Vector2(0, -h)));
            retour.Add(new Wall(new Vector2(w, 4 * h), new Vector2(w, 0)));

            retour.Add(new Wall(new Vector2(4 * w, 4 * h), new Vector2(-w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, 4 * h), new Vector2(0, -h)));

            return retour;
        }            
    }
}
