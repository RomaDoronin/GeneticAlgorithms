using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Рекомбинация
    /// Если гены в одной позиции у родителей совпадают, то у ребенка будет такой же ген. Если геры различные по случайно выбирается один из них, который достанется ребенку
    /// </summary>
    class Recombination : ACross
    {
        public override void Cross(ref IPopulation population, ITask task, ASelectParent selectParent)
        {
            throw new NotImplementedException();
        }
    }
}
