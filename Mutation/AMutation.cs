using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AMutation
    {
        /// <summary>
        /// Вероятность мутации
        /// Значение располагается от 0 до 100
        /// </summary>
        private int _mutationProbability;
        private OPERATION_TARGET _mutationTarget;

        public AMutation(int mutationProbability, OPERATION_TARGET mutationTarget)
        {
            _mutationProbability = mutationProbability;
            _mutationTarget = mutationTarget;
        }

        protected abstract void DoMutation(ref List<Gen> chromosome);

        protected abstract void SetMutChromosomeNumList(IPopulation population, ref List<int> mutChromosomeNumList);

        public void Mutation(ref IPopulation matingPool, ref IPopulation children, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            List<int> mutChromosomeNumList = new List<int>();

            PopulationGroup population = new PopulationGroup();

            if ((_mutationTarget & OPERATION_TARGET.PARENTS) > 0)
            {
                population.AddPopulation(ref matingPool);
            }
            if ((_mutationTarget & OPERATION_TARGET.CHILDREN) > 0)
            {
                population.AddPopulation(ref children);
            }

            SetMutChromosomeNumList(population, ref mutChromosomeNumList);

            List<Individ> iteratorPopList = population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                foreach (var mutChromosomeNum in mutChromosomeNumList)
                {
                    if (mutChromosomeNum == populationList.Count)
                    {
                        do
                        {
                            List<Gen> chromosome = individ.GetChromosome();
                            do // Чтобы не заменилась аллель, такая что приведет к несуществующему гену
                            {
                                if (_mutationProbability >= RNGCSP.GetRandomNum(0, 101))
                                {
                                    DoMutation(ref chromosome);
                                }

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
