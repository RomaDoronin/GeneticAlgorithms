using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AMutation
    {
        protected abstract void DoMutation(ref RNGCSP rngcsp, ref List<Gen> chromosome);

        protected abstract void SetMutchromosomeNumList(IPopulation population, ref RNGCSP rngcsp, ref List<int> mutchromosomeNumList);

        public virtual void Mutation(ref IPopulation population, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            RNGCSP rngcsp = new RNGCSP();
            List<int> mutchromosomeNumList = new List<int>();
            SetMutchromosomeNumList(population, ref rngcsp, ref mutchromosomeNumList);

            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                foreach (var mutchromosomeNum in mutchromosomeNumList)
                {
                    if (mutchromosomeNum == populationList.Count)
                    {
                        do
                        {
                            List<Gen> chromosome = individ.GetChromosome();
                            do // Чтобы не заменилась аллель такая что приветел к несуществующему гену
                            {
                                DoMutation(ref rngcsp, ref chromosome);

                                //Console.WriteLine("Mutation chromosome: " + mutGen.ToString());
                                //Console.WriteLine("Mutation gen: " + mutGenNum.ToString());

                                individ.SetChromosome(chromosome);

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
