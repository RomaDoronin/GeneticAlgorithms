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
        public IOrderedEnumerable<KeyValuePair<int, Individ>> GetSortResultOfSelect(SortType sortType, IPopulation population, ITask task)
        {
            Dictionary<int, Individ> resSelect = new Dictionary<int, Individ>();

            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                resSelect[task.TargetFunction(individ)] = individ;
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
