using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class GraphOperation
    {
        // Алгоритм Дейкстры
        private static void DijkstraAlgorithmRec(SymmetricMatrix distancesMatrix, ref List<bool> visitedVertex, ref List<int> shortestDistancesFromVertex, int currVertex, int startVertexNum)
        {
            visitedVertex[currVertex] = true;
            int min = Program.INFINITY;
            int index = -1;

            for (int j = 0; j < distancesMatrix.GetMatrixSize(); j++)
            {
                int dist = distancesMatrix.GetVal(currVertex, j);
                if (dist != 0 && j != currVertex)
                {
                    int allDist = dist + shortestDistancesFromVertex[currVertex];
                    if (allDist < shortestDistancesFromVertex[j])
                    {
                        shortestDistancesFromVertex[j] = allDist;
                    }
                }

                if (min > shortestDistancesFromVertex[j] && !visitedVertex[j])
                {
                    min = shortestDistancesFromVertex[j];
                    index = j;
                }
            }

            if (index != -1)
            {
                DijkstraAlgorithmRec(distancesMatrix, ref visitedVertex, ref shortestDistancesFromVertex, index, startVertexNum);
            }
        }

        private static List<int> DijkstraAlgorithm(SymmetricMatrix distancesMatrix, int startVertexNum)
        {
            List<bool> visitedVertex = new List<bool>();
            List<int> shortestDistancesFromVertex = new List<int>();

            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
                shortestDistancesFromVertex.Add(Program.INFINITY);
            }
            shortestDistancesFromVertex[startVertexNum] = 0;

            DijkstraAlgorithmRec(distancesMatrix, ref visitedVertex, ref shortestDistancesFromVertex, startVertexNum, startVertexNum);

            return shortestDistancesFromVertex;
        }

        public static SymmetricMatrix GetShortestDistancesMatrix(SymmetricMatrix distancesMatrix)
        {
            SymmetricMatrix shortestDistancesMatrix = new SymmetricMatrix(distancesMatrix.GetMatrixSize());

            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                List<int> distanceList = DijkstraAlgorithm(distancesMatrix, i);

                for (int j = 0; j < distanceList.Count; j++)
                {
                    shortestDistancesMatrix.SetVal(i, j, distanceList[j]);
                }
            }

            return shortestDistancesMatrix;
        }

        // Проверка что граф является деревом // Хотя на самом деле проверка что нет циклов
        private static bool CheckGraphIsTreeRec(SymmetricMatrix distancesMatrix, ref List<bool> visitedVertex, int previousVertex, int currVertex)
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
                            if (!CheckGraphIsTreeRec(distancesMatrix, ref visitedVertex, currVertex, j))
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
                        if (!CheckGraphIsTreeRec(distancesMatrix, ref visitedVertex, vertex, vertex))
                        {
                            return false;
                        }
                        break;
                    }
                }
            }

            return true;
        }

        public static bool CheckGraphIsTree(SymmetricMatrix distancesMatrix)
        {
            List<bool> visitedVertex = new List<bool>();
            for (int i = 0; i < distancesMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
            }

            return CheckGraphIsTreeRec(distancesMatrix, ref visitedVertex, 0, 0);
        }

        // Проверка что граф является связным
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

        // Проверка что граф является остовным
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
