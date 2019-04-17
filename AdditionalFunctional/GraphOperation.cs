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
        private static List<int> DijkstraAlgorithm(SymmetricMatrix distancesMatrix, int vertex)
        {
            throw new NotImplementedException();
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

        // Проверка что граф является деревом
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
