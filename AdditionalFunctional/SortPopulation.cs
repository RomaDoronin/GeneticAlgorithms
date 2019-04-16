using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    enum SortType
    {
        Ascending,
        Descending
    }

    class SortPopulation
    {
        public static IOrderedEnumerable<KeyValuePair<int, Individ>> GetSortResultOfSelect(SortType sortType, IPopulation population, FitnessFunctionDel FitnessFunction)
        {
            Dictionary<int, Individ> resSelect = new Dictionary<int, Individ>();

            List<Individ> iteratorPopList = population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                resSelect[FitnessFunction(individ)] = individ;
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
    }
}
