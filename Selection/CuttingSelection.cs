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
    class CuttingSelection : ASelection
    {
        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            SortPopulation sortPopulation = new SortPopulation();
            var sortResSelect = sortPopulation.GetSortResultOfSelect(SortType.Descending, currPopulation, FitnessFunction);
            List<Individ> popList = new List<Individ>();

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
                }

                popList.Add(res.Value);
                count++;
                if (count >= matingPoolSize)
                {
                    break;
                }
            }
            Console.WriteLine();

            IPopulation population = currPopulation.GetInterfaceCopy();
            population.SetPopulationList(popList);
            return population;
        }
    }
}
