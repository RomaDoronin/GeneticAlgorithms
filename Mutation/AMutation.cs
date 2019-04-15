using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    public enum MUTATION_TARGET
    {
        CHILDREN = 1,
        PARENTS = 2,
        ALL = 3
    }

    abstract class AMutation
    {
        /// <summary>
        /// Вероятность мутации
        /// Значение располагается от 0 до 100
        /// </summary>
        private int _mutationProbability;
        private MUTATION_TARGET _mutationTarget;

        // --------------------------------------------------------------- Настройки
        public void SetMutationProbability(int mutationProbability) => _mutationProbability = mutationProbability;
        public void SetMutationTarget(MUTATION_TARGET mutationTarget) => _mutationTarget = mutationTarget;
        // ---------------------------------------------------------------

        public AMutation()
        {
            _mutationProbability = 100; // %
            _mutationTarget = MUTATION_TARGET.ALL;
        }

        protected abstract void DoMutation(ref RNGCSP rngcsp, ref List<Gen> chromosome);

        protected abstract void SetMutChromosomeNumList(IPopulation population, ref RNGCSP rngcsp, ref List<int> mutChromosomeNumList);

        public void Mutation(ref IPopulation matingPool, ref IPopulation children, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            RNGCSP rngcsp = new RNGCSP();
            List<int> mutChromosomeNumList = new List<int>();

            PopulationGroup population = new PopulationGroup();

            if ((_mutationTarget & MUTATION_TARGET.PARENTS) > 0)
            {
                population.AddPopulation(ref matingPool);
            }
            if ((_mutationTarget & MUTATION_TARGET.CHILDREN) > 0)
            {
                population.AddPopulation(ref children);
            }

            SetMutChromosomeNumList(population, ref rngcsp, ref mutChromosomeNumList);

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
                                if (_mutationProbability >= rngcsp.GetRandomNum(0, 101))
                                {
                                    DoMutation(ref rngcsp, ref chromosome);
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
