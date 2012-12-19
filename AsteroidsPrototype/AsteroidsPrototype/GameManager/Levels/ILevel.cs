using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.GameObjects.Weapons;
using AsteroidsPrototype.GameObjects.Walls;
using AsteroidsPrototype.GameObjects;
using AsteroidsPrototype.GameObjects.Powerups;
using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameManager.Levels
{
    /// <summary>
    /// Classe abstraite pour créer des niveaux
    /// </summary>
    public abstract class ILevel
    {
        /// <summary>
        /// Focntions à overrider pour définir quels objects contiendra le Niveau.
        /// </summary>
        /// <returns></returns>
        protected abstract  List<Wall> GetWalls();
        protected abstract Ship GetShip();
        protected abstract List<MovingObject> GetAsteroids();
        protected abstract List<MovingObject> GetIAs(Ship playerShip);
        protected abstract List<MovingObject> GetAlies();
        protected abstract List<Powerup> GetPowerUps();
        
        protected abstract int GetWidth();
        protected abstract int GetHeight();

        public virtual String GetName()
        {
            return "Name";
        }
        /// <summary>
        /// Si dans une boite ou si passe de 0 à maxWidth() en position
        /// </summary>
        /// <returns></returns>
        protected virtual Boolean GetWorldMode(){
            return false;
        }

        /// <summary>
        /// Si le niveau est un sideScroller
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsSideScroller()
        {
            return false;
        }

        /// <summary>
        /// Appelé par gameManager avec en référence les listes a remplir
        /// </summary>
        /// <param name="_ennemies"></param>
        /// <param name="_allies"></param>
        /// <param name="_walls"></param>
        /// <param name="_powerUps"></param>
        /// <param name="_ship"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        public void InitializeWorld(ref List<MovingObject> _ennemies, ref List<MovingObject> _allies, ref List<Wall> _walls, ref List<Powerup> _powerUps, ref Ship _ship, ref int _width, ref int _height)
        {
            _width = GetWidth();
            _height = GetHeight();
            GameManager.InfiniteWorld = GetWorldMode();
            _ship = GetShip();

            _allies = new List<MovingObject>();

            _ennemies = GetAsteroids();

            _ennemies.AddRange(GetIAs(_ship));

            _powerUps = GetPowerUps();

            _walls = GetWalls();
            if(!GameManager.InfiniteWorld){
                _walls.Add(new Wall(new Vector2(0, 0), new Vector2(GetWidth(), 0)));
                _walls.Add(new Wall(new Vector2(0, 0), new Vector2(0, GetHeight())));
                _walls.Add(new Wall(new Vector2(0, GetHeight()), new Vector2(GetWidth(), 0)));
                _walls.Add(new Wall(new Vector2(GetWidth(), 0), new Vector2(0, GetHeight())));
            }

        }
    }
}
