using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects.Weapons
{
    /// <summary>
    /// Classe principale de gestion d'Armes d'un Ship
    /// </summary>
    public class Weapon
    {
        protected MovingObject _parent;
        protected long _timeSinceShot;
        protected float TIME_TO_SHOOT = 1000;
        private float _multiplicateurVitesse = 1;
        protected Texture2D _icon = null;
        protected int _ttl ;

        public int TTL
        {
            get { return _ttl; }
            set { _ttl = value; }
        }
        public Texture2D Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        protected float MultiplicateurVitesse
        {
            get { return _multiplicateurVitesse; }
            set { _multiplicateurVitesse = value; }
        }

        /// <summary>
        /// Constructeur de base 
        /// </summary>
        /// <param name="pParent">Le Ship qui tirera </param>
        /// <param name="pTtl">millisecondes où le laser restera en vie</param>
        public Weapon(MovingObject pParent, int pTtl = 500)
        {
            _ttl = pTtl;
            _icon = SpriteManager.IconLaser;
            _parent = pParent;
            _timeSinceShot = DateTime.Now.Ticks / 1000;
            _multiplicateurVitesse = 1;
        }

        /// <summary>
        /// Regarde si c'est le temps de tirer (avec TIME_TO_SHOT * _multiplicateuritesse)
        /// sI oui, tire avec Shoot() qui est overridable
        /// </summary>
        /// <param name="lstLasers">Liste de lasers du gameManager passée en référence</param>
        public  void CheckShoot(ref List<MovingObject> lstLasers)
        {
            if ((DateTime.Now.Ticks / 1000 - _timeSinceShot) > TIME_TO_SHOOT)
            {
                Shoot(ref lstLasers);
                _timeSinceShot = DateTime.Now.Ticks / 1000;
            }
        }

        /// <summary>
        /// Méthode overridable pour tirer un laser.
        /// </summary>
        /// <param name="lstLasers">Liste de lasers du gameManager passée en référenc</param>
        protected virtual void Shoot(ref List<MovingObject> lstLasers)
        {
            lstLasers.Add(new Laser(_parent, true, _ttl));
            _timeSinceShot = DateTime.Now.Ticks / 1000;
            if (_parent is EnnemyShip)
            {
                //SoundManager.Play(SoundManager.LaserShorter, 0.2f);
            }
            else
                SoundManager.Play(SoundManager.LaserShorter, 0.2f);
        }
        
    }
}
