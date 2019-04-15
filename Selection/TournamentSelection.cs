using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особи разбиваются на подгруппы по 2-3 особи с последующим выбором лучших из подгрупп
    /// Разбивать можно случайно - любая особь может попасть в любую подгруппу сколько угодно раз,
    /// можно детерминированно - одна особь попадает ровно в одну подгруппу
    /// </summary>
    class TournamentSelection : ASelection
    {
        private int _subgroupSize;
        private bool _isDeterministicPartitioning;
        
        public void SetSubgroupSize(int subgroupSize) => _subgroupSize = subgroupSize;
        public void SetIsDeterministicPartitioning(bool isDeterministicPartitioning) => _isDeterministicPartitioning = isDeterministicPartitioning;

        public TournamentSelection()
        {
            _subgroupSize = 2;
            _isDeterministicPartitioning = true;
        }

        private Individ BestInList(FitnessFunctionDel FitnessFunction, List<Individ> subgroup, ref ResultPair max)
        {
            Individ bestIndivid = subgroup[0];
            int maxValFitnessFunction = FitnessFunction(subgroup[0]);

            foreach (var individ in subgroup)
            {
                int fitnessFunctionRes = FitnessFunction(individ);

                if (fitnessFunctionRes > maxValFitnessFunction)
                {
                    maxValFitnessFunction = fitnessFunctionRes;
                    bestIndivid = individ;
                }
            }

            if (maxValFitnessFunction > max.maxVal)
            {
                max.maxVal = maxValFitnessFunction;
                max.individ = bestIndivid;
            }

            return bestIndivid;
        }

        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            List<Individ> populationList = new List<Individ>();
            List<Individ> popList = currPopulation.GetPopulationList();
            RNGCSP rngcsp = new RNGCSP();

            do
            {
                List<Individ> subgroup = new List<Individ>();
                for (int i = 0; i < _subgroupSize; i++)
                {
                    int index = rngcsp.GetRandomNum(0, popList.Count);
                    subgroup.Add(popList[index]);
                    if (_isDeterministicPartitioning)
                    {
                        popList.Remove(popList[index]);
                    }
                }

                populationList.Add(BestInList(FitnessFunction, subgroup, ref max));

            } while (populationList.Count != matingPoolSize);

            currPopulation.SetPopulationList(populationList);
            return currPopulation;
        }
    }
}
