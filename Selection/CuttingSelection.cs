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
            var sortResSelect = SortPopulation.GetSortResultOfSelect(SortType.Descending, currPopulation, FitnessFunction);
            List<Individ> popList = new List<Individ>();

            int count = 0;
            foreach (var res in sortResSelect)
            {
                if (count == 0)
                {
                    if (max.maxVal < res.Value)
                    {
                        max.maxVal = res.Value;
                        max.individ = res.Key;
                    }
                }

                popList.Add(res.Key);
                count++;
                if (count >= matingPoolSize)
                {
                    break;
                }
            }
            //Console.WriteLine();

            IPopulation population = currPopulation.GetInterfaceCopy();
            population.SetPopulationList(popList);
            return population;
        }
    }
}
