using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class SymmetricMatrix
    {
        private int _matrixSize;
        private List<double> _matrix;

        public SymmetricMatrix(int matrixSize)
        {
            _matrix = new List<double>();
            _matrixSize = matrixSize;
            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    _matrix.Add(0.0);
                }
            }
        }

        public void SetVal(int indexI, int indexJ, double val)
        {
            if (indexI > indexJ)
            {
                int index = indexI;
                indexI = indexJ;
                indexJ = index;
            }

            _matrix[indexI * _matrixSize + indexJ] = val;
        }

        public double GetVal(int indexI, int indexJ)
        {
            if (indexI > indexJ)
            {
                int index = indexI;
                indexI = indexJ;
                indexJ = index;
            }

            return _matrix[indexI * _matrixSize + indexJ];
        }

        public int GetMatrixSize()
        {
            return _matrixSize;
        }
    }
}
