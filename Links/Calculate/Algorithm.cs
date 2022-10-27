using Links.Basic;
using Links.TypesOfLinks;
using System.Collections.Generic;
using static Links.Calculate.Calculate;


namespace Links.Calculate
{
    public class Algorithm
    {
        public List<List<int>> Links { get; private set; } = new List<List<int>>();


        public delegate void LinkOperation(List<int> linksId, List<Link> links, List<Vertex> vertices);
        public List<LinkOperation> LinkOperations { get; private set; } = new List<LinkOperation>();

        public Algorithm(List<Link> _links, List<Vertex> _vertices)
        {
            CalculateAlgorithm(_links, _vertices);
        }

        private void CalculateAlgorithm(List<Link> links, List<Vertex> vertices)
        {
            // матрица инциденции, где строчки - звенья, столбцы - узлы 
            sbyte[,] incidentMatrix = GetIncidentMatrix(links, vertices);

            while (incidentMatrix.GetLength(0) != 1)
            {
                for (int i = 0; i < incidentMatrix.GetLength(0); i++)
                {
                    int start = -1;
                    int end = -1;
                    List<int> readyLinks = new List<int>();
                    for (int j = 0; j < incidentMatrix.GetLength(1); j++)
                    {
                        if (start == -1 && incidentMatrix[i, j] == -1)
                            start = j;
                        if (end == -1 && incidentMatrix[i, j] == 1)
                            end = j;
                    }

                    // поиск параллельных звенье
                    if (FindParallelLinks(ref readyLinks, incidentMatrix, i, start, end, links))
                    {
                        AddOperation(readyLinks, Adding);
                        incidentMatrix = GetIncidentMatrix(links, vertices);
                        continue;
                    }

                    // поиск последовательных звеньев
                    if (FindConsecutiveLinks(ref readyLinks, incidentMatrix, i, start, end, links, vertices))
                    {
                        AddOperation(readyLinks, Multiply);
                        incidentMatrix = GetIncidentMatrix(links, vertices);
                        continue;
                    }
                }
            }
        }

        private bool FindParallelLinks(ref List<int> readyLinks, sbyte[,] incidentMatrix, int index, int start, int end, List<Link> links)
        {
            bool result = false;

            for (int i = index + 1; i < incidentMatrix.GetLength(0); i++)
            {
                if (incidentMatrix[index, start] == incidentMatrix[i, start] && incidentMatrix[index, end] == incidentMatrix[i, end])
                {
                    readyLinks.Add(i);
                    result = true;
                }
            }

            if (result)
            {
                for (int i = readyLinks.Count - 1; i >= 0; i--)
                {
                    links.RemoveAt(readyLinks[i]);
                }
                links.RemoveAt(index);
                links.Add(new Temp(0));
                readyLinks.Insert(0, index);
            }

            return result;
        }

        private bool FindConsecutiveLinks(ref List<int> readyLinks, sbyte[,] incidentMatrix, int index, int start, int end, List<Link> links, List<Vertex> vertices)
        {
            bool result = false;
            bool check = false;

            while (check)
            {
                check = false;

                int isEnd = IsEnd(incidentMatrix, ref end);
                if (isEnd != -1)
                {
                    readyLinks.Add(isEnd);
                    end = isEnd;
                    check = true;
                }

                int isStart = IsStart(incidentMatrix, ref start);
                if (isEnd != -1)
                {
                    readyLinks.Add(isStart);
                    end = isStart;
                    check = true;
                }
            }

            if (readyLinks.Count > 0)
            {
                readyLinks.Add(index);
                readyLinks.Sort();

                for (int i = readyLinks.Count - 1; i >= 0; i--)
                {
                    links.RemoveAt(readyLinks[i]);
                }


                links.Add(new Temp(1));
            }

            return result;
        }

        private int IsStart(sbyte[,] incidentMatrix, ref int startIndex)
        {
            int count = 0;
            int findIndeks = -1;
            for (int i = 0; i < incidentMatrix.GetLength(0); i++)
            {
                if (incidentMatrix[i, startIndex] == -1 || incidentMatrix[i, startIndex] == 1)
                {
                    count++;
                    findIndeks = i;
                }
            }

            if (count == 1 && incidentMatrix[findIndeks, startIndex] == -1)
            {
                return findIndeks;
            }

            return -1;
        }
        private int IsEnd(sbyte[,] incidentMatrix, ref int endIndex)
        {
            int count = 0;
            int findIndeks = -1;
            for (int i = 0; i < incidentMatrix.GetLength(0); i++)
            {
                if (incidentMatrix[i, endIndex] == -1 || incidentMatrix[i, endIndex] == 1)
                {
                    count++;
                    findIndeks = i;
                }
            }

            if (count == 1 && incidentMatrix[findIndeks, endIndex] == 1)
            {
                return findIndeks;
            }

            return -1;
        }


        private void AddOperation(List<int> useLinks, LinkOperation operation)
        {
            Links.Add(useLinks);
            LinkOperations.Add(operation);
        }
        private sbyte[,] GetIncidentMatrix(List<Link> links, List<Vertex> vertices)
        {
            sbyte[,] incidentMatrix = new sbyte[links.Count, vertices.Count];

            for (int i = 0; i < links.Count; i++)
            {
                int start = vertices.IndexOf(links[i].Start);
                if (start != -1)
                    incidentMatrix[i, start] = -1;

                int end = vertices.IndexOf(links[i].End);
                if (end != -1)
                    incidentMatrix[i, end] = 1;
            }

            return incidentMatrix;
        }
    }
}
