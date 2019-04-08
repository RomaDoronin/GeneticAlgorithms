using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AMutation
    {
        public abstract void Mutation(ref IPopulation population, ITask task);
    }
}
