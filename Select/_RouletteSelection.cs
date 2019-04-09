using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особи для размножения выбираются случайно, но вероятность выбора особи зависит от ее значения функции приспособленности
    /// </summary>
    class RouletteSelection : ASelect
    {
        public override void Select(ref IPopulation population, ITask task, ref ResultPair max)
        {
            throw new NotImplementedException();
        }
    }
}
