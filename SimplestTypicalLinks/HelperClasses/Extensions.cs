using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SimplestTypicalLinks.HelperClasses
{
    internal static class Extensions
    {
        public static double[] Xs(this PointCollection points)
        {
            double[] xs = new double[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                xs[i] = points[i].X;
            }
            return xs;
        }

        public static double[] Ys(this PointCollection points)
        {
            double[] ys = new double[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                ys[i] = points[i].Y;
            }
            return ys;
        }
    }
}
