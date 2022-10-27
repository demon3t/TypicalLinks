using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Дифференцирующее звено
    /// </summary>
    public class Differentiating : Link
    {
        public override double h { get => K / T * Math.Exp(-i / T); set { } }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
