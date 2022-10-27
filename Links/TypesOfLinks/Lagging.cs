using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Запаздывабщее звено
    /// </summary>
    public class Lagging : Link
    {
        public override double h { get => 1; set { } }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
