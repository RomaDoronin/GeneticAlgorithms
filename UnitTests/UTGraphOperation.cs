using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.UnitTests
{
    class UTGraphOperation
    {
        public static double Sum(List<double> list)
        {
            if (list.Count == 1)
            {
                return list[0];
            }

            double sumT = 0.0;

            for (int i = 0; i < list.Count; i++)
            {
                sumT += 1 / list[i];
            }

            return 1 / sumT;
        }

        public static void UnitTestsStart()
        {
            // Дерево
            SymmetricMatrix matrix1 = new SymmetricMatrix(8);
            {
                matrix1.SetVal(0, 2, 8);
                matrix1.SetVal(1, 2, 8);
                matrix1.SetVal(2, 4, 2);
                matrix1.SetVal(3, 5, 4); matrix1.SetVal(3, 6, 6);
                matrix1.SetVal(4, 5, 3); matrix1.SetVal(4, 7, 1);
            }
            SymmetricMatrix shortestDistancesMatrix1 = new SymmetricMatrix(8);
            {
                shortestDistancesMatrix1.SetVal(0, 1, 16); shortestDistancesMatrix1.SetVal(0, 2, 8); shortestDistancesMatrix1.SetVal(0, 3, 17); shortestDistancesMatrix1.SetVal(0, 4, 10); shortestDistancesMatrix1.SetVal(0, 5, 13); shortestDistancesMatrix1.SetVal(0, 6, 23); shortestDistancesMatrix1.SetVal(0, 7, 11);
                shortestDistancesMatrix1.SetVal(1, 2, 8); shortestDistancesMatrix1.SetVal(1, 3, 17); shortestDistancesMatrix1.SetVal(1, 4, 10); shortestDistancesMatrix1.SetVal(1, 5, 13); shortestDistancesMatrix1.SetVal(1, 6, 23); shortestDistancesMatrix1.SetVal(1, 7, 11);
                shortestDistancesMatrix1.SetVal(2, 3, 9); shortestDistancesMatrix1.SetVal(2, 4, 2); shortestDistancesMatrix1.SetVal(2, 5, 5); shortestDistancesMatrix1.SetVal(2, 6, 15); shortestDistancesMatrix1.SetVal(2, 7, 3);
                shortestDistancesMatrix1.SetVal(3, 4, 7); shortestDistancesMatrix1.SetVal(3, 5, 4); shortestDistancesMatrix1.SetVal(3, 6, 6); shortestDistancesMatrix1.SetVal(3, 7, 8);
                shortestDistancesMatrix1.SetVal(4, 5, 3); shortestDistancesMatrix1.SetVal(4, 6, 13); shortestDistancesMatrix1.SetVal(4, 7, 1);
                shortestDistancesMatrix1.SetVal(5, 6, 10); shortestDistancesMatrix1.SetVal(5, 7, 4);
                shortestDistancesMatrix1.SetVal(6, 7, 14);
            }            
            SymmetricMatrix maxSpeedMatrix1 = new SymmetricMatrix(8);
            {
                maxSpeedMatrix1.SetVal(0, 1, Sum(new List<double>() { 8, 8 })); maxSpeedMatrix1.SetVal(0, 2, 8); maxSpeedMatrix1.SetVal(0, 3, Sum(new List<double>() { 8, 2, 3, 4 })); maxSpeedMatrix1.SetVal(0, 4, Sum(new List<double>() { 8,2 })); maxSpeedMatrix1.SetVal(0, 5, Sum(new List<double>() { 8,2,3 })); maxSpeedMatrix1.SetVal(0, 6, Sum(new List<double>() { 8,2,3,4,6 })); maxSpeedMatrix1.SetVal(0, 7, Sum(new List<double>() { 8,2,1 }));
                maxSpeedMatrix1.SetVal(1, 2, 8); maxSpeedMatrix1.SetVal(1, 3, Sum(new List<double>() { 8,2,3,4 })); maxSpeedMatrix1.SetVal(1, 4, Sum(new List<double>() { 8,2 })); maxSpeedMatrix1.SetVal(1, 5, Sum(new List<double>() { 8,2,3 })); maxSpeedMatrix1.SetVal(1, 6, Sum(new List<double>() { 8,2,3,4,6 })); maxSpeedMatrix1.SetVal(1, 7, Sum(new List<double>() { 8,2,1 }));
                maxSpeedMatrix1.SetVal(2, 3, Sum(new List<double>() { 2,3,4 })); maxSpeedMatrix1.SetVal(2, 4, 2); maxSpeedMatrix1.SetVal(2, 5, Sum(new List<double>() { 2,3 })); maxSpeedMatrix1.SetVal(2, 6, Sum(new List<double>() { 2,3,4,6 })); maxSpeedMatrix1.SetVal(2, 7, Sum(new List<double>() { 2,1 }));
                maxSpeedMatrix1.SetVal(3, 4, Sum(new List<double>() { 4,3 })); maxSpeedMatrix1.SetVal(3, 5, 4); maxSpeedMatrix1.SetVal(3, 6, 6); maxSpeedMatrix1.SetVal(3, 7, Sum(new List<double>() { 4,3,1 }));
                maxSpeedMatrix1.SetVal(4, 5, 3); maxSpeedMatrix1.SetVal(4, 6, Sum(new List<double>() { 3,4,6 })); maxSpeedMatrix1.SetVal(4, 7, 1);
                maxSpeedMatrix1.SetVal(5, 6, Sum(new List<double>() { 4,6 })); maxSpeedMatrix1.SetVal(5, 7, Sum(new List<double>() {3,1}));
                maxSpeedMatrix1.SetVal(6, 7, Sum(new List<double>() { 1,3,4,6 }));
            }

            // Граф
            SymmetricMatrix matrix2 = new SymmetricMatrix(8);
            {
                matrix2.SetVal(0, 1, 6); matrix2.SetVal(0, 2, 8);
                matrix2.SetVal(1, 2, 8); matrix2.SetVal(1, 3, 8); matrix2.SetVal(1, 4, 5);
                matrix2.SetVal(2, 4, 2); matrix2.SetVal(2, 7, 2);
                matrix2.SetVal(3, 4, 10); matrix2.SetVal(3, 5, 4); matrix2.SetVal(3, 6, 6);
                matrix2.SetVal(4, 5, 3); matrix2.SetVal(4, 7, 1);
            }
            SymmetricMatrix shortestDistancesMatrix2 = new SymmetricMatrix(8);
            {
                shortestDistancesMatrix2.SetVal(0, 1, 6); shortestDistancesMatrix2.SetVal(0, 2, 8); shortestDistancesMatrix2.SetVal(0, 3, 14); shortestDistancesMatrix2.SetVal(0, 4, 10); shortestDistancesMatrix2.SetVal(0, 5, 13); shortestDistancesMatrix2.SetVal(0, 6, 20); shortestDistancesMatrix2.SetVal(0, 7, 10);
                shortestDistancesMatrix2.SetVal(1, 2, 7); shortestDistancesMatrix2.SetVal(1, 3, 8); shortestDistancesMatrix2.SetVal(1, 4, 5); shortestDistancesMatrix2.SetVal(1, 5, 8); shortestDistancesMatrix2.SetVal(1, 6, 14); shortestDistancesMatrix2.SetVal(1, 7, 6);
                shortestDistancesMatrix2.SetVal(2, 3, 9); shortestDistancesMatrix2.SetVal(2, 4, 2); shortestDistancesMatrix2.SetVal(2, 5, 5); shortestDistancesMatrix2.SetVal(2, 6, 15); shortestDistancesMatrix2.SetVal(2, 7, 2);
                shortestDistancesMatrix2.SetVal(3, 4, 7); shortestDistancesMatrix2.SetVal(3, 5, 4); shortestDistancesMatrix2.SetVal(3, 6, 6); shortestDistancesMatrix2.SetVal(3, 7, 8);
                shortestDistancesMatrix2.SetVal(4, 5, 3); shortestDistancesMatrix2.SetVal(4, 6, 13); shortestDistancesMatrix2.SetVal(4, 7, 1);
                shortestDistancesMatrix2.SetVal(5, 6, 10); shortestDistancesMatrix2.SetVal(5, 7, 4);
                shortestDistancesMatrix2.SetVal(6, 7, 14);
            }
            SymmetricMatrix maxSpeedMatrix2 = new SymmetricMatrix(8);
            {
            }

            // Неостовный граф
            SymmetricMatrix matrix3 = new SymmetricMatrix(8);
            {
                matrix3.SetVal(0, 1, 6); matrix3.SetVal(0, 2, 8);
                matrix3.SetVal(1, 2, 8); matrix3.SetVal(1, 3, 8);
                matrix3.SetVal(2, 7, 2);
                matrix3.SetVal(3, 5, 4); matrix3.SetVal(3, 6, 6);
            }
            SymmetricMatrix shortestDistancesMatrix3 = new SymmetricMatrix(8);
            {
                shortestDistancesMatrix3.SetVal(0, 1, 6); shortestDistancesMatrix3.SetVal(0, 2, 8); shortestDistancesMatrix3.SetVal(0, 3, 14); shortestDistancesMatrix3.SetVal(0, 4, Program.INFINITY); shortestDistancesMatrix3.SetVal(0, 5, 18); shortestDistancesMatrix3.SetVal(0, 6, 20); shortestDistancesMatrix3.SetVal(0, 7, 10);
                shortestDistancesMatrix3.SetVal(1, 2, 8); shortestDistancesMatrix3.SetVal(1, 3, 8); shortestDistancesMatrix3.SetVal(1, 4, Program.INFINITY); shortestDistancesMatrix3.SetVal(1, 5, 12); shortestDistancesMatrix3.SetVal(1, 6, 14); shortestDistancesMatrix3.SetVal(1, 7, 10);
                shortestDistancesMatrix3.SetVal(2, 3, 16); shortestDistancesMatrix3.SetVal(2, 4, Program.INFINITY); shortestDistancesMatrix3.SetVal(2, 5, 20); shortestDistancesMatrix3.SetVal(2, 6, 22); shortestDistancesMatrix3.SetVal(2, 7, 2);
                shortestDistancesMatrix3.SetVal(3, 4, Program.INFINITY); shortestDistancesMatrix3.SetVal(3, 5, 4); shortestDistancesMatrix3.SetVal(3, 6, 6); shortestDistancesMatrix3.SetVal(3, 7, 18);
                shortestDistancesMatrix3.SetVal(4, 5, Program.INFINITY); shortestDistancesMatrix3.SetVal(4, 6, Program.INFINITY); shortestDistancesMatrix3.SetVal(4, 7, Program.INFINITY);
                shortestDistancesMatrix3.SetVal(5, 6, 10); shortestDistancesMatrix3.SetVal(5, 7, 22);
                shortestDistancesMatrix3.SetVal(6, 7, 24);
            }
            SymmetricMatrix maxSpeedMatrix3 = new SymmetricMatrix(8);
            {

            }

            // Граф
            SymmetricMatrix matrix4 = new SymmetricMatrix(8);
            {
                matrix4.SetVal(1, 4, 5);
                matrix4.SetVal(2, 4, 2); matrix4.SetVal(2, 7, 2);
                matrix4.SetVal(3, 5, 4); matrix4.SetVal(3, 6, 6);
                matrix4.SetVal(4, 7, 1);
            }
            SymmetricMatrix shortestDistancesMatrix4 = new SymmetricMatrix(8);
            {
                shortestDistancesMatrix4.SetVal(0, 1, 6); shortestDistancesMatrix4.SetVal(0, 2, 8); shortestDistancesMatrix4.SetVal(0, 3, 14); shortestDistancesMatrix4.SetVal(0, 4, 10); shortestDistancesMatrix4.SetVal(0, 5, 13); shortestDistancesMatrix4.SetVal(0, 6, 20); shortestDistancesMatrix4.SetVal(0, 7, 10);
                shortestDistancesMatrix4.SetVal(1, 2, 7); shortestDistancesMatrix4.SetVal(1, 3, 8); shortestDistancesMatrix4.SetVal(1, 4, 5); shortestDistancesMatrix4.SetVal(1, 5, 8); shortestDistancesMatrix4.SetVal(1, 6, 14); shortestDistancesMatrix4.SetVal(1, 7, 6);
                shortestDistancesMatrix4.SetVal(2, 3, 9); shortestDistancesMatrix4.SetVal(2, 4, 2); shortestDistancesMatrix4.SetVal(2, 5, 5); shortestDistancesMatrix4.SetVal(2, 6, 15); shortestDistancesMatrix4.SetVal(2, 7, 2);
                shortestDistancesMatrix4.SetVal(3, 4, 7); shortestDistancesMatrix4.SetVal(3, 5, 4); shortestDistancesMatrix4.SetVal(3, 6, 6); shortestDistancesMatrix4.SetVal(3, 7, 8);
                shortestDistancesMatrix4.SetVal(4, 5, 3); shortestDistancesMatrix4.SetVal(4, 6, 13); shortestDistancesMatrix4.SetVal(4, 7, 1);
                shortestDistancesMatrix4.SetVal(5, 6, 10); shortestDistancesMatrix4.SetVal(5, 7, 4);
                shortestDistancesMatrix4.SetVal(6, 7, 14);
            }
            
            SymmetricMatrix matrix5 = new SymmetricMatrix(8);
            {
                matrix5.SetVal(0, 1, 6);
                matrix5.SetVal(1, 2, 8); matrix5.SetVal(1, 3, 8); matrix5.SetVal(1, 4, 5);
                matrix5.SetVal(2, 7, 2);
                matrix5.SetVal(3, 5, 4); matrix5.SetVal(3, 6, 6);
            }

            // Матрица наикротчайших путей
            Console.WriteLine("GetShortestDistancesMatrix");
            {
                Console.WriteLine("TEST 1 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetShortestDistancesMatrix(matrix1), shortestDistancesMatrix1)).ToString());
                Console.WriteLine("TEST 2 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetShortestDistancesMatrix(matrix2), shortestDistancesMatrix2)).ToString());
                Console.WriteLine("TEST 3 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetShortestDistancesMatrix(matrix3), shortestDistancesMatrix3)).ToString());
            }

            // Матрица максимальных скоростей
            Console.WriteLine("GetMaxSpeedMatrix");
            {
                Console.WriteLine("TEST 1 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetMaxSpeedMatrix(matrix1), maxSpeedMatrix1)).ToString());
                Console.WriteLine("TEST 2 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetMaxSpeedMatrix(matrix2), maxSpeedMatrix2)).ToString());
                Console.WriteLine("TEST 3 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetMaxSpeedMatrix(matrix3), maxSpeedMatrix3)).ToString());

                Console.WriteLine("TEST 4 : " + (MatrixOperation.CompareMatrix(GraphOperation.GetMaxSpeedMatrix(matrix5), maxSpeedMatrix3)).ToString());
            }

            // Проверка на дерево
            Console.WriteLine("CheckGraphIsTree");
            {
                Console.WriteLine("TEST 1 : " + (GraphOperation.CheckNoCyclesInGraph(matrix1) == true).ToString());
                Console.WriteLine("TEST 2 : " + (GraphOperation.CheckNoCyclesInGraph(matrix2) == false).ToString());
                Console.WriteLine("TEST 3 : " + (GraphOperation.CheckNoCyclesInGraph(matrix3) == false).ToString());
                Console.WriteLine("TEST 4 : " + (GraphOperation.CheckNoCyclesInGraph(matrix4) == false).ToString());
            }

            // Проверка на остов
            Console.WriteLine("CheckGraphIsSkeleton");
            {
                Console.WriteLine("TEST 1 : " + (GraphOperation.CheckGraphIsSkeleton(matrix1) == true).ToString());
                Console.WriteLine("TEST 2 : " + (GraphOperation.CheckGraphIsSkeleton(matrix2) == true).ToString());
                Console.WriteLine("TEST 3 : " + (GraphOperation.CheckGraphIsSkeleton(matrix3) == false).ToString());
                Console.WriteLine("TEST 4 : " + (GraphOperation.CheckGraphIsSkeleton(matrix4) == false).ToString());
            }

            // Проверка на связность
            Console.WriteLine("CheckGraphIsSkeleton");
            {
                Console.WriteLine("TEST 1 : " + (GraphOperation.CheckGraphIsConnected(matrix1) == true).ToString());
                Console.WriteLine("TEST 2 : " + (GraphOperation.CheckGraphIsConnected(matrix2) == true).ToString());
                Console.WriteLine("TEST 3 : " + (GraphOperation.CheckGraphIsConnected(matrix3) == false).ToString());
                Console.WriteLine("TEST 4 : " + (GraphOperation.CheckGraphIsConnected(matrix4) == false).ToString());
            }
        }
    }
}
