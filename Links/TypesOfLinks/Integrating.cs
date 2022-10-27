using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Интегрирующее звено
    /// </summary>
    public class Integrating : Link
    {
        public override double h { get => K * i - K * T * (1 - Math.Exp(-i / T)); set { } }
        public override string ToString()
        {
            return Id.ToString();
        }

    }
}
