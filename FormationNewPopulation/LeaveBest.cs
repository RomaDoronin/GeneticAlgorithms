using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class LeaveBest : AFormationNewPopulation
    {
        public override IPopulation FormationNewPopulation(IPopulation matingPool, IPopulation children, FitnessFunctionDel FitnessFunction, int populationSize)
        {
            List<Individ> popList = new List<Individ>();

            List<Individ> childPopList = children.GetPopulationList();
            foreach (var individ in childPopList)
            {
                matingPool.AddIndivid(individ);
            }
            
            var result = SortPopulation.GetSortResultOfSelect(SortType.Descending, matingPool, FitnessFunction);
            int count = 0;
            foreach (var res in result)
            {
                popList.Add(res.Value);
                count++;

                if (count == populationSize)
                {
                    break;
                }
            }

            IPopulation population = matingPool;
            population.SetPopulationList(popList);
            return population;
        }
    }
}
