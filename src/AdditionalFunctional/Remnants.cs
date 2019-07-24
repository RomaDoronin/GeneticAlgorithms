using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Remnants
    {
        /// <summary>
        /// Перевод набор вершин в остатки
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="sizeInitialEdgeSet"></param>
        /// <param name="edgeNum"></param>
        /// <returns></returns>
        public static List<int> EdgeToRemnants(VectorSolutionDouble solution, int sizeInitialEdgeSet, int edgeNum)
        {
            List<int> remnantList = new List<int>();
            remnantList.Add((int)solution.GetResult()[0]);

            for (int i = 1; i < solution.GetResult().Count; i++)
            {
                remnantList.Add((int)solution.GetResult()[i] - (int)solution.GetResult()[i - 1] - 1);
            }

            return remnantList;
        }

        /// <summary>
        /// Нахождение кодового числа по остаткам
        /// </summary>
        /// <param name="remnantsSet"></param>
        /// <param name="decisionTree"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="leftBound"></param>
        /// <returns></returns>
        public static long DescentBySegments(List<int> remnantsSet, List<List<long>> decisionTree, int row, int col, long leftBound)
        {
            if (remnantsSet.Count > 0)
            {
                long left = decisionTree[row][col - 1];

                if (remnantsSet[0] > 0)
                {
                    // UP [↑]
                    remnantsSet[0]--;
                    return DescentBySegments(remnantsSet, decisionTree, row - 1, col, leftBound + left);
                }
                else
                {
                    // LEFT [←]
                    remnantsSet.RemoveAt(0);
                    return DescentBySegments(remnantsSet, decisionTree, row, col - 1, leftBound);
                }
            }
            else
            {
                return leftBound + 1;
            }
        }

        /// <summary>
        /// Нахождение остатков по кодовому числу
        /// </summary>
        /// <param name="decisionTree"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="leftBound"></param>
        /// <param name="codeNumber"></param>
        /// <param name="result"></param>
        /// <param name="storage"></param>
        public static void FindingResidualSequence(List<List<long>> decisionTree, int row, int col, long leftBound, long codeNumber, List<int> result, int storage)
        {
            if (col > 0 && row > 0)
            {
                long left = decisionTree[row][col - 1];

                if (codeNumber <= leftBound + left)
                {
                    // LEFT [←]
                    result.Add(storage);
                    FindingResidualSequence(decisionTree, row, col - 1, leftBound, codeNumber, result, 0);
                }
                else
                {
                    // UP [↑]
                    FindingResidualSequence(decisionTree, row - 1, col, leftBound + left, codeNumber, result, storage + 1);
                }
            }
        }

        /// <summary>
        /// Перевод остатков в набор вершин
        /// </summary>
        /// <param name="result"></param>
        /// <param name="edgeNum"></param>
        /// <param name="sizeInitialEdgeSet"></param>
        /// <returns></returns>
        public static List<double> RemnantsToVertex(List<int> result, int edgeNum, int sizeInitialEdgeSet)
        {
            List<double> edgeList = new List<double>();
            bool first = true;

            foreach (var remainder in result)
            {
                if (first)
                {
                    edgeList.Add(remainder);
                    first = false;
                }
                else
                {
                    edgeList.Add(edgeList[edgeList.Count - 1] + 1 + remainder);
                }
            }

            return edgeList;
        }
    }
}
