//#define BACKPACK_300
//#define BACKPACK_800

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

        public BinaryGeneticAlgorithm(bool printPopulation)
        {
            _printPopulation = printPopulation;
        }

        private void PrintPopulation(String opName, IPopulation population)
        {
            if (_printPopulation)
            {
                double sum = 0;
                List<Individ> iteratorPopList = population.GetPopulationList();
                foreach (var individ in iteratorPopList)
                {
                    sum += FitnessFunction(individ);
                }

                int count = 0;
                Console.WriteLine("\n" + opName);
                foreach (var individ in iteratorPopList)
                {
                    List<Gen> chromosome = individ.GetChromosome();
                    double targFuncRes = FitnessFunction(individ);
                    string acc;
                    if (sum != 0)
                    {
                        acc = ((int)((targFuncRes * 100) / sum)).ToString();
                    }
                    else
                    {
                        acc = "0";
                    }
                    Console.WriteLine("[" + count.ToString() + "] " + individ.ToString() + " - " + (((int)(targFuncRes * 1000)) / 1000.0).ToString() + " - " + acc + "%");
                    count++;
                }
            }
        }

        private double CurrMaxInPopulation()
        {
            double currMax = 0;

            List<Individ> iteratorPopList = _population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                double fitnessFucnRes = FitnessFunction(individ);

                if (currMax < fitnessFucnRes)
                {
                    currMax = fitnessFucnRes;
                }
            }

            return currMax;
        }

        public override void Solve(ref ITask task)
        {
            _task = task;
            IPopulation currPopulation = CreatePopulation();
            PrintPopulation("CreatePopulation", currPopulation);

            while (!Stop())
            {
                IPopulation matingPool = Selection(currPopulation);
                PrintPopulation("Selection", matingPool); Console.WriteLine("\nSelection - OK");

                IPopulation children = Сrossover(matingPool);
                PrintPopulation("Crossover", children); Console.WriteLine("Crossover - OK");

                Mutation(ref currPopulation, ref children);
                PrintPopulation("Mutation", currPopulation); Console.WriteLine("Mutation - OK");
                PrintPopulation("Mutation", children);

                currPopulation = FormationNewPopulation(currPopulation, children);
                PrintPopulation("Formation New Population", currPopulation); Console.WriteLine("Formation New Population - OK");

#if BACKPACK_300
                if (_max.maxVal >= 3343) break;
#elif BACKPACK_800
                if (_max.maxVal >= 8986) break;
#else
                // Максимум: 1.5274
                if (_max.maxVal > 100) break;
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
                _solution = _task.Decoder(res.Key);
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
