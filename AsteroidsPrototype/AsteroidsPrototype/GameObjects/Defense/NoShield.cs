using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsPrototype.GameObjects.Walls;

namespace AsteroidsPrototype.GameObjects.Defense
{
    /// <summary>
    /// Classe appelée par un movingObject quand touché
    /// gère rebonds sur les murs et touché par laser
    /// </summary>
    public class NoShield
    {
        protected MovingObject _parent;
        protected Texture2D _sprite = null;
        protected Color _color = Color.White;
        protected int _lifes = 0;
        public  static int MAX_LIFES = 0;
        protected List<Color> _lstCoul;
        protected Vector2 _origine;
        protected List<Wall> _wallsTouched;
        protected Boolean _weapon = false;

        #region "accessors"
        public int Lifes
        {
            get { return _lifes; }
            set { _lifes = value; }
        }
        public Boolean isWeapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        #endregion
        public NoShield(MovingObject pParent, Boolean pWeapon = false)
        {
            _parent = pParent;
            _weapon = pWeapon;
            _wallsTouched = new List< Wall>();
        }

        public virtual void TouchWall(Wall pWall)
        {
            //float rotTmp = _parent.RotationInDegrees;
            
            Vector2 AP = _parent.Position - pWall.Position;
            Vector2 AB = pWall.Direction;
            float ab2 = AB.X * AB.X + AB.Y * AB.Y;
            float ap_ab = AP.X * AB.X + AP.Y * AB.Y;
            float t = ap_ab / ab2;
           
            if (true)
            {
                if (t < 0.0f) t = 0.0f;
                else if (t > 1.0f) t = 1.0f;
            }
            Vector2 Closest = pWall.Position + AB * t;
            AB = Closest - _parent.Position ;
            AP = pWall.Direction;
            AP.Normalize();
            if (Math.Abs(AB.Length()) < _parent.Sprite.Width * _parent.scale / 2)
            {
                if (!_wallsTouched.Contains(pWall))
                {
                    AB.Normalize();
                    _parent.Speed = 2 * (Vector2.Dot(_parent.Speed, AP)) * AP - _parent.Speed;
                    if (typeof(EnnemyShip).IsAssignableFrom(_parent.GetType()))
                    {
                        _parent.RotationInRadians = (float)Math.Atan2((double)_parent.Speed.Y, (double)_parent.Speed.X);
                    }
                    _wallsTouched.Add(pWall);
                    if (_weapon)
                    {
                        _parent.Die();
                    }
                }
            }
            else if (_wallsTouched.Contains(pWall))
            {
                _wallsTouched.Remove(pWall);
            }
            //_parent.RotationInDegrees = rotTmp;
        }

        public virtual Vector2 GetClosetPoint(Vector2 A, Vector2 B, Vector2 P, bool segmentClamp)
        {
            Vector2 AP = P - A;
            Vector2 AB = B - A;
            float ab2 = AB.X*AB.X + AB.Y*AB.Y;
            float ap_ab = AP.X*AB.X + AP.Y*AB.Y;
            float t = ap_ab / ab2;
            if (segmentClamp)
            {
                 if (t < 0.0f) t = 0.0f;
                 else if (t > 1.0f) t = 1.0f;
            }
            Vector2 Closest = A + AB * t;
            return Closest;
        }

        public virtual void Touch()
        {
             _parent.Die();
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 pOrigin, float pScale)
        { }

        public object pWallP { get; set; }
    }
}
