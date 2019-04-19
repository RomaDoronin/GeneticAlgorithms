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
        public static IOrderedEnumerable<KeyValuePair<Individ, int>> GetSortResultOfSelect(SortType sortType, IPopulation population, FitnessFunctionDel FitnessFunction)
        {
            // WARNING: Уязвимое место (!) Есть возможно что если будет много одинаковых решений, то просто нехватит осоьей на популяцию
            //Dictionary<int, Individ> resSelect = new Dictionary<int, Individ>();
            Dictionary<Individ, int> resSelect = new Dictionary<Individ, int>();

            List<Individ> iteratorPopList = population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                resSelect[individ] = FitnessFunction(individ);
            }

            if (sortType == SortType.Descending)
            {
                var sortResSelect = from individ in resSelect
                                    orderby individ.Value descending
                                    select individ;

                return sortResSelect;
            }
            else //if (sortType == SortType.Ascending)
            {
                var sortResSelect = from individ in resSelect
                                    orderby individ.Value ascending
                                    select individ;

                return sortResSelect;
            }
        }
    }
}
