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

        private static void PrintPopulation(IPopulation population)
        {
            int count = 0;
            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                List<Gen> genom = individ.GetGenom();
                Console.WriteLine("[" + count.ToString() + "] " + individ.ToString());
                count++;
            }
        }

        public override void Solve(ref ITask task)
        {
            _task = task;
            CreatePopulation();

            Select();
            // ## LOG
            Console.WriteLine("Select"); PrintPopulation(_population);

            while (!Stop())
            {
                Сross();
                // ## LOG
                //Console.WriteLine("Cross"); PrintPopulation(_population);
                Mutation();
                // ## LOG
                //Console.WriteLine("Mutation"); PrintPopulation(_population);
                Select();
                // ## LOG
                //Console.WriteLine("Select"); PrintPopulation(_population);

                if (_max.maxVal >= 8986)
                {
                    break;
                }
            }

            // Формирование решения
            SortPopulation sortPopulation = new SortPopulation();
            var result = sortPopulation.GetSortResultOfSelect(SortType.Descending, _population, _task);
            foreach (var res in result)
            {
                _solution = _task.Decoder(res.Value);
                _solution.PrintResult();
                break;
            }

            Console.WriteLine("=====================================");
            Console.WriteLine("Max val:   " + _max.maxVal.ToString());
            Console.WriteLine("Max genom: " + _max.individ.ToString());
        }

        // Создание начальной популяции
        protected override void CreatePopulation()
        {
            for (int i = 0; i < _population.GetStartPopSize(); i++)
            {
                _population.AddIndivid(_task.GenerateInitialSolution()); // В GenerateIndivid должен заполнять геном
            }
        }
        
        // Остановка
        protected override bool Stop()
        {
            _stepCount++;

            Console.WriteLine("Population number: " + _stepCount.ToString());

            return _stepCount == _maxIterNum;
        }
    }
}
