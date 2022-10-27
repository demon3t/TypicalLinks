using System;
using Links.Basic;

namespace Links.TypesOfLinks
{
    /// <summary>
    /// Пропорциональное звено
    /// </summary>
    public class Proportional : Link
    {
        public override double h { get => K * 1; set { } }
        public override string ToString()
        {
            return Id.ToString();
        }


    }
}
