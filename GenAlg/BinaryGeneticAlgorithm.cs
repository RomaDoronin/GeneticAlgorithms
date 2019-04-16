//#define BACKPACK_300

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{    
    /// <summary>
    /// Бинарный генетический алгоритм
    /// Переводит входные данные в двоичный код
    /// </summary>
    class BinaryGeneticAlgorithm : AGenAlg
    {
        private const double ACCURACY = 1;
        private int _stepCount = 0;
        private bool _printPopulation = false;

        private void PrintPopulation(String opName, IPopulation population)
        {
            if (_printPopulation)
            {
                int sum = 0;
                List<Individ> iteratorPopList = population.GetPopulationList();
                foreach (var individ in iteratorPopList)
                {
                    sum += FitnessFunction(individ);
                }

                int count = 0;
                Console.WriteLine(opName);
                foreach (var individ in iteratorPopList)
                {
                    List<Gen> chromosome = individ.GetChromosome();
                    int targFuncRes = FitnessFunction(individ);
                    Console.WriteLine(/*"[" + count.ToString() + "] " +*/ individ.ToString() /*+ " - " + targFuncRes.ToString() + " - " + ((targFuncRes * 100) / sum).ToString() + "%"*/);
                    count++;
                }
            }
        }

        private int CurrMaxInPopulation()
        {
            int currMax = 0;

            List<Individ> iteratorPopList = _population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                int fitnessFucnRes = FitnessFunction(individ);

                if (currMax < fitnessFucnRes)
                {
                    currMax = fitnessFucnRes;
                }
            }

            return currMax;
        }

        public void SetPrintPopulation(bool printPopulation) => _printPopulation = printPopulation;

        public override void Solve(ref ITask task)
        {
            _task = task;
            IPopulation currPopulation = CreatePopulation();
            PrintPopulation("CreatePopulation", currPopulation);

            while (!Stop())
            {
                IPopulation matingPool = Selection(currPopulation);
                PrintPopulation("Selection", matingPool);

                IPopulation children = Сrossover(matingPool);
                PrintPopulation("Crossover", children);

                Mutation(ref currPopulation, ref children);
                PrintPopulation("Mutation", currPopulation);
                PrintPopulation("Mutation", children);

                currPopulation = FormationNewPopulation(currPopulation, children);
                PrintPopulation("Formation New Population", currPopulation);

#if BACKPACK_300
                if (_max.maxVal >= 3343) break;
#else
                if (_max.maxVal >= 8986) break;
#endif
            }

            // Выбор "наилучшего" решения
            ChoosingBestSolution();

            Console.WriteLine("\n=====================================");
            Console.WriteLine("Max val:    " + _max.maxVal.ToString());
            Console.WriteLine("Max result: " + _task.Decoder(_max.individ).ToString());
        }

        private void ChoosingBestSolution()
        {
            var result = SortPopulation.GetSortResultOfSelect(SortType.Descending, _population, _task.TargetFunction);
            foreach (var res in result)
            {
                _solution = _task.Decoder(res.Value);
                //_solution.PrintResult();
                break;
            }
        }

        // Создание начальной популяции
        protected override IPopulation CreatePopulation()
        {
            do
            {
                _population.AddIndivid(_task.GenerateInitialSolution()); // В GenerateIndivid должен заполнять геном
            } while (_population.GetCurrSize() < GetPopulationSize());

            return _population;
        }
        
        // Остановка
        protected override bool Stop()
        {
            _stepCount++;
            PrintStatistic();

            return _stepCount == _maxIterNum;
        }

        private void PrintStatistic()
        {
            if ((_stepCount - 1) % 100 != 0)
            {
                Console.Write("\r");
            }
            else
            {
                Console.WriteLine();
            }
            Console.Write("Population number: " + _stepCount.ToString());
            Console.Write(" | Current Max: " + CurrMaxInPopulation().ToString());
            Console.Write(" | Max: " + _max.maxVal.ToString());
        }
    }
}
