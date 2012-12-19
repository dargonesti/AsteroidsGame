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

namespace AsteroidsPrototype.GameObjects.Defense
{
    /// <summary>
    /// Classe héritée de NoShield mais donnant un champ de force comme bouclier
    /// </summary>
    public class DefenseBase : NoShield
    {
        new public static int MAX_LIFES = 5;


        public DefenseBase(MovingObject pParent) : base(pParent)
        {
            _color = Color.White;
            _lifes = 5;
            _color = new Color(0,255,0);
            _lstCoul = new List<Color>() { Color.White, Color.Aqua, Color.Blue, Color.Purple, Color.Red };

            _parent = pParent;
            _sprite = SpriteManager.Shield1;
            _origine = new Vector2(_sprite.Width, _sprite.Height) / 2;// *SpriteManager.scaleRateShield / 2;
        }


        override public void Touch()
        {
            if (--_lifes <= 0)
            {
                _parent.Die();
            }
        }

        override public void Draw(SpriteBatch spriteBatch, Vector2 pOrigin, float pScale)
        {
            //spriteBatch.Draw(_sprite, _position - GameManager.GameManager._CameraPosition, null, Color.White, RotationInRadians, _origin, _scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(_sprite, _parent.Position /*- GameManager.GameManager._CameraPosition*/, null, (_lstCoul.Count >= _lifes && _lifes > 0 ? _lstCoul[(MAX_LIFES - _lifes) ] : Color.Yellow) * 0.5f, 0, _origine, SpriteManager.scaleRateShield, SpriteEffects.None, 0f);
        }
    }
}
