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
    public class Level2
        :ILevel
    {

        public override String GetName()
        {
            return "Power Testing";
        }
        protected override int GetWidth()
        {
            return 2000;
        }

        protected override int GetHeight()
        {
            return 2000;
        }

        protected override List<Powerup> GetPowerUps()
        {
            float w = GetWidth() / 5;
            float h = GetHeight() / 5;
            List<Powerup> lstRetour = new List<Powerup>();
            ShotgunPowerup shotgun = new ShotgunPowerup();
            FrostOrbPowerup frostOrb = new FrostOrbPowerup();
            TimeFreezePowerup timeFreeze = new TimeFreezePowerup();
            RandomPowerup rand = new RandomPowerup();
            WeaponsLockPowerup wpLock = new WeaponsLockPowerup();
            ShieldPowerup shield = new ShieldPowerup();

            frostOrb.Position = new Vector2(w+100, h+100);
            shotgun.Position = new Vector2(4*w - 100, h + 100);
            timeFreeze.Position = new Vector2(GetWidth()/2- 111, GetHeight()/2 -111);
            rand.Position = new Vector2(GetWidth() / 2, GetHeight() / 2  + 100);
            wpLock.Position = new Vector2(w + 100, 4 *h - 100);
            shield.Position = new Vector2(4*w - 100, 4*h - 100);

            List<Wall> retour = new List<Wall>();
            retour.Add(new Wall(new Vector2(w, h), new Vector2(w, 0)));
            retour.Add(new Wall(new Vector2(w, h), new Vector2(0, h)));

            retour.Add(new Wall(new Vector2(3 * w, h), new Vector2(w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, h), new Vector2(0, h)));

            retour.Add(new Wall(new Vector2(w, 4 * h), new Vector2(0, -h)));
            retour.Add(new Wall(new Vector2(w, 4 * h), new Vector2(w, 0)));

            retour.Add(new Wall(new Vector2(4 * w, 4 * h), new Vector2(-w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, 4 * h), new Vector2(0, -h)));


            lstRetour.Add(shotgun);
            lstRetour.Add(frostOrb);
            lstRetour.Add(timeFreeze);
            lstRetour.Add(rand);
            lstRetour.Add(wpLock);
            lstRetour.Add(shield);
            return lstRetour;
        }
        protected override  Ship GetShip()
        {
            Ship ship;
            ship = new Ship();
            ship.XPosition = GetWidth() / 2;
            ship.YPosition = GetHeight() / 2;
            ship.Moove = new GameObjects.Moving.MovingBase(ship);
            ship.Moove.Friction = 0.01f;
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
       
          
            return retour;
        }


        protected override List<MovingObject> GetAlies()
        {
            List<MovingObject> retour = new List<MovingObject>();

            return retour;
        }
        protected override List<Wall> GetWalls()
        {
            float w = GetWidth()/5;
            float h = GetHeight()/5;
            List<Wall> retour = new List<Wall>();
            retour.Add(new Wall(new Vector2(w, h), new Vector2(w, 0)));
            retour.Add(new Wall(new Vector2(w, h), new Vector2(0, h)));

            retour.Add(new Wall(new Vector2(3 * w, h), new Vector2( w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, h), new Vector2( 0, h)));

            retour.Add(new Wall(new Vector2(w, 4*h), new Vector2(0, -h)));
            retour.Add(new Wall(new Vector2(w, 4 * h), new Vector2(w, 0)));

            retour.Add(new Wall(new Vector2(4 * w, 4*h), new Vector2( -w, 0)));
            retour.Add(new Wall(new Vector2(4 * w, 4*h), new Vector2( 0, -h)));

            return retour;
        }            
    }
}
