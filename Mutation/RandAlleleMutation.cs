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
        private int _numOfMutGen;

        public RandAlleleMutation()
        {
            _numOfMutGen = 1;
        }

        public void SetNumOfMutGen(int numOfMutGen) => _numOfMutGen = numOfMutGen;

        public override void Mutation(ref IPopulation population, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);
            List<int> mutGenomNumList = new List<int>();
            for (int i = 0; i < _numOfMutGen; i++)
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
                            int mutAlleleNum = rnd.Next(0, 100 * genom[mutGenNum].alleleList.Count) % genom[mutGenNum].alleleList.Count;
                            
                            //Console.WriteLine("Mutation genom: " + mutGen.ToString());
                            //Console.WriteLine("Mutation gen: " + mutGenNum.ToString());
                            if (genom[mutGenNum].alleleList[mutAlleleNum] == 0)
                            {
                                genom[mutGenNum].alleleList[mutAlleleNum] = 1;
                            }
                            else
                            {
                                genom[mutGenNum].alleleList[mutAlleleNum] = 0;
                            }

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
