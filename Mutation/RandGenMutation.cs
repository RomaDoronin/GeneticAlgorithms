using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Mutation
{
    /// <summary>
    /// Мутирует целый ген. Внутри гена аллели мутируют с вероятность 50%
    /// Должна быть возможность выбирать какое количество генов мутирует
    /// </summary>
    class RandGenMutation : AMutation
    {
        public override void Mutation(ref IPopulation population, ITask task)
        {
            throw new NotImplementedException();
        }
    }
}
