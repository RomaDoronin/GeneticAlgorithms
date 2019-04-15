using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Мутирует целый ген. Внутри гена аллели мутируют с вероятность 50%
    /// Должна быть возможность выбирать какое количество генов мутирует
    /// </summary>
    class RandGenMutation : AMutation
    {
        private int _numOfMutGen;

        public RandGenMutation()
        {
            _numOfMutGen = 1;
        }

        public void SetNumOfMutGen(int numOfMutGen) => _numOfMutGen = numOfMutGen;

        protected override void DoMutation(ref RNGCSP rngcsp, ref List<Gen> chromosome)
        {
            int mutGenNum = rngcsp.GetRandomNum(0, chromosome.Count);

            List<short> alleleList = new List<short>();
            foreach (var allele in chromosome[mutGenNum].GetAlleleList())
            {
                if (rngcsp.GetRandomNum(0, 2) == 0)
                {
                    alleleList.Add(allele);
                }
                else
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
            }

            // На самом деле можно было бы просто написать
            //for (int i = 0; i < chromosome[mutGenNum].GetAlleleList().Count; i++)
            //{
            //    alleleList.Add((short)rngcsp.GetRandomNum(0, 2));
            //}
            // Потому что в случае бинарного представления это однозначно

            chromosome[mutGenNum].SetAlleleList(alleleList);
        }

        protected override void SetMutChromosomeNumList(IPopulation population, ref RNGCSP rngcsp, ref List<int> mutChromosomeNumList)
        {
            int popSize = population.GetCurrSize();
            for (int i = 0; i < _numOfMutGen; i++)
            {
                mutChromosomeNumList.Add(rngcsp.GetRandomNum(0, popSize));
            }
        }
    }
}
