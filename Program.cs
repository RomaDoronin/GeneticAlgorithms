//#define BACKPACK_300
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
#else
            backpackTask.SetMaxWieght(800);
            backpackTask.SetMaxNumOfObject(8);
#endif

            return backpackTask;
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
            genAlg.SetMaxIterNum(INFINITY); // Настройка количества итераций генетического алгоритма
            genAlg.SetPoolsSize(
                40, // Размер популяции
                20, // Размер родительского пула
                10  // Количество получаемых потомков после скрещивания
                );

            AGenAlg genAlg = new BuildingMinumumSpanningTree()

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
                3 // Количество мутирующих генов
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

            genAlg.SetCross(new Recombination());

            // ------------------------------------------------------------------------------------ Настройка выбора новой популяции
            genAlg.SetFormationNewPopulation(new LeaveBest());

            // ------------------------------------------------------------------------------------ Выбор задачи
            ITask task = CreateBackpackTask();
            genAlg.Solve(ref task);
#endif
        }
    }
}
