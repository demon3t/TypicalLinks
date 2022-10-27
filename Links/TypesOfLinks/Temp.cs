using Links.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Links.TypesOfLinks
{
    public class Temp : Link
    {
        public override double h { get => _h; set { _h = value; } }
        private double _h;

        public Temp(double result)
        {
            _h = result;
        }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
