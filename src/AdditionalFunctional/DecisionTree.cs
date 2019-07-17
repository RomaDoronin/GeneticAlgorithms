using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class DecisionTree
    {
        /// <summary>
        /// Строит дерево решений. Возвращает количество листьев в дереве решений - вариантов подграфов
        /// </summary>
        /// <param name="vertexNum"></param>
        /// <param name="sizeInitialEdgeSet"></param>
        /// <param name="decisionTree"></param>
        /// <returns></returns>
        public static long CreateDecisionTree(int vertexNum, int sizeInitialEdgeSet, List<List<long>> decisionTree)
        {
            return CreateDecisionTreeRec(vertexNum, sizeInitialEdgeSet, decisionTree, 2);
        }

        /// <summary>
        /// Рекурсивная часть функции
        /// </summary>
        private static long CreateDecisionTreeRec(int vertexNum, int sizeInitialEdgeSet, List<List<long>> decisionTree, int step)
        {
            long addition = 0;
            List<long> xNew = new List<long>
            {
                step
            };

            for (int i = 0; i < vertexNum - 3; i++)
            {
                xNew.Add(xNew[i] + decisionTree[step - 2][i + 1]);
            }

            decisionTree.Add(xNew);

            if (step < sizeInitialEdgeSet)
            {
                addition = CreateDecisionTreeRec(vertexNum, sizeInitialEdgeSet, decisionTree, step + 1);
            }
            else
            {
                addition = 1;
            }

            return xNew[xNew.Count - 1] + addition;
        }
    }
}
