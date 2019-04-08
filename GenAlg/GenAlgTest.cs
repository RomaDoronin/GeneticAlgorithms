using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneticAlgorithms
{
    enum SortType
    {
        Ascending,
        Descending
    }

    class GenAlgTest : AGenAlg
    {
        private int _maxVal;
        private const double ACCURACY = 1;
        private int _stepCount = 0;

        private IOrderedEnumerable<KeyValuePair<int, Individ>> GetSortResultOfSelect(TargetFunction fTargetFunction, SortType sortType)
        {
            Dictionary<int, Individ> resSelect = new Dictionary<int, Individ>();

            for (Individ individ = _population.GetFirstIndivid(); !_population.IsEnd(); individ = _population.GetNextIndivid())
            {
                resSelect[fTargetFunction(individ)] = individ;
            }

            if (sortType == SortType.Descending)
            {
                var sortResSelect = from individ in resSelect
                                    orderby individ.Key descending
                                    select individ;

                return sortResSelect;
            }
            else //if (sortType == SortType.Ascending)
            {
                var sortResSelect = from individ in resSelect
                                    orderby individ.Key ascending
                                    select individ;

                return sortResSelect;
            }
        }

        private static void PrintPopulation(IPopulation population)
        {
            int count = 0;
            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                List<int> genom = individ.GetGenom();
                Console.Write("[" + count.ToString() + "] ");
                int genCount = 0;
                foreach (var gen in genom)
                {
                    Console.Write(gen.ToString() + " ");
                    if (genCount == genom.Count / 2 - 1)
                    {
                        Console.Write("| ");
                    }
                    genCount++;
                }
                Console.WriteLine();
                count++;
            }
        }

        public override void Solve(ITask task)
        {
            _task = task;
            CreatePopulation();
            _maxVal = task.GetTargetFunction()(_population.GetFirstIndivid());

            Select();
            // ## LOG
            Console.WriteLine("Select"); PrintPopulation(_population);

            while (!Stop())
            {
                Сross();
                // ## LOG
                Console.WriteLine("Cross"); PrintPopulation(_population);
                Mutation();
                // ## LOG
                Console.WriteLine("Mutation"); PrintPopulation(_population);
                Select();
                // ## LOG
                Console.WriteLine("Select"); PrintPopulation(_population);
            }

            // Формирование решения
            var result = GetSortResultOfSelect(task.GetTargetFunction(), SortType.Descending);
            foreach (var res in result)
            {
                _solution.SetResult(res.Value);
                break;
            }
        }

        // Отбор родителей
        protected override void SelectParent(ref Individ  parentFirst, ref Individ parentSecond)
        {
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);

            int numParentFirst = rnd.Next(0, _population.GetSizeAfterSelect());
            int numParentSecond;
            do
            {
                numParentSecond = rnd.Next(0, _population.GetSizeAfterSelect());
            } while (numParentSecond == numParentFirst);
            Thread.Sleep(100);

            /*Console.WriteLine("----------------");
            Console.WriteLine("numParentFirst: " + numParentFirst.ToString());
            Console.WriteLine("numParentSecond: " + numParentSecond.ToString());*/

            int count = 0;
            for (Individ individ = _population.GetFirstIndivid(); ; individ = _population.GetNextIndivid())
            {
                if (numParentFirst == count) parentFirst = individ;
                if (numParentSecond == count) parentSecond = individ;

                count++;
                if (count >= _population.GetSizeAfterSelect()) break;
            }
        }

        // Создание начальной популяции
        protected override void CreatePopulation()
        {
            for (int i = 0; i < _population.GetStartPopSize(); i++)
            {
                _population.AddIndivid(_task.GenerateIndivid()); // В GenerateIndivid должен заполнять геном
            }
        }

        // Скрешивание
        protected override void Сross()
        {
            int size = _population.GetFirstIndivid().GetGenom().Count;
            while (_population.GetCurrSize() < _population.GetStartPopSize())
            {
                Individ parentFirst = new Individ();
                Individ parentSecond = new Individ();
                SelectParent(ref parentFirst, ref parentSecond);

                var genomFirst = parentFirst.GetGenom();
                var genomSecond = parentSecond.GetGenom();

                // Кросинговер
                List<int> childGenomFirst = new List<int>();
                List<int> childGenomSecond = new List<int>();
                for (int i = 0; i < size / 2; i++)
                {
                    childGenomFirst.Add(genomFirst[i]);
                    childGenomSecond.Add(genomSecond[i]);
                }
                for (int i = size / 2; i < size; i++)
                {
                    childGenomFirst.Add(genomSecond[i]);
                    childGenomSecond.Add(genomFirst[i]);
                }

                Individ childFirst = new Individ();
                childFirst.SetGenom(childGenomFirst);
                if (_task.GetLimitationsFunction()(childFirst))
                {
                    _population.AddIndivid(childFirst);
                }

                Individ childSecond = new Individ();
                childSecond.SetGenom(childGenomSecond);
                if (_task.GetLimitationsFunction()(childSecond))
                {
                    _population.AddIndivid(childSecond);
                }
            }
        }

        // Мутация
        protected override void Mutation()
        {
            List<Individ> populationList = new List<Individ>();
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);
            int mutGenomNum = rnd.Next(0, 100 * _population.GetCurrSize()) % _population.GetCurrSize();
            Console.WriteLine("Mutation genom: " + mutGenomNum.ToString());

            for (Individ individ = _population.GetFirstIndivid(); !_population.IsEnd(); individ = _population.GetNextIndivid())
            {
                if (mutGenomNum == 0)
                {
                    do
                    {
                        List<int> genom = individ.GetGenom();
                        int mutGenNum = rnd.Next(0, 100 * genom.Count) % genom.Count;
                        Console.WriteLine("Mutation gen: " + mutGenNum.ToString());
                        if (genom[mutGenNum] == 0)
                        {
                            genom[mutGenNum] = 1;
                        }
                        else
                        {
                            genom[mutGenNum] = 0;
                        }
                        individ.SetGenom(genom);
                    } while (!_task.GetLimitationsFunction()(individ));
                }

                populationList.Add(individ);
                mutGenomNum--;
            }
        }

        // Отбор
        protected override void Select()
        {
            var sortResSelect = GetSortResultOfSelect(_task.GetTargetFunction(), SortType.Descending);

            int count = 0;
            Console.Write("Max:");
            foreach (var res in sortResSelect)
            {
                _population.AddIndivid(res.Value);
                Console.Write(" " + res.Key.ToString());
                count++;
                if (count == _population.GetSizeAfterSelect())
                {
                    break;
                }
            }
            Console.WriteLine();

            _population.ClearOldPopulation();
        }

        // Остановка
        protected override bool Stop()
        {
            _stepCount++;
            int maxIterNum = 100;

            Console.WriteLine("Population number: " + _stepCount.ToString());
            //Console.Write("\r" + _stepCount.ToString() + "%");

            return _stepCount > maxIterNum;

            /*
            var sortResSelect = GetSortResultOfSelect(population, task.GetTargetFunction(), SortType.Descending);
            int maxValPred = _maxVal;

            foreach (var max in sortResSelect)
            {
                _maxVal = max.Key;
                break;
            }

            return (_maxVal - maxValPred) < ACCURACY;*/
        }
    }
}
