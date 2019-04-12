using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Выбор из популяции лучших особей по значению функции приспособления
    /// Количество остающихся осособей после выбора хранится в population
    /// </summary>
    class CuttingSelection : ASelect
    {
        public override void Select(ref IPopulation population, ITask task, ref ResultPair max)
        {
            SortPopulation sortPopulation = new SortPopulation();
            var sortResSelect = sortPopulation.GetSortResultOfSelect(SortType.Descending, population, task);

            int count = 0;
            foreach (var res in sortResSelect)
            {
                if (count == 0)
                {
                    if (max.maxVal < res.Key)
                    {
                        max.maxVal = res.Key;
                        max.individ = res.Value;
                    }
                    //Console.Write("Max: " + res.Key.ToString());
                }

                population.AddIndivid(res.Value);
                count++;
                if (count == population.GetSizeAfterSelect())
                {
                    break;
                }
            }
            Console.WriteLine();

            population.ClearOldPopulation();
        }
    }
}
