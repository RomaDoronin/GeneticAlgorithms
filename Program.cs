//#define BACKPACK_300

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Program
    {
        private const int INFINITY = -1;

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
            // ------------------------------------------------------------------------------------ Настройка генетичекого алгоритма
            BinaryGeneticAlgorithm binaryGeneticAlgorithm = new BinaryGeneticAlgorithm();
            binaryGeneticAlgorithm.SetPrintPopulation(false); // Настройка вывода популяции
            AGenAlg genAlg = binaryGeneticAlgorithm;
            genAlg.SetMaxIterNum(INFINITY);
            genAlg.SetPoolsSize(40, 20, 10);

            // ------------------------------------------------------------------------------------ Настройка популяции
            IPopulation population = new StdPopulation();
            genAlg.SetPopulation(ref population);

            // ------------------------------------------------------------------------------------ Настройка мутации
            /*RandAlleleMutation randAlleleMutation = new RandAlleleMutation();
            randAlleleMutation.SetNumOfMutAllele(8);
            genAlg.SetMutation(randAlleleMutation);*/
            RandGenMutation randGenMutation = new RandGenMutation();
            randGenMutation.SetNumOfMutGen(3);
            randGenMutation.SetMutationProbability(100);
            genAlg.SetMutation(randGenMutation);

            // ------------------------------------------------------------------------------------ Настройна селекции
            genAlg.SetSelect(new CuttingSelection());
            /*RouletteSelection rouletteSelection = new RouletteSelection();
            rouletteSelection.SetIsRemoveSelectIndividFromSelectPool(false);
            genAlg.SetSelect(rouletteSelection);*/
            //genAlg.SetSelect(new TournamentSelection());

            // ------------------------------------------------------------------------------------ Настройка выбора "родителей"
            genAlg.SetSelectParent(new RandomlyWithoutRepetitions());

            // ------------------------------------------------------------------------------------ Настройка скрещивания
            Krossingover krossingover = new Krossingover();
            krossingover.SetBreakPoint(new List<double>() { Krossingover.RAND_SET_BREAK_POINT });
            genAlg.SetCross(krossingover);
            //genAlg.SetCross(new Recombination());

            // ------------------------------------------------------------------------------------ Настройка выбора новой популяции
            genAlg.SetFormationNewPopulation(new LeaveBest());

            // ------------------------------------------------------------------------------------ Выбор задачи
            ITask task = CreateBackpackTask();
            genAlg.Solve(ref task);
        }
    }
}
