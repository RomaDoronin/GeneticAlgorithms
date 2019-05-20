using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class MatrixOperation
    {
        public static bool CompareMatrix(CMatrix symmetricMatrix1, CMatrix symmetricMatrix2)
        {
            double accuracy = 0.000001;

            if (symmetricMatrix1.GetMatrixSize() != symmetricMatrix2.GetMatrixSize())
            {
                return false;
            }

            for (int i = 0; i < symmetricMatrix1.GetMatrixSize(); i++)
            {
                for (int j = i; j < symmetricMatrix1.GetMatrixSize(); j++)
                {
                    //Console.WriteLine(symmetricMatrix1.GetVal(i, j) + " vs " + symmetricMatrix2.GetVal(i, j));
                    if (Math.Abs(symmetricMatrix1.GetVal(i,j) - symmetricMatrix2.GetVal(i,j)) > accuracy)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static void PrintMatrix(CMatrix symmetricMatrix)
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

        public static double FindMinValInMatrix(CMatrix symmetricMatrix)
        {
            double min = Program.INFINITY;

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

        private static CMatrix GetSubMatrix(CMatrix matrix, int deleteRow, int deleteCollum)
        {
            CMatrix submatrix = new CMatrix(matrix.GetMatrixSize() - 1);
            int additiveI = 0;

            for (int i = 0; i < matrix.GetMatrixSize(); i++)
            {
                if (i == deleteRow)
                {
                    additiveI = 1;
                    continue;
                }

                for (int j = 0; j < matrix.GetMatrixSize(); j++)
                {
                    if (j < deleteCollum)
                    {
                        submatrix.SetVal(i - additiveI, j, matrix.GetVal(i, j));
                    }
                    else if (j > deleteCollum)
                    {
                        submatrix.SetVal(i - additiveI, j - 1, matrix.GetVal(i, j));
                    }
                }
            }

            return submatrix;
        }

        public static double GetDeterminantRec(CMatrix matrix)
        {
            if (matrix.GetMatrixSize() == 1)
            {
                return matrix.GetVal(0, 0);
            }

            double res = 0;

            for (int i = 0; i < matrix.GetMatrixSize(); i++)
            {
                double val = matrix.GetVal(i, 0);
                if (val != 0)
                {
                    res += val * Math.Pow(-1, i) * GetDeterminantRec(GetSubMatrix(matrix, i, 0));
                }
            }

            return res;
        }

        public static int GenNumOfSkeletonTrees(CMatrix symmetricMatrix)
        {
            CMatrix kirchhoffMatrix = new CMatrix(symmetricMatrix.GetMatrixSize());

            for (int i = 0; i < symmetricMatrix.GetMatrixSize(); i++)
            {
                int edgeNum = 0;
                for (int j = 0; j < symmetricMatrix.GetMatrixSize(); j++)
                {
                    if (symmetricMatrix.GetVal(i,j) != 0)
                    {
                        edgeNum++;
                        kirchhoffMatrix.SetSimetricVal(i, j, -1);
                    }
                }
                kirchhoffMatrix.SetSimetricVal(i, i, edgeNum);
            }

            return (int)GetDeterminantRec(GetSubMatrix(kirchhoffMatrix, 0, 0));
        }
    }
}
