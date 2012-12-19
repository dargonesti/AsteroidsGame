using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsPrototype.Utilities;

using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.GameObjects
{
    /// <summary>
    /// Classe héritée de Laser représentant une boule avancant lentement et tirant des lasers
    /// </summary>
    public class SlowBall : Laser
    {
        new public const float LASER_SPEED = 5;
        public new int MAX_SPEED = 100;
        private float _ttlTotal = 3000;
        private float _degreTir = 0;
        private int _timeBetweenProjectiles = 60;
        private int _shotProjectiles = 0;//parceque,
        //gameTime.millisec % _timeBetween == 0  
        //peut dépasser et donc pas ==0 ...


        public SlowBall(MovingObject parent, int pTimeBetweenProjectiles = 100, int pTtl = 3000) : base(parent, true, pTtl)
        {
           _sprite = SpriteManager.Shield1;
            _scale = SpriteManager.scaleRateSlowBall;
            _timeBetweenProjectiles = pTimeBetweenProjectiles;

            _speed =  parent.Speed + new Microsoft.Xna.Framework.Vector2((float)Math.Cos(parent.RotationInRadians) * LASER_SPEED,
                (float)Math.Sin(parent.RotationInRadians) * LASER_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _ttl -= gameTime.ElapsedGameTime.Milliseconds;
            if ((_ttlTotal - _ttl) / _timeBetweenProjectiles > _shotProjectiles)
            {
                Laser laserTmp;
             
                    laserTmp = new Laser(this, false, 1400);

                    laserTmp.SpeedX = Laser.LASER_SPEED / 2 * (float)Math.Cos(_degreTir);// +this.SpeedX; // pkmarche pas?????
                    laserTmp.SpeedY = Laser.LASER_SPEED / 2 * (float)Math.Sin(_degreTir);// +this.SpeedY;

                    laserTmp.RotationInRadians = _degreTir ;

                    _lstToAdd.Add(laserTmp);
              
                _degreTir += (float)Math.PI * 2f / 10f;
                _shotProjectiles++;
            }
        }
        /// <summary>
        /// Si touche une cible, explose!
        /// </summary>
        override public void Die()
        {
            // 1 degree = 0.0174532925 
            //degrees = radians * (180/ Pi)
            //360 Degree = radians * 

            Laser laserTmp;
            float nbLance = 20;
            float angleRad = 0;

            for (int i = 0; i < nbLance; i++)
            {
                angleRad = (float)(i * Math.PI * 2 / nbLance); 
                laserTmp = new Laser(this, false, 900);

                laserTmp.SpeedX = Laser.LASER_SPEED * (float)Math.Cos(angleRad);
                laserTmp.SpeedY = Laser.LASER_SPEED * (float)Math.Sin(angleRad);

                laserTmp.RotationInRadians = angleRad;

                _lstToAdd.Add(laserTmp);
            }
            IsAlive = false;

            SoundManager.Play(SoundManager.ShotGun);
        }
    }
}
