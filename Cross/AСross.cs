using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class ACross
    {
        protected abstract void DoCross(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childchromosomeFirst, ref List<Gen> childchromosomeSecond);

        public virtual void Cross(ref IPopulation population, ITask task, ASelectParent selectParent)
        {
            List<int> parentNumbers = new List<int>();

            int listSize = population.GetSizeAfterSelect();
            for (int i = 0; i < listSize; i++)
            {
                parentNumbers.Add(i);
            }

            while (population.GetCurrSize() < population.GetStartPopSize())
            {
                Individ parentFirst = new Individ();
                Individ parentSecond = new Individ();
                selectParent.SelectParent(ref parentNumbers, ref parentFirst, ref parentSecond, population);

                List<Gen> chromosomeFirst = parentFirst.GetChromosome();
                List<Gen> chromosomeSecond = parentSecond.GetChromosome();

                List<Gen> childchromosomeFirst = new List<Gen>();
                List<Gen> childchromosomeSecond = new List<Gen>();

                DoCross(chromosomeFirst, chromosomeSecond, ref childchromosomeFirst, ref childchromosomeSecond);

                Individ childFirst = new Individ();
                childFirst.SetChromosome(childchromosomeFirst);
                if (task.LimitationsFunction(childFirst))
                {
                    population.AddIndivid(childFirst);
                }

                Individ childSecond = new Individ();
                childSecond.SetChromosome(childchromosomeSecond);
                if (task.LimitationsFunction(childSecond))
                {
                    population.AddIndivid(childSecond);
                }

                if (parentNumbers.Count == 0)
                {
                    for (int i = 0; i < listSize; i++)
                    {
                        parentNumbers.Add(i);
                    }
                }
            }
        }
    }
}
