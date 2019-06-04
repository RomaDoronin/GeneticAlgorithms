using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class ASelectParent
    {
        public abstract void SelectParent(ref List<int> parentNumbers, ref Individ parentFirst, ref Individ parentSecond, IPopulation population, int matingPoolSize);
    }
}
