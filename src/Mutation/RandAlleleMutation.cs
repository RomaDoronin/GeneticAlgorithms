using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Мутирует случайная аллель
    /// Должна быть возможность выбирать какое количество аллелей мутирует
    /// </summary>
    class RandAlleleMutation : AMutation
    {
        private int _numOfMutAllele;

        // TODO: Сделать для этой мутации двойную мутацию 
        public RandAlleleMutation(int mutationProbability, OPERATION_TARGET mutationTarget, bool isDualMutation, int numOfMutAllele) : base(mutationProbability, mutationTarget, isDualMutation)
        {
            _numOfMutAllele = numOfMutAllele;
        }

        protected override void SetMutChromosomeNumList(IPopulation population, ref List<int> mutchromosomeNumList)
        {
            for (int i = 0; i < _numOfMutAllele; i++)
            {
                mutchromosomeNumList.Add(RNGCSP.GetRandomNum(0, population.GetCurrSize()));
            }
        }

        protected override void DoMutation(ref List<Gen> chromosome)
        {
            int mutGenNum = RNGCSP.GetRandomNum(0, 100 * chromosome.Count) % chromosome.Count;
            int mutAlleleNum = RNGCSP.GetRandomNum(0, 100 * chromosome[mutGenNum].GetGenSize()) % chromosome[mutGenNum].GetGenSize();

            List<short> alleleList = new List<short>();
            int alleleCount = 0;
            foreach (var allele in chromosome[mutGenNum].GetAlleleList())
            {
                if (alleleCount == mutAlleleNum)
                {
                    if (allele == 0)
                    {
                        alleleList.Add(1);
                    }
                    else
                    {
                        alleleList.Add(0);
                    }
                }
                else
                {
                    alleleList.Add(allele);
                }

                alleleCount++;
            }

            chromosome[mutGenNum].SetAlleleList(alleleList);
        }
    }
}
