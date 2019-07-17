using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.UnitTests
{
    class UTDecisionTree
    {
        public static void UnitTestsStart()
        {
            int vertexSize = 6;
            int edgeSize = 8;
            int sizeInitialEdgeSet = edgeSize - vertexSize + 2;
            List<long> RefList = new List<long>();
            for (int i = 0; i < vertexSize - 2; i++)
            {
                RefList.Add(1);
            }
            List<List<long>> decisionTree = new List<List<long>>() { RefList };

            Console.WriteLine("NumVariantInDecisionTree");
            {
                Console.WriteLine("TEST 1 : " + long.Equals(DecisionTree.CreateDecisionTree(vertexSize, sizeInitialEdgeSet, decisionTree), (long)56));
            }
        }
    }
}
