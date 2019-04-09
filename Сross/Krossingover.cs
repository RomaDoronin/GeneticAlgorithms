using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Сross
{
    class Krossingover : ACross
    {
        public override void Cross(ref IPopulation population, ITask task)
        {
            int size = population.GetFirstIndivid().GetGenom().Count;
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
                SelectParent(ref parentNumbers, ref parentFirst, ref parentSecond);

                var genomFirst = parentFirst.GetGenom();
                var genomSecond = parentSecond.GetGenom();

                // Кросинговер
                List<Gen> childGenomFirst = new List<Gen>();
                List<Gen> childGenomSecond = new List<Gen>();
                for (int i = 0; i < size / 2; i++)
                {
                    childGenomFirst.Add(genomFirst[i]);
                    childGenomSecond.Add(genomSecond[i]);
                }
                for (int i = size / 2; i < size; i++)
                {
                    childGenomFirst.Add(genomSecond[i]);
                    childGenomSecond.Add(genomFirst[i]);
                }

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
