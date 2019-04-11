using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class ACross
    {
        protected abstract void DoCross(List<Gen> genomFirst, List<Gen> genomSecond, ref List<Gen> childGenomFirst, ref List<Gen> childGenomSecond);

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

                List<Gen> genomFirst = parentFirst.GetGenom();
                List<Gen> genomSecond = parentSecond.GetGenom();

                List<Gen> childGenomFirst = new List<Gen>();
                List<Gen> childGenomSecond = new List<Gen>();

                DoCross(genomFirst, genomSecond, ref childGenomFirst, ref childGenomSecond);

                Individ childFirst = new Individ();
                childFirst.SetGenom(childGenomFirst);
                if (task.LimitationsFunction(childFirst))
                {
                    population.AddIndivid(childFirst);
                }

                Individ childSecond = new Individ();
                childSecond.SetGenom(childGenomSecond);
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
