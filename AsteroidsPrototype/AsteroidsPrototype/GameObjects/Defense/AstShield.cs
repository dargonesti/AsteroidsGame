using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsPrototype.GameObjects.Defense
{
    /// <summary>
    /// Classe héritée de NoShield
    /// </summary>
    public class AstShield : NoShield
    {
        public AstShield(MovingObject pParent)
            : base(pParent)
        {

        }
        public override void Touch()
        {
            base.Touch();
        }
    }
}
