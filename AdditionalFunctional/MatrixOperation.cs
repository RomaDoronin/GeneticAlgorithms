using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class MatrixOperation
    {
        public static int FindMinValInMatrix(SymmetricMatrix symmetricMatrix)
        {
            int min = Int32.MaxValue;

            for (int i = 0; i < symmetricMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < symmetricMatrix.GetMatrixSize(); j++)
                {
                    if (min > symmetricMatrix.GetVal(i, j))
                    {
                        min = symmetricMatrix.GetVal(i, j);
                    }
                }
            }

            return min;
        }
    }
}
