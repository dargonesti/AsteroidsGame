// Permet d'afficher les différentes modifications de score à l'écran

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AsteroidsPrototype.Utilities
{
    /// <summary>
    /// Classe pour afficher et déplacer les scores dans la scène
    /// </summary>
    public class Score
    {
        public Vector2 _pos;
        public int _max;
        public string _value;
        public bool _display;
        public int _alpha = 250;

        /// <summary>
        ///  Constructeur
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        public Score(Vector2 pos, int max, int value)
        {
            _pos = pos;
            _max = max - 150;
            _value = value.ToString();
            _display = true;
            _alpha = 250;
        }
        
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        public Score(Vector2 pos, int max, String value)
        {
            _pos = pos;
            _max = max - 150;
            _value = value;
            _display = true;
            _alpha = 250;
        }

        ///
        /// Affiche le texte flottant associé au score
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public virtual void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, _value, _pos, Color.White * ((float)_alpha / 250f), 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
