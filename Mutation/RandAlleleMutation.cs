using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);
            List<int> mutGenomNumList = new List<int>();
            for (int i = 0; i < _numOfMutAllele; i++)
            {
                mutGenomNumList.Add(rnd.Next(0, 100 * population.GetCurrSize()) % population.GetCurrSize());
                Thread.Sleep(100);
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
                            int mutGenNum = rnd.Next(0, 100 * genom.Count) % genom.Count;
                            int mutAlleleNum = rnd.Next(0, 100 * genom[mutGenNum].GetGenSize()) % genom[mutGenNum].GetGenSize();

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
                        } while (!task.LimitationsFunction(individ));
                    }
                }

                populationList.Add(individ);
            }

            population.SetPopulationList(populationList);
        }
    }
}
