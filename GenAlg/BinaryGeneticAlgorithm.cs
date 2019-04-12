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

        private void PrintPopulation(String opName)
        {
            if (_printPopulation)
            {
                int sum = 0;
                for (Individ individ = _population.GetFirstIndivid(); !_population.IsEnd(); individ = _population.GetNextIndivid())
                {
                    sum += _task.TargetFunction(individ);
                }

                int count = 0;
                Console.WriteLine(opName);
                for (Individ individ = _population.GetFirstIndivid(); !_population.IsEnd(); individ = _population.GetNextIndivid())
                {
                    List<Gen> genom = individ.GetGenom();
                    int targFuncRes = _task.TargetFunction(individ);
                    Console.WriteLine("[" + count.ToString() + "] " + individ.ToString() + " - " + targFuncRes.ToString() + " - " + ((targFuncRes * 100) / sum).ToString() + "%");
                    count++;
                }
            }
        }

        public void SetPrintPopulation(bool printPopulation) => _printPopulation = printPopulation;

        public override void Solve(ref ITask task)
        {
            _task = task;
            CreatePopulation();

            Select();
            // ## LOG
            PrintPopulation("Select");

            while (!Stop())
            {
                Сross();
                // ## LOG
                PrintPopulation("Cross");
                Mutation();
                // ## LOG
                PrintPopulation("Mutation");
                Select();
                // ## LOG
                PrintPopulation("Select");

                Console.WriteLine("Max: " + _max.maxVal.ToString());

#if BACKPACK_300
                if (_max.maxVal >= 3343) break;
#else
                if (_max.maxVal >= 8986) break;
#endif
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
            Console.WriteLine("Max val:    " + _max.maxVal.ToString());
            Console.WriteLine("Max result: " + _task.Decoder(_max.individ).ToString());
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
