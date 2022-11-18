using Links.Basic;
using Links.TypesOfLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Links.Calculate
{
    public static class Calculate
    {
        public static double GetValue(Algorithm algorithm, List<Link> links)
        {

            foreach (Iteration iteration in algorithm)
            {
                iteration.Operation(iteration.LinksId, links);
            }
            return links[0].h;
        }

        internal static void Multiply(List<int> linksId, List<Link> links)
        {
            var temp = new Temp(1);
            foreach (int n in linksId)
            {
                temp.h *= links[n].h;
            }
            for (int i = linksId.Count - 1; i > 0; i--)
            {
                links.RemoveAt(linksId[i]);
            }
            links.Add(temp);
        }

        internal static void Adding(List<int> linksId, List<Link> links)
        {
            var temp = new Temp(0);
            foreach (int n in linksId)
            {
                temp.h += links[n].IsAdding ? links[n].h : -links[n].h;
            }
            for (int i = linksId.Count - 1; i > 0; i--)
            {
                links.RemoveAt(linksId[i]);
            }
            links.Add(temp);
        }

        internal static void Revers(List<int> linksId, List<Link> links)
        {
            var temp = new Temp(0);
            temp.h += links[1].IsAdding ? links[1].h / (1 - links[0].h * links[1].h) : links[1].h / (1 + links[0].h * links[1].h);

            links.RemoveAt(linksId[1]);
            links.RemoveAt(linksId[0]);

            links.Add(temp);
        }
    }
}
