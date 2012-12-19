using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Weapons
{
    /// <summary>
    /// Classe héritée de Weapon qui tir 5 lasers en meme temps plutot que 1
    /// </summary>
    public class ShotGun : Weapon
    {
        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="pParent"></param>
        /// <param name="pTtl"></param>
        public ShotGun( MovingObject pParent, int pTtl = 400)
            : base( pParent, pTtl)
        {
            TIME_TO_SHOOT = 2500;
            _icon = SpriteManager.IconShotGun;
         }

        /// <summary>
        /// Tir 8 lasers avec un angle
        /// </summary>
        /// <param name="lstLasers">liste de moving object où ajouter les lasers tirés</param>
        override protected void Shoot(ref List<MovingObject> lstLasers)
        {
            Laser laser;

            ////Pour un kindof shotGun en fait
            for (int i = -4; i < 4; i++)
            {
                laser = new Laser(_parent, false, _ttl);
                  
                laser.SpeedX = Laser.LASER_SPEED * (float)Math.Cos(_parent.RotationInRadians - (float)i / (float)12);
                laser.SpeedY = Laser.LASER_SPEED * (float)Math.Sin(_parent.RotationInRadians - (float)i / (float)12);
                    
                laser.Speed += _parent.Speed;
                lstLasers.Add(laser);
            }

            if (_parent is EnnemyShip)
            {
                //SoundManager.Play(SoundManager.ShotGun, 0.2f);
            }
            else
                SoundManager.Play(SoundManager.ShotGun, 0.2f);
        }
    }
}
