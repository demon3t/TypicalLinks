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
        public static double GetValue(Algorithm algorithm, List<Link> links, List<Vertex> vertices)
        {
            for (int i = 0; i < algorithm.LinkOperations.Count; i++)
            {
                algorithm.LinkOperations[i](algorithm.Links[i],links,vertices);
            }

            return links[0].h;
        }

        internal static void Multiply(List<int> linksId, List<Link> links, List<Vertex> vertices)
        {
            //var _links = new List<Link>();

            //foreach (var link in links)
            //{
            //    var _findedLinks = links.FindAll(x =>
            //    x.Start == link.End &&
            //    !x.IsEquivalent &&
            //    !link.IsEquivalent);

            //    if (_findedLinks.Count == 1)
            //    {
            //        _links.Add(new Temp(_findedLinks.First().h * link.h)
            //        {
            //            Start = link.Start,
            //            End = _findedLinks.First().End,
            //            IsEquivalent = false
            //        });

            //        _findedLinks.First().IsEquivalent = true;
            //        link.IsEquivalent = true;

            //        link.End.IsEquivalent = true;
            //    }
            //}
            //links.AddRange(_links);
            //links.RemoveAll(x => x.IsEquivalent);
            //vertices.RemoveAll(x => x.IsEquivalent);
        }

        internal static void Adding(List<int> linksId, List<Link> links, List<Vertex> vertices)
        {
            var temp = new Temp(0);
            foreach (int n in linksId)
            {
                temp.h += links[n].h;
            }
            for (int i = linksId.Count - 1; i > 0; i--)
            {
                links.RemoveAt(linksId[i]);
            }
            links.Add(temp);
        }
    }
}
