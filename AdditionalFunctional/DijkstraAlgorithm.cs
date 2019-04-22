using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class DijkstraAlgorithm
    {
        private TARGET _target;
        private ASumFunction _sumFunction;

        public DijkstraAlgorithm(TARGET target, ASumFunction sumFunction)
        {
            _target = target;
            _sumFunction = sumFunction;
        }

        /// <summary>
        /// Рекурсивная часть алгоритма Дейкстры
        /// </summary>
        void DijkstraAlgorithmRec(SymmetricMatrix weightsMatrix, ref List<bool> visitedVertex, ref List<double> weightsFromVertex, int currVertex, int startVertexNum)
        {
            visitedVertex[currVertex] = true;

            double limit;
            if (_target == TARGET.MIN)
            {
                limit = Program.INFINITY;
            }
            else
            {
                limit = -1;
            }

            int index = -1;

            for (int j = 0; j < weightsMatrix.GetMatrixSize(); j++)
            {
                double dist = weightsMatrix.GetVal(currVertex, j);
                if (dist != 0 && j != currVertex && !visitedVertex[j]) // ?
                {
                    double allDist = _sumFunction.SumFunc(dist, weightsFromVertex[currVertex]); //dist + weightsFromVertex[currVertex];

                    bool result;
                    if (_target == TARGET.MIN)
                    {
                        result = allDist < weightsFromVertex[j];
                    }
                    else
                    {
                        result = allDist > weightsFromVertex[j];
                    }

                    if (result)
                    {
                        weightsFromVertex[j] = allDist;
                    }
                }

                bool resultForMax;
                if (_target == TARGET.MIN)
                {
                    resultForMax = limit > weightsFromVertex[j] && !visitedVertex[j];
                }
                else
                {
                    resultForMax = limit < weightsFromVertex[j] && !visitedVertex[j];
                }

                if (resultForMax)
                {
                    limit = weightsFromVertex[j];
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
        public List<double> DoDijkstraAlgorithm(SymmetricMatrix weightsMatrix, int startVertexNum)
        {
            List<bool> visitedVertex = new List<bool>();
            List<double> weightsFromVertex = new List<double>();

            for (int i = 0; i < weightsMatrix.GetMatrixSize(); i++)
            {
                visitedVertex.Add(false);
                if (_target == TARGET.MIN)
                {
                    weightsFromVertex.Add(Program.INFINITY);
                }
                else
                {
                    weightsFromVertex.Add(-1);
                }
            }
            weightsFromVertex[startVertexNum] = 0;

            DijkstraAlgorithmRec(weightsMatrix, ref visitedVertex, ref weightsFromVertex, startVertexNum, startVertexNum);

            return weightsFromVertex;
        }
    }
}
