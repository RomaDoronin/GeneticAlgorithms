using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AMutation
    {
        protected abstract void DoMutation(ref RNGCSP rngcsp, ref List<Gen> genom);

        protected abstract void SetMutGenomNumList(IPopulation population, ref RNGCSP rngcsp, ref List<int> mutGenomNumList);

        public virtual void Mutation(ref IPopulation population, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            RNGCSP rngcsp = new RNGCSP();
            List<int> mutGenomNumList = new List<int>();
            SetMutGenomNumList(population, ref rngcsp, ref mutGenomNumList);

            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                foreach (var mutGenomNum in mutGenomNumList)
                {
                    if (mutGenomNum == populationList.Count)
                    {
                        do
                        {
                            List<Gen> genom = individ.GetGenom();
                            do // Чтобы не заменилась аллель такая что приветел к несуществующему гену
                            {
                                DoMutation(ref rngcsp, ref genom);

                                //Console.WriteLine("Mutation genom: " + mutGen.ToString());
                                //Console.WriteLine("Mutation gen: " + mutGenNum.ToString());

                                individ.SetGenom(genom);

                            } while (!task.CheckIndivid(individ));
                        } while (!task.LimitationsFunction(individ));
                    }
                }

                populationList.Add(individ);
            }

            population.SetPopulationList(populationList);
        }
    }
}
