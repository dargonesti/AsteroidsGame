using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AsteroidsPrototype.Utilities
{
    /// <summary>
    /// Classe contenant les fonctions ayant à être appelée à plusieurs places (conversions, enums, etc.)
    /// </summary>
    static class Utilitaire
    {
        public static Random _random = new Random();
        public static float DiffVec2(Vector2 pVec)
        {
            return Math.Abs(pVec.X) + Math.Abs(pVec.Y);
        }

        public static float RotToAimRad(Vector2 PosShooter, Vector2 SpeedShooter, Vector2 posTarget,Vector2 speedTarget, float speedProjectil)
        {
            double tx = posTarget.X - PosShooter.X;
            double ty = posTarget.Y - PosShooter.Y;

            double a = speedTarget.X * speedTarget.X + speedTarget.Y * speedTarget.Y - speedProjectil * speedProjectil;
            double b = 2 * (speedTarget.X * tx + speedTarget.Y * ty);
            double c = tx * tx + ty * ty;

            double tt = 333;

            Object result = quad(a, b, c);
            if (result != null)
            {
                double t1 = ((Vector2)result).X;
                double t2 = ((Vector2)result).Y;
                tt = Math.Min(t1, t2);
                t1 = Math.Max(t2, t1);
                t2 = tt;

                tt = (t2 > 0 ? t2 : t1);
               t1 = posTarget.X + speedTarget.X * tt/1;
                t2 = posTarget.Y + speedTarget.Y * tt / 1;

                tt = (float)Math.Atan2(t2 - PosShooter.Y, t1 - PosShooter.X);
            }

            return (float)tt;
        }

        /// <summary>
        /// retourne null ou un Vector2 de la position d'impacte
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>null ou un Vector2 de la position d'impacte</returns>
        public static Object quad(double a, double b, double c)
        {
            Object retour = Vector2.Zero;
            if (a == 0)
            {
                if (b == 0)
                {
                    if (c == 0)
                    {
                        retour = new Vector2(0, 0);
                    }
                    else
                    {
                        retour = null;
                    }
                }
                else
                {
                    retour = new Vector2((float)(-c / b),(float)( -c / b));
                }
            }
            else
            {
                double disc = b * b - 4 * a * c;
                if (disc >= 0)
                {
                    disc = Math.Sqrt(disc);
                    a = 2 * a;
                    retour = new Vector2((float)((-b - disc) / a),(float)( (-b + disc) / a));
                }
            }
            return retour;
        }
    }

    enum TypeObjet
    {
        Player,
        Asteroid,
        EnemiShip,
        EnemiLaser,
        Wall,
        Laser,
        Null
    }

    // Permet de centraliser la valeur des différents objets qui affectent le score
    public class TypeScore
    {
        public const int Asteroid = 100;
        public const int EnemiShip = 250;
        public const int Hit = -100;
    }
}
