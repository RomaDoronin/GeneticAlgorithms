using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class MatrixOperation
    {
        public static bool CompareMatrix(SymmetricMatrix symmetricMatrix1, SymmetricMatrix symmetricMatrix2)
        {
            if (symmetricMatrix1.GetMatrixSize() != symmetricMatrix2.GetMatrixSize())
            {
                return false;
            }

            for (int i = 0; i < symmetricMatrix1.GetMatrixSize(); i++)
            {
                for (int j = i; j < symmetricMatrix1.GetMatrixSize(); j++)
                {
                    if (symmetricMatrix1.GetVal(i,j) != symmetricMatrix2.GetVal(i,j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void PrintMatrix(SymmetricMatrix symmetricMatrix)
        {
            for (int i = 0; i < symmetricMatrix.GetMatrixSize(); i++)
            {
                for (int j = 0; j < symmetricMatrix.GetMatrixSize(); j++)
                {
                    Console.Write(symmetricMatrix.GetVal(i,j).ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        public static double FindMinValInMatrix(SymmetricMatrix symmetricMatrix)
        {
            double min = Int32.MaxValue;

            for (int i = 0; i < symmetricMatrix.GetMatrixSize(); i++)
            {
                for (int j = i + 1; j < symmetricMatrix.GetMatrixSize(); j++)
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
