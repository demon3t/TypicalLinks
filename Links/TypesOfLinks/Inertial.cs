using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Инерционное звено
    /// </summary>
    public class Inertial : Link
    {
        public override double h {get => K * (1 - Math.Exp(-i / T)); set { } }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
