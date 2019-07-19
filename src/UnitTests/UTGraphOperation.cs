using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.UnitTests
{
    class UTGraphOperation
    {
        public static void UnitTestsStart()
        {
            // Дерево
            CMatrix matrix1 = new CMatrix(8);
            {
                matrix1.SetSimetricVal(0, 2, 8);
                matrix1.SetSimetricVal(1, 2, 8);
                matrix1.SetSimetricVal(2, 4, 2);
                matrix1.SetSimetricVal(3, 5, 4); matrix1.SetSimetricVal(3, 6, 6);
                matrix1.SetSimetricVal(4, 5, 3); matrix1.SetSimetricVal(4, 7, 1);
            }
            CMatrix shortestDistancesMatrix1 = new CMatrix(8);
            {
                shortestDistancesMatrix1.SetSimetricVal(0, 1, 16); shortestDistancesMatrix1.SetSimetricVal(0, 2, 8); shortestDistancesMatrix1.SetSimetricVal(0, 3, 17); shortestDistancesMatrix1.SetSimetricVal(0, 4, 10); shortestDistancesMatrix1.SetSimetricVal(0, 5, 13); shortestDistancesMatrix1.SetSimetricVal(0, 6, 23); shortestDistancesMatrix1.SetSimetricVal(0, 7, 11);
                shortestDistancesMatrix1.SetSimetricVal(1, 2, 8); shortestDistancesMatrix1.SetSimetricVal(1, 3, 17); shortestDistancesMatrix1.SetSimetricVal(1, 4, 10); shortestDistancesMatrix1.SetSimetricVal(1, 5, 13); shortestDistancesMatrix1.SetSimetricVal(1, 6, 23); shortestDistancesMatrix1.SetSimetricVal(1, 7, 11);
                shortestDistancesMatrix1.SetSimetricVal(2, 3, 9); shortestDistancesMatrix1.SetSimetricVal(2, 4, 2); shortestDistancesMatrix1.SetSimetricVal(2, 5, 5); shortestDistancesMatrix1.SetSimetricVal(2, 6, 15); shortestDistancesMatrix1.SetSimetricVal(2, 7, 3);
                shortestDistancesMatrix1.SetSimetricVal(3, 4, 7); shortestDistancesMatrix1.SetSimetricVal(3, 5, 4); shortestDistancesMatrix1.SetSimetricVal(3, 6, 6); shortestDistancesMatrix1.SetSimetricVal(3, 7, 8);
                shortestDistancesMatrix1.SetSimetricVal(4, 5, 3); shortestDistancesMatrix1.SetSimetricVal(4, 6, 13); shortestDistancesMatrix1.SetSimetricVal(4, 7, 1);
                shortestDistancesMatrix1.SetSimetricVal(5, 6, 10); shortestDistancesMatrix1.SetSimetricVal(5, 7, 4);
                shortestDistancesMatrix1.SetSimetricVal(6, 7, 14);
            }            
            CMatrix maxSpeedMatrix1 = new CMatrix(8);
            {
                maxSpeedMatrix1.SetSimetricVal(0, 1, ListOperation.InverseSumInverseNumbers(new List<double>() { 8, 8 })); maxSpeedMatrix1.SetSimetricVal(0, 2, 8); maxSpeedMatrix1.SetSimetricVal(0, 3, ListOperation.InverseSumInverseNumbers(new List<double>() { 8, 2, 3, 4 })); maxSpeedMatrix1.SetSimetricVal(0, 4, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2 })); maxSpeedMatrix1.SetSimetricVal(0, 5, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,3 })); maxSpeedMatrix1.SetSimetricVal(0, 6, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,3,4,6 })); maxSpeedMatrix1.SetSimetricVal(0, 7, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,1 }));
                maxSpeedMatrix1.SetSimetricVal(1, 2, 8); maxSpeedMatrix1.SetSimetricVal(1, 3, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,3,4 })); maxSpeedMatrix1.SetSimetricVal(1, 4, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2 })); maxSpeedMatrix1.SetSimetricVal(1, 5, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,3 })); maxSpeedMatrix1.SetSimetricVal(1, 6, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,3,4,6 })); maxSpeedMatrix1.SetSimetricVal(1, 7, ListOperation.InverseSumInverseNumbers(new List<double>() { 8,2,1 }));
                maxSpeedMatrix1.SetSimetricVal(2, 3, ListOperation.InverseSumInverseNumbers(new List<double>() { 2,3,4 })); maxSpeedMatrix1.SetSimetricVal(2, 4, 2); maxSpeedMatrix1.SetSimetricVal(2, 5, ListOperation.InverseSumInverseNumbers(new List<double>() { 2,3 })); maxSpeedMatrix1.SetSimetricVal(2, 6, ListOperation.InverseSumInverseNumbers(new List<double>() { 2,3,4,6 })); maxSpeedMatrix1.SetSimetricVal(2, 7, ListOperation.InverseSumInverseNumbers(new List<double>() { 2,1 }));
                maxSpeedMatrix1.SetSimetricVal(3, 4, ListOperation.InverseSumInverseNumbers(new List<double>() { 4,3 })); maxSpeedMatrix1.SetSimetricVal(3, 5, 4); maxSpeedMatrix1.SetSimetricVal(3, 6, 6); maxSpeedMatrix1.SetSimetricVal(3, 7, ListOperation.InverseSumInverseNumbers(new List<double>() { 4,3,1 }));
                maxSpeedMatrix1.SetSimetricVal(4, 5, 3); maxSpeedMatrix1.SetSimetricVal(4, 6, ListOperation.InverseSumInverseNumbers(new List<double>() { 3,4,6 })); maxSpeedMatrix1.SetSimetricVal(4, 7, 1);
                maxSpeedMatrix1.SetSimetricVal(5, 6, ListOperation.InverseSumInverseNumbers(new List<double>() { 4,6 })); maxSpeedMatrix1.SetSimetricVal(5, 7, ListOperation.InverseSumInverseNumbers(new List<double>() {3,1}));
                maxSpeedMatrix1.SetSimetricVal(6, 7, ListOperation.InverseSumInverseNumbers(new List<double>() { 1,3,4,6 }));
            }

            // Граф
            CMatrix matrix2 = new CMatrix(8);
            {
                matrix2.SetSimetricVal(0, 1, 6); matrix2.SetSimetricVal(0, 2, 8);
                matrix2.SetSimetricVal(1, 2, 8); matrix2.SetSimetricVal(1, 3, 8); matrix2.SetSimetricVal(1, 4, 5);
                matrix2.SetSimetricVal(2, 4, 2); matrix2.SetSimetricVal(2, 7, 2);
                matrix2.SetSimetricVal(3, 4, 10); matrix2.SetSimetricVal(3, 5, 4); matrix2.SetSimetricVal(3, 6, 6);
                matrix2.SetSimetricVal(4, 5, 3); matrix2.SetSimetricVal(4, 7, 1);
            }
            CMatrix shortestDistancesMatrix2 = new CMatrix(8);
            {
                shortestDistancesMatrix2.SetSimetricVal(0, 1, 6); shortestDistancesMatrix2.SetSimetricVal(0, 2, 8); shortestDistancesMatrix2.SetSimetricVal(0, 3, 14); shortestDistancesMatrix2.SetSimetricVal(0, 4, 10); shortestDistancesMatrix2.SetSimetricVal(0, 5, 13); shortestDistancesMatrix2.SetSimetricVal(0, 6, 20); shortestDistancesMatrix2.SetSimetricVal(0, 7, 10);
                shortestDistancesMatrix2.SetSimetricVal(1, 2, 7); shortestDistancesMatrix2.SetSimetricVal(1, 3, 8); shortestDistancesMatrix2.SetSimetricVal(1, 4, 5); shortestDistancesMatrix2.SetSimetricVal(1, 5, 8); shortestDistancesMatrix2.SetSimetricVal(1, 6, 14); shortestDistancesMatrix2.SetSimetricVal(1, 7, 6);
                shortestDistancesMatrix2.SetSimetricVal(2, 3, 9); shortestDistancesMatrix2.SetSimetricVal(2, 4, 2); shortestDistancesMatrix2.SetSimetricVal(2, 5, 5); shortestDistancesMatrix2.SetSimetricVal(2, 6, 15); shortestDistancesMatrix2.SetSimetricVal(2, 7, 2);
                shortestDistancesMatrix2.SetSimetricVal(3, 4, 7); shortestDistancesMatrix2.SetSimetricVal(3, 5, 4); shortestDistancesMatrix2.SetSimetricVal(3, 6, 6); shortestDistancesMatrix2.SetSimetricVal(3, 7, 8);
                shortestDistancesMatrix2.SetSimetricVal(4, 5, 3); shortestDistancesMatrix2.SetSimetricVal(4, 6, 13); shortestDistancesMatrix2.SetSimetricVal(4, 7, 1);
                shortestDistancesMatrix2.SetSimetricVal(5, 6, 10); shortestDistancesMatrix2.SetSimetricVal(5, 7, 4);
                shortestDistancesMatrix2.SetSimetricVal(6, 7, 14);
            }
            CMatrix maxSpeedMatrix2 = new CMatrix(8);
            {
            }

            // Неостовный граф
            CMatrix matrix3 = new CMatrix(8);
            {
                matrix3.SetSimetricVal(0, 1, 6); matrix3.SetSimetricVal(0, 2, 8);
                matrix3.SetSimetricVal(1, 2, 8); matrix3.SetSimetricVal(1, 3, 8);
                matrix3.SetSimetricVal(2, 7, 2);
                matrix3.SetSimetricVal(3, 5, 4); matrix3.SetSimetricVal(3, 6, 6);
            }
            CMatrix shortestDistancesMatrix3 = new CMatrix(8);
            {
                shortestDistancesMatrix3.SetSimetricVal(0, 1, 6); shortestDistancesMatrix3.SetSimetricVal(0, 2, 8); shortestDistancesMatrix3.SetSimetricVal(0, 3, 14); shortestDistancesMatrix3.SetSimetricVal(0, 4, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(0, 5, 18); shortestDistancesMatrix3.SetSimetricVal(0, 6, 20); shortestDistancesMatrix3.SetSimetricVal(0, 7, 10);
                shortestDistancesMatrix3.SetSimetricVal(1, 2, 8); shortestDistancesMatrix3.SetSimetricVal(1, 3, 8); shortestDistancesMatrix3.SetSimetricVal(1, 4, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(1, 5, 12); shortestDistancesMatrix3.SetSimetricVal(1, 6, 14); shortestDistancesMatrix3.SetSimetricVal(1, 7, 10);
                shortestDistancesMatrix3.SetSimetricVal(2, 3, 16); shortestDistancesMatrix3.SetSimetricVal(2, 4, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(2, 5, 20); shortestDistancesMatrix3.SetSimetricVal(2, 6, 22); shortestDistancesMatrix3.SetSimetricVal(2, 7, 2);
                shortestDistancesMatrix3.SetSimetricVal(3, 4, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(3, 5, 4); shortestDistancesMatrix3.SetSimetricVal(3, 6, 6); shortestDistancesMatrix3.SetSimetricVal(3, 7, 18);
                shortestDistancesMatrix3.SetSimetricVal(4, 5, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(4, 6, Program.INFINITY); shortestDistancesMatrix3.SetSimetricVal(4, 7, Program.INFINITY);
                shortestDistancesMatrix3.SetSimetricVal(5, 6, 10); shortestDistancesMatrix3.SetSimetricVal(5, 7, 22);
                shortestDistancesMatrix3.SetSimetricVal(6, 7, 24);
            }
            CMatrix maxSpeedMatrix3 = new CMatrix(8);
            {

            }

            // Граф
            CMatrix matrix4 = new CMatrix(8);
            {
                matrix4.SetSimetricVal(1, 4, 5);
                matrix4.SetSimetricVal(2, 4, 2); matrix4.SetSimetricVal(2, 7, 2);
                matrix4.SetSimetricVal(3, 5, 4); matrix4.SetSimetricVal(3, 6, 6);
                matrix4.SetSimetricVal(4, 7, 1);
            }
            CMatrix shortestDistancesMatrix4 = new CMatrix(8);
            {
                shortestDistancesMatrix4.SetSimetricVal(0, 1, 6); shortestDistancesMatrix4.SetSimetricVal(0, 2, 8); shortestDistancesMatrix4.SetSimetricVal(0, 3, 14); shortestDistancesMatrix4.SetSimetricVal(0, 4, 10); shortestDistancesMatrix4.SetSimetricVal(0, 5, 13); shortestDistancesMatrix4.SetSimetricVal(0, 6, 20); shortestDistancesMatrix4.SetSimetricVal(0, 7, 10);
                shortestDistancesMatrix4.SetSimetricVal(1, 2, 7); shortestDistancesMatrix4.SetSimetricVal(1, 3, 8); shortestDistancesMatrix4.SetSimetricVal(1, 4, 5); shortestDistancesMatrix4.SetSimetricVal(1, 5, 8); shortestDistancesMatrix4.SetSimetricVal(1, 6, 14); shortestDistancesMatrix4.SetSimetricVal(1, 7, 6);
                shortestDistancesMatrix4.SetSimetricVal(2, 3, 9); shortestDistancesMatrix4.SetSimetricVal(2, 4, 2); shortestDistancesMatrix4.SetSimetricVal(2, 5, 5); shortestDistancesMatrix4.SetSimetricVal(2, 6, 15); shortestDistancesMatrix4.SetSimetricVal(2, 7, 2);
                shortestDistancesMatrix4.SetSimetricVal(3, 4, 7); shortestDistancesMatrix4.SetSimetricVal(3, 5, 4); shortestDistancesMatrix4.SetSimetricVal(3, 6, 6); shortestDistancesMatrix4.SetSimetricVal(3, 7, 8);
                shortestDistancesMatrix4.SetSimetricVal(4, 5, 3); shortestDistancesMatrix4.SetSimetricVal(4, 6, 13); shortestDistancesMatrix4.SetSimetricVal(4, 7, 1);
                shortestDistancesMatrix4.SetSimetricVal(5, 6, 10); shortestDistancesMatrix4.SetSimetricVal(5, 7, 4);
                shortestDistancesMatrix4.SetSimetricVal(6, 7, 14);
            }
            
            CMatrix matrix5 = new CMatrix(8);
            {
                matrix5.SetSimetricVal(0, 1, 6);
                matrix5.SetSimetricVal(1, 2, 8); matrix5.SetSimetricVal(1, 3, 8); matrix5.SetSimetricVal(1, 4, 5);
                matrix5.SetSimetricVal(2, 7, 2);
                matrix5.SetSimetricVal(3, 5, 4); matrix5.SetSimetricVal(3, 6, 6);
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

            // Проверка на связность
            Console.WriteLine("CheckGraphIsSkeleton");
            {
                Console.WriteLine("TEST 1 : " + (GraphOperation.CheckGraphIsConnected(matrix1) == true).ToString());
                Console.WriteLine("TEST 2 : " + (GraphOperation.CheckGraphIsConnected(matrix2) == true).ToString());
                Console.WriteLine("TEST 3 : " + (GraphOperation.CheckGraphIsConnected(matrix3) == false).ToString());
                Console.WriteLine("TEST 4 : " + (GraphOperation.CheckGraphIsConnected(matrix4) == false).ToString());
            }

            /////////////////////////////////////////////////////////////////////////////////////
            CMatrix matrix6 = new CMatrix(5);
            matrix6.SetSimetricVal(0, 2, 8);
            matrix6.SetSimetricVal(1, 2, 8);
            matrix6.SetSimetricVal(2, 4, 2);

            for (int i = 0; i < matrix6.GetMatrixSize(); i++)
            {
                matrix6.SetSimetricVal(i, i, 1);
            }

            CMatrix matrix7 = new CMatrix(6);
            matrix7.SetSimetricVal(0, 2, 8);
            matrix7.SetSimetricVal(1, 2, 8);
            matrix7.SetSimetricVal(2, 4, 2);
            matrix7.SetSimetricVal(2, 5, 10);
            matrix7.SetSimetricVal(3, 5, 7);

            for (int i = 0; i < matrix7.GetMatrixSize(); i++)
            {
                matrix7.SetSimetricVal(i, i, 2);
            }

            CMatrix matrix8 = new CMatrix(7);
            matrix8.SetSimetricVal(0, 2, 8);
            matrix8.SetSimetricVal(0, 6, 12);
            matrix8.SetSimetricVal(1, 2, 8);
            matrix8.SetSimetricVal(1, 3, 2);
            matrix8.SetSimetricVal(1, 5, 3);
            matrix8.SetSimetricVal(2, 4, 2);
            matrix8.SetSimetricVal(2, 5, 10);
            matrix8.SetSimetricVal(3, 5, 7);
            matrix8.SetSimetricVal(3, 6, 8);
            matrix8.SetSimetricVal(4, 5, 7);
            matrix8.SetSimetricVal(4, 6, 4);
            matrix8.SetSimetricVal(5, 6, 6);

            for (int i = 0; i < matrix8.GetMatrixSize(); i++)
            {
                matrix8.SetSimetricVal(i, i, 3);
            }

            // Проверка определителя
            Console.WriteLine("GetDeterminantRec");
            {
                Console.WriteLine("TEST 1 : " + (MatrixOperation.GetDeterminantRec(matrix6) == -131).ToString());
                Console.WriteLine("TEST 2 : " + (MatrixOperation.GetDeterminantRec(matrix7) == 21440).ToString());
                Console.WriteLine("TEST 3 : " + (MatrixOperation.GetDeterminantRec(matrix8) == -4189590).ToString());
                //Console.WriteLine("TEST 4 : " + (GraphOperation.CheckGraphIsConnected(matrix4) == false).ToString());
            }
        }
    }
}
