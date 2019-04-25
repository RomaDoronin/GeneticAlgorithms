using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class GraphOperation
    {
        public static SymmetricMatrix GetMaxSpeedMatrix(SymmetricMatrix speedMatrix)
        {
            SymmetricMatrix timeMatrix = new SymmetricMatrix(speedMatrix.GetMatrixSize());
            for (int i = 0; i < speedMatrix.GetMatrixSize(); i++)
            {
                for (int j = 0; j < speedMatrix.GetMatrixSize(); j++)
                {
                    if (speedMatrix.GetVal(i, j) != 0)
                    {
                        timeMatrix.SetVal(i, j, 1 / speedMatrix.GetVal(i, j));
                    }
                    else
                    {
                        timeMatrix.SetVal(i, j, 0);
                    }
                }
            }

            SymmetricMatrix minTimeMatrix = new SymmetricMatrix(speedMatrix.GetMatrixSize());
            for (int i = 0; i < speedMatrix.GetMatrixSize(); i++)
            {
                DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(new SumFunctionStd());
                List<double> timeList = dijkstraAlgorithm.DoDijkstraAlgorithm(timeMatrix, i);

                for (int j = 0; j < timeList.Count; j++)
                {
                    minTimeMatrix.SetVal(i, j, timeList[j]);
                }
            }

            SymmetricMatrix maxSpeedMatrix = new SymmetricMatrix(speedMatrix.GetMatrixSize());
            for (int i = 0; i < speedMatrix.GetMatrixSize(); i++)
            {
                for (int j = 0; j < speedMatrix.GetMatrixSize(); j++)
                {
                    if (minTimeMatrix.GetVal(i, j) != 0)
                    {
                        maxSpeedMatrix.SetVal(i, j, 1 / minTimeMatrix.GetVal(i, j));
                    }
                    else
                    {
                        maxSpeedMatrix.SetVal(i, j, 0);
                    }
                }
            }

            return maxSpeedMatrix;
        }

        public static SymmetricMatrix GetShortestDistancesMatrix(SymmetricMatrix distancesMatrix)
        {
            SymmetricMatrix shortestDistancesMatrix = new SymmetricMatrix(distancesMatrix.GetMatrixSize());

            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(new SumFunctionStd());
                List<double> distanceList = dijkstraAlgorithm.DoDijkstraAlgorithm(distancesMatrix, i);

                for (int j = 0; j < distanceList.Count; j++)
                {
                    shortestDistancesMatrix.SetVal(i, j, distanceList[j]);
                }
            }
            
            return shortestDistancesMatrix;
        }
        
        /// <summary>
        /// Рекурсивная часть проверки что в графе нет циклов
        /// </summary>
        /// <param name="distancesMatrix">Матриц растояний</param>
        /// <param name="visitedVertex">Массив посещенных вершин</param>
        /// <param name="previousVertex">Предыдущая вершина</param>
        /// <param name="currVertex">текущая вершина</param>
        /// <returns></returns>
        private static bool CheckNoCyclesInGraph(SymmetricMatrix distancesMatrix, ref List<bool> visitedVertex, int previousVertex, int currVertex)
        {
            visitedVertex[currVertex] = true;
            for (int j = 0; j < distancesMatrix.GetMatrixSize(); j++)
            {
                if (j != previousVertex && j != currVertex)
                {
                    if (distancesMatrix.GetVal(currVertex, j) != 0)
                    {
                        if (visitedVertex[j] == true)
                        {
                            return false;
                        }
                        else
                        {
                            if (!CheckNoCyclesInGraph(distancesMatrix, ref visitedVertex, currVertex, j))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (previousVertex == currVertex)
            {
                for (int vertex = currVertex; vertex < visitedVertex.Count; vertex++)
                {
                    if (!visitedVertex[vertex])
                    {
                        if (!CheckNoCyclesInGraph(distancesMatrix, ref visitedVertex, vertex, vertex))
                        {
                            return false;
                        }
                        break;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Проверка что в граф нет циклов. Граф может быть не связным
        /// </summary>
        public static bool CheckNoCyclesInGraph(SymmetricMatrix distancesMatrix)
        {
            List<bool> visitedVertex = new List<bool>();
            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
            }

            return CheckNoCyclesInGraph(distancesMatrix, ref visitedVertex, 0, 0);
        }
        
        private static void BypassInDepthRec(SymmetricMatrix distancesMatrix, ref List<bool> visitedVertex, int currVertex)
        {
            visitedVertex[currVertex] = true;

            for (int vertexCount = 0; vertexCount < distancesMatrix.GetMatrixSize(); vertexCount++)
            {
                if (!visitedVertex[vertexCount])
                {
                    if (0 != distancesMatrix.GetVal(currVertex, vertexCount))
                    {
                        BypassInDepthRec(distancesMatrix, ref visitedVertex, vertexCount);
                    }
                }
            }
        }

        /// <summary>
        /// Проверка что граф является связным
        /// </summary>
        /// <param name="distancesMatrix"></param>
        /// <param name="visitedVertex"></param>
        /// <param name="currVertex"></param>
        public static bool CheckGraphIsConnected(SymmetricMatrix distancesMatrix)
        {
            List<bool> visitedVertex = new List<bool>();
            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
            }

            BypassInDepthRec(distancesMatrix, ref visitedVertex, 0);

            foreach (var vertex in visitedVertex)
            {
                if (!vertex)
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Проверка что граф является остовным
        /// </summary>
        /// <param name="distancesMatrix"></param>
        /// <returns></returns>
        public static bool CheckGraphIsSkeleton(SymmetricMatrix distancesMatrix)
        {
            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                bool isBreak = false;

                for (int j = 0; j < distancesMatrix.GetMatrixSize(); j++)
                {
                    if (distancesMatrix.GetVal(i, j) != 0)
                    {
                        isBreak = true;
                        break;
                    }
                }

                if (!isBreak)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
