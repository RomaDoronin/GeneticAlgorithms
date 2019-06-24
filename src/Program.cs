//#define BACKPACK_300
//#define BACKPACK_800
//#define UNIT_TESTS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Program
    {
        public const int INFINITY = Int32.MaxValue;

        private static ITask CreateBackpackTask()
        {
            BackpackTask backpackTask = new BackpackTask();

            List<Object> objectList = new List<Object>();

            /* 0-1 Рюкзак | Max = 1125
            objectList.Add(new Object(106,12)); objectList.Add(new Object(122,11));
            objectList.Add(new Object(191,16)); objectList.Add(new Object(137,12));
            objectList.Add(new Object(167,12)); objectList.Add(new Object(153,19));
            objectList.Add(new Object(114,12)); objectList.Add(new Object(138,20));
            objectList.Add(new Object(194,16)); objectList.Add(new Object(161,10));

            backpackTask.SetObjectList(objectList);
            backpackTask.SetMaxWieght(100);
            backpackTask.SetMaxNumOfObject(1);
            */

            // * W = 300 | n = 3 | Max = 3343
            // * W = 800 | n = 8 | Max = 8986
            objectList.Add(new Object(152, 15)); objectList.Add(new Object(124, 14));
            objectList.Add(new Object(169, 19)); objectList.Add(new Object(125, 10));
            objectList.Add(new Object(125, 18)); objectList.Add(new Object(120, 18));
            objectList.Add(new Object(158, 12)); objectList.Add(new Object(200, 19));
            objectList.Add(new Object(145, 11)); objectList.Add(new Object(139, 10));

            backpackTask.SetObjectList(objectList);
#if BACKPACK_300
            backpackTask.SetMaxWieght(300);
            backpackTask.SetMaxNumOfObject(3);
#elif BACKPACK_800
            backpackTask.SetMaxWieght(800);
            backpackTask.SetMaxNumOfObject(8);
#endif

            return backpackTask;
        }

        struct VertexCount
        {
            public int vertexNum;
            public int vertexCount;

            public VertexCount(int vertexNum, int vertexCount)
            {
                this.vertexNum = vertexNum;
                this.vertexCount = vertexCount;
            }

            public void DecVertexCount()
            {
                vertexCount--;
            }
        };

        private static int RunRoulette(List<int> sectorList)
        {
            List<int> sumList = new List<int>();
            int sum = 0;
            foreach (var val in sectorList)
            {
                sum += val;
                sumList.Add(sum);
            }

            int rnd = RNGCSP.GetRandomNum(0, sum);
            int count = 0;
            foreach (var val in sumList)
            {
                if (rnd < val)
                {
                    break;
                }
                count++;
            }

            return count;
        }

        private static List<int> DecValInList(List<int> list, int index)
        {
            List<int> resList = new List<int>();

            int count = 0;
            foreach (var val in list)
            {
                if (count == index)
                {
                    resList.Add(val - 1);
                }
                else
                {
                    resList.Add(val);
                }

                count++;
            }

            return resList;
        }

        private static ITask CreateWorstClientTask()
        {
            int matrixSize;
            CMatrix distancesMatrix;

            // Первый вариант, Ответ 1
            /*matrixSize = 8;
            distancesMatrix = new SymmetricMatrix(matrixSize);
            distancesMatrix.SetSimetricVal(0, 1, 6); distancesMatrix.SetSimetricVal(0, 2, 8);
            distancesMatrix.SetSimetricVal(1, 2, 8); distancesMatrix.SetSimetricVal(1, 3, 8); distancesMatrix.SetSimetricVal(1, 4, 5);
            distancesMatrix.SetSimetricVal(2, 4, 2); distancesMatrix.SetSimetricVal(2, 7, 2);
            distancesMatrix.SetSimetricVal(3, 4, 10); distancesMatrix.SetSimetricVal(3, 5, 4); distancesMatrix.SetSimetricVal(3, 6, 6);
            distancesMatrix.SetSimetricVal(4, 5, 3); distancesMatrix.SetSimetricVal(4, 7, 1);*/

            // Второй вариант
            // В реальных задачах каждая вершина имеет 2-5 ребер
            matrixSize = 50;

            // Настройка количества ребер у вершин
            int maxNumVertexEdge = 5;
            int diapUp = 3;
            int diapDown = 6;

            distancesMatrix = new CMatrix(matrixSize);
            
            List<int> sectorList = new List<int>();
            for (int i = 0; i < matrixSize; i++)
            {
                sectorList.Add(maxNumVertexEdge);
            }

            int edgeNum = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                for (int rnd = RNGCSP.GetRandomNum(diapDown, diapUp); rnd > 0; rnd--)
                {
                    if (sectorList[i] == 0) break;

                    int index = 0;
                    int debugCount = 0;

                    do {
                        if (debugCount > 10) break;
                        debugCount++;
                        index = RunRoulette(sectorList);
                    } while (index == i || distancesMatrix.GetVal(i, index) != 0);

                    sectorList = DecValInList(sectorList, i);
                    sectorList = DecValInList(sectorList, index);

                    distancesMatrix.SetSimetricVal(i, index, RNGCSP.GetRandomNum(10, 31));
                    edgeNum++;
                    Console.Write("\r" + "Vertex: " + (i + 1) + " | Edge Num: " + edgeNum);
                }
                Console.Write("\r" + "Vertex: " + (i + 1) + " | Edge Num: " + edgeNum);
            }

            if (matrixSize < 50)
            {
                Console.WriteLine("\n" + "Number of skeleton trees: " + MatrixOperation.GenNumOfSkeletonTrees(distancesMatrix));
            }
            //distancesMatrix.PrintMatrix();

            return new WorstClientTask(distancesMatrix);
        }

        static void Main(string[] args)
        {
#if UNIT_TESTS
            UnitTests.UTGraphOperation.UnitTestsStart();
#else

            // ------------------------------------------------------------------------------------ Настройка генетичекого алгоритма
            AGenAlg genAlg = new BinaryGeneticAlgorithm(
                false // Настройка вывода популяции
                );
            genAlg.SetMaxIterNum(/*INFINITY*/ 1000 ); // Настройка количества итераций генетического алгоритма
            genAlg.SetPoolsSize(
                20, // Размер популяции
                10, // Размер родительского пула
                10  // Количество получаемых потомков после скрещивания
                );

            // ------------------------------------------------------------------------------------ Настройка популяции
            genAlg.SetPopulation(new StdPopulation());

            // ------------------------------------------------------------------------------------ Настройка мутации
            /*genAlg.SetMutation(new RandAlleleMutation(
                50, // Вероятность мутации
                OPERATION_TARGET.ALL, // Выбор объекта муктации (дети, родители, все)
                8 // Количество мутирующих аллелей
                ));*/

            genAlg.SetMutation(new RandGenMutation(
                100, // Вероятность мутации
                OPERATION_TARGET.ALL, // Выбор объекта муктации (дети, родители, все)
                true, // Двойная мутация - сохраняет количество "0" и "1" в том же количестве, что и были
                1 // Количество мутирующих генов
                ));

            // ------------------------------------------------------------------------------------ Настройна селекции
            /*genAlg.SetSelect(new CuttingSelection());*/

            /*genAlg.SetSelect(new RouletteSelection(
                false // Выбывают ли участники из рулетки, после их выбора
                ));*/

            genAlg.SetSelect(new TournamentSelection(
                2, // Размер турнирной группы
                true // Детерминированный выбор или нет. При детерминированном выборе из группы берется "лучший" с вероятность 1, при недетерминированном выборе выбирается "лучший" по принцципу рулетки
                ));

            // ------------------------------------------------------------------------------------ Настройка выбора "родителей"
            genAlg.SetSelectParent(new RandomlyWithoutRepetitions());

            // ------------------------------------------------------------------------------------ Настройка скрещивания
            /*genAlg.SetCross(new Krossingover(
                new List<double>() { Krossingover.RAND_SET_BREAK_POINT } // Список точек для разбиения при кросинговере. Указываются числа от 0 до 1 невключительно. Хромасома делится на доли указанные в спике.
                ));*/

            genAlg.SetCross(new Recombination(49));

            // ------------------------------------------------------------------------------------ Настройка выбора новой популяции
            genAlg.SetFormationNewPopulation(new LeaveBest());

            // ------------------------------------------------------------------------------------ Выбор задачи
#if BACKPACK_300 || BACKPACK_800
            ITask task = CreateBackpackTask();
#else
            ITask task = CreateWorstClientTask();
#endif

            genAlg.Solve(ref task);
#endif
        }
    }
}
