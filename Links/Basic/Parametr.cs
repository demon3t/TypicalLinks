using System;
using System.Collections.Generic;
using System.Text;

namespace Links.Basic
{
    public class Parametr
    {
        public string Name { get; set; }
        public double Value { get; set; } 

        public Parametr(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
