using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Weapons
{
    /// <summary>
    /// Classe héritée de Weapon qui tir un orbe qui tire des lasers en avancant ou quand il meurt
    /// </summary>
    public class FrostOrb : Weapon
    {

        public FrostOrb(MovingObject pParent, int pTtl = 3000)
            : base( pParent, pTtl)
        {
            TIME_TO_SHOOT = 2500;
            _icon = SpriteManager.IconOrbre;
         }

        /// <summary>
        /// Tir unOrbe qui tirera des lasers en avancant
        /// </summary>
        /// <param name="lstLasers">liste de moving object où ajouter les lasers tirés</param>
        override protected void Shoot(ref List<MovingObject> lstLasers)
        {
            SlowBall  laser;

            laser = new SlowBall(_parent, 100, TTL);
                                      
            // laser.Speed += _parent.Speed;
            lstLasers.Add(laser);
            if (_parent is EnnemyShip)
            {
                //SoundManager.Play(SoundManager.LaserHeavy, 0.2f);
            }
            else
                SoundManager.Play(SoundManager.LaserHeavy, 0.2f);
        }
    }
}
