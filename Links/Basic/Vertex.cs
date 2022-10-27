using System;
using System.Collections.Generic;
using System.Text;

namespace Links.Basic
{
    public class Vertex
    {
        internal int Id { get;}
        public bool IsParametr
        {
            get { return _isParametr; }
            set
            {
                _isParametr = value;
                Parametr = new Parametr("Name", 0);
            }
        }
        private bool _isParametr;
        public Parametr Parametr { get; set; }
        public bool IsFunction { get; set; }
        public bool IsEquivalent { get; set; }

        public Vertex( int id)
        {
            Id = id;
        }





        public static bool operator ==(Vertex v1, Vertex v2)
        {
            return v1.Equals(v2);
        }
        public static bool operator !=(Vertex v1, Vertex v2)
        {
            return !v1.Equals(v2);
        }
        public override bool Equals(object obj)
        {
            return obj is Vertex vertex &&
                   Id.Equals(vertex.Id);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IsParametr, IsFunction);
        }
    }
}
