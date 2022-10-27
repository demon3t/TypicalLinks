using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Колебательно звено
    /// </summary>
    public class Oscillatory : Link
    {
        public override double h {get => 1; set { } }
        public override string ToString()
        {
            return Id.ToString();
        }

    }
}
