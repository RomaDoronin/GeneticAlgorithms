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

        public override void Mutation(ref IPopulation population, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            RNGCSP rngcsp = new RNGCSP();
            List<int> mutGenomNumList = new List<int>();
            for (int i = 0; i < _numOfMutAllele; i++)
            {
                mutGenomNumList.Add(rngcsp.GetRandomNum(0, population.GetCurrSize()));
            }

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
                                int mutGenNum = rngcsp.GetRandomNum(0, 100 * genom.Count) % genom.Count;
                                int mutAlleleNum = rngcsp.GetRandomNum(0, 100 * genom[mutGenNum].GetGenSize()) % genom[mutGenNum].GetGenSize();

                                List<short> alleleList = new List<short>();
                                int alleleCount = 0;
                                foreach (var allele in genom[mutGenNum].GetAlleleList())
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

                                genom[mutGenNum].SetAlleleList(alleleList);

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
