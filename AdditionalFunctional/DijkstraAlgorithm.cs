using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class DijkstraAlgorithm
    {
        private ASumFunction _sumFunction;

        public DijkstraAlgorithm(ASumFunction sumFunction)
        {
            _sumFunction = sumFunction;
        }

        /// <summary>
        /// Рекурсивная часть алгоритма Дейкстры
        /// </summary>
        void DijkstraAlgorithmRec(CMatrix weightsMatrix, ref List<bool> visitedVertex, ref List<double> weightsFromVertex, int currVertex, int startVertexNum)
        {
            visitedVertex[currVertex] = true;

            double min;
            min = Program.INFINITY;

            int index = -1;

            for (int j = 0; j < weightsMatrix.GetMatrixSize(); j++)
            {
                double dist = weightsMatrix.GetVal(currVertex, j);
                if (dist != 0 && j != currVertex && !visitedVertex[j])
                {
                    double allDist = _sumFunction.SumFunc(dist, weightsFromVertex[currVertex]);
                    
                    if (allDist < weightsFromVertex[j])
                    {
                        weightsFromVertex[j] = allDist;
                    }
                }

                if (min > weightsFromVertex[j] && !visitedVertex[j])
                {
                    min = weightsFromVertex[j];
                    index = j;
                }
            }

            if (index != -1)
            {
                DijkstraAlgorithmRec(weightsMatrix, ref visitedVertex, ref weightsFromVertex, index, startVertexNum);
            }
        }

        /// <summary>
        /// Алгоритм Дейкстры
        /// </summary>
        /// <param name="weightsMatrix">Матрица весов</param>
        /// <param name="startVertexNum">Стартовая вершина</param>
        public List<double> DoDijkstraAlgorithm(CMatrix weightsMatrix, int startVertexNum)
        {
            List<bool> visitedVertex = new List<bool>();
            List<double> weightsFromVertex = new List<double>();

            for (int i = 0; i < weightsMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
                weightsFromVertex.Add(Program.INFINITY);
            }
            weightsFromVertex[startVertexNum] = 0;

            DijkstraAlgorithmRec(weightsMatrix, ref visitedVertex, ref weightsFromVertex, startVertexNum, startVertexNum);

            return weightsFromVertex;
        }
    }
}
