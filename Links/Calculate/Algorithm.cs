using Links.Basic;
using Links.TypesOfLinks;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using static Links.Calculate.Calculate;


namespace Links.Calculate
{
    public delegate void LinkOperation(List<int> linksId, List<Link> links);

    public class Algorithm : IEnumerable<Iteration>
    {
        public List<List<int>> Links { get; private set; } = new List<List<int>>();
        public List<LinkOperation> LinkOperations { get; private set; } = new List<LinkOperation>();

        public Iteration this[int i] { get => new Iteration(Links[i], LinkOperations[i]); }

        public Algorithm(List<Link> _links, List<Vertex> _vertices)
        {
            CalculateAlgorithm(_links, _vertices);
        }

        private void CalculateAlgorithm(List<Link> links, List<Vertex> vertices)
        {
            // матрица инциденции, где строчки - звенья, столбцы - узлы 
            sbyte[,] incidentMatrix = GetIncidentMatrix(links, vertices);

            while (links.Count != 1)
            {
                for (int i = 0; i < incidentMatrix.GetLength(0); i++)
                {
                    int start = -1;
                    int end = -1;
                    List<int> readyLinks;
                    for (int j = 0; j < incidentMatrix.GetLength(1); j++)
                    {
                        if (start == -1 && incidentMatrix[i, j] == -1)
                            start = j;
                        if (end == -1 && incidentMatrix[i, j] == 1)
                            end = j;
                    }

                    // поиск последовательных звеньев
                    //if (FindConsecutiveLinks(out readyLinks, incidentMatrix, i, start, end, links))
                    //{
                    //    AddOperation(readyLinks, Multiply);
                    //    incidentMatrix = GetIncidentMatrix(links, vertices);
                    //    continue;
                    //}

                    // поиск параллельных звенье
                    if (FindParallelLinks(out readyLinks, incidentMatrix, i, start, end, links))
                    {
                        AddOperation(readyLinks, Adding);
                        incidentMatrix = GetIncidentMatrix(links, vertices);
                        continue;
                    }

                    // поиск обратных звеньев
                    if (FindReverseLinks(out readyLinks, incidentMatrix, i, start, end, links))
                    {
                        AddOperation(readyLinks, Revers);
                        incidentMatrix = GetIncidentMatrix(links, vertices);
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Поиск последовательных связанных звеньев.
        /// </summary>
        /// <param name="readyLinks"> Коллекция парралельных звеньев. </param>
        /// <param name="incidentMatrix"> Матрица инциденции. </param>
        /// <param name="index"> Индекс с какова звена идёт поиск. </param>
        /// <param name="start"> Индекс узла являющегося началом звена. </param>
        /// <param name="end"> Индекс узла являющемся концом звена. </param>
        /// <param name="links"> Коллекция звеньев. </param>
        /// <returns> true - параленые ветви найдены, false - не найдены. </returns>
        private bool FindConsecutiveLinks(out List<int> readyLinks, sbyte[,] incidentMatrix, int index, int start, int end, List<Link> links)
        {
            readyLinks = new List<int>();



            return false;
        }

        /// <summary>
        /// Поиск параллельных связанных звеньев.
        /// </summary>
        /// <param name="readyLinks"> Коллекция парралельных звеньев. </param>
        /// <param name="incidentMatrix"> Матрица инциденции. </param>
        /// <param name="index"> Индекс с какова звена идёт поиск. </param>
        /// <param name="start"> Индекс узла являющегося началом звена. </param>
        /// <param name="end"> Индекс узла являющемся концом звена. </param>
        /// <param name="links"> Коллекция звеньев. </param>
        /// <returns> true - параленые ветви найдены, false - не найдены. </returns>
        private bool FindParallelLinks(out List<int> readyLinks, sbyte[,] incidentMatrix, int index, int start, int end, List<Link> links)
        {
            bool result = false;
            readyLinks = new List<int>();

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

        /// <summary>
        /// Поиск обратно связанных звеньев.
        /// </summary>
        /// <param name="readyLinks"> Коллекция парралельных звеньев. </param>
        /// <param name="incidentMatrix"> Матрица инциденции. </param>
        /// <param name="index"> Индекс с какова звена идёт поиск. </param>
        /// <param name="start"> Индекс узла являющегося началом звена. </param>
        /// <param name="end"> Индекс узла являющемся концом звена. </param>
        /// <param name="links"> Коллекция звеньев. </param>
        /// <returns> true - параленые ветви найдены, false - не найдены. </returns>
        private bool FindReverseLinks(out List<int> readyLinks, sbyte[,] incidentMatrix, int index, int start, int end, List<Link> links)
        {
            readyLinks = new List<int>();

            for (int i = index + 1; i < incidentMatrix.GetLength(0); i++)
            {
                if (incidentMatrix[index, start] == incidentMatrix[i, end] && incidentMatrix[index, end] == incidentMatrix[i, start])
                {
                    readyLinks.Add(index);
                    readyLinks.Add(i);

                    links.RemoveAt(index);
                    links.RemoveAt(i);
                    links.Add(new Temp(0));

                    return true;
                }
            }
            return false;
        }

        private int IsStart(sbyte[,] incidentMatrix, ref int startIndex, int index)
        {
            int count = 0;
            int findIndeks = -1;
            for (int i = 0; i < incidentMatrix.GetLength(0); i++)
                if ((incidentMatrix[i, startIndex] == -1 || incidentMatrix[i, startIndex] == 1) && i != index)
                {
                    count++;
                    findIndeks = i;
                }

            if (count == 1 && incidentMatrix[findIndeks, startIndex] == 1)
                return findIndeks;

            return -1;
        }
        private int IsEnd(sbyte[,] incidentMatrix, ref int endIndex, int index)
        {
            int count = 0;
            int findIndeks = -1;
            for (int i = 0; i < incidentMatrix.GetLength(0); i++)
                if ((incidentMatrix[i, endIndex] == -1 || incidentMatrix[i, endIndex] == 1) && i != index)
                {
                    count++;
                    findIndeks = i;
                }

            if (count == 1 && incidentMatrix[findIndeks, endIndex] == -1)
                return findIndeks;

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


        public IEnumerator<Iteration> GetEnumerator()
        {
            for (int i = 0; i < LinkOperations.Count; i++)
            {
                yield return new Iteration(Links[i], LinkOperations[i]);
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class Iteration
    {
        public LinkOperation Operation;
        public List<int> LinksId;

        public Iteration(List<int> _linksId, LinkOperation _operation)
        {
            Operation = _operation;
            LinksId = _linksId;
        }
    }
}
