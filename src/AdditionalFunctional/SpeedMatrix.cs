using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class SpeedMatrix
    {
        /// <summary>
        /// Функция перевода данных из набора вершин в матрицу расстояний
        /// </summary>
        public static CMatrix EdgeListToSpeedMatrix(List<double> edgeList, List<SEdge> refEdgeList, CMatrix refSpeedMatrix)
        {
            CMatrix speedMatrix = new CMatrix(refSpeedMatrix.GetMatrixSize());
            
            foreach (var edge in edgeList)
            {
                int row = refEdgeList[(int)edge].row;
                int col = refEdgeList[(int)edge].col;
                speedMatrix.SetSimetricVal(row, col, refSpeedMatrix.GetVal(row, col));
            }

            return speedMatrix;
        }

        /// <summary>
        /// Функция перевода данных из особи в матрицу расстояний
        /// </summary>
        public static CMatrix IndividToSpeedMatrix(Individ individ, ITask task, List<SEdge> refEdgeList, CMatrix refSpeedMatrix)
        {
            List<double> result = task.Decoder(individ).GetResult();
            return EdgeListToSpeedMatrix(result, refEdgeList, refSpeedMatrix);
        }
    }
}
