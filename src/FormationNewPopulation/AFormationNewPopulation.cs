using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AFormationNewPopulation
    {
        public abstract IPopulation FormationNewPopulation(IPopulation matingPool, IPopulation children, FitnessFunctionDel FitnessFunction, int populationSize);
    }
}
