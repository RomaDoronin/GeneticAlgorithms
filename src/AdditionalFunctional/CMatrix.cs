using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class CMatrix
    {
        private int _matrixSize;
        private List<double> _matrix;

        public CMatrix(int matrixSize)
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
            _matrix[indexI * _matrixSize + indexJ] = val;
        }

        public void SetSimetricVal(int indexI, int indexJ, double val)
        {
            _matrix[indexI * _matrixSize + indexJ] = val;
            _matrix[indexJ * _matrixSize + indexI] = val;
        }

        public double GetVal(int indexI, int indexJ)
        {
            return _matrix[indexI * _matrixSize + indexJ];
        }

        public int GetMatrixSize()
        {
            return _matrixSize;
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    Console.Write(_matrix[i * _matrixSize + j].ToString() + "	");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            String outStr = " ";

            for (int i = 0; i < _matrixSize; i++)
            {
                for (int j = 0; j < _matrixSize; j++)
                {
                    outStr += _matrix[i * _matrixSize + j].ToString() + "	";
                }
                outStr += "\n";
            }

            return outStr;
        }
    }
}
