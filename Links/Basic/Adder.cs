using System;
using System.Collections.Generic;
using System.Text;

namespace Links.Basic
{
    public class Adder : Vertex
    {
        public bool Top { get; set; } = true;

        public bool Right { get; set; } = true;

        public bool Bottom { get; set; } = true;

        public bool Left { get; set; } = true;

        public Adder(int id) : base(id)
        {

        }
    }
}
