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

        public RandAlleleMutation()
        {
            _numOfMutAllele = 1;
        }

        public void SetNumOfMutAllele(int numOfMutAllele) => _numOfMutAllele = numOfMutAllele;

        protected override void SetMutChromosomeNumList(IPopulation population, ref RNGCSP rngcsp, ref List<int> mutchromosomeNumList)
        {
            for (int i = 0; i < _numOfMutAllele; i++)
            {
                mutchromosomeNumList.Add(rngcsp.GetRandomNum(0, population.GetCurrSize()));
            }
        }

        protected override void DoMutation(ref RNGCSP rngcsp, ref List<Gen> chromosome)
        {
            int mutGenNum = rngcsp.GetRandomNum(0, 100 * chromosome.Count) % chromosome.Count;
            int mutAlleleNum = rngcsp.GetRandomNum(0, 100 * chromosome[mutGenNum].GetGenSize()) % chromosome[mutGenNum].GetGenSize();

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
