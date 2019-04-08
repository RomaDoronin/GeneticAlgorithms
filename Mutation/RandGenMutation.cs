using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneticAlgorithms
{
    class RandGenMutation : AMutation
    {
        private int _numOfMutGen;

        public void SetNumOfMutGen(int numOfMutGen) => _numOfMutGen = numOfMutGen;

        public override void Mutation(ref IPopulation population, ITask task)
        {
            List<Individ> populationList = new List<Individ>();
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);
            List<int> mutGenomNum = new List<int>();
            for (int i = 0; i < _numOfMutGen; i++)
            {
                mutGenomNum.Add(rnd.Next(0, 100 * population.GetCurrSize()) % population.GetCurrSize());
                Thread.Sleep(100);
            }

            for (Individ individ = population.GetFirstIndivid(); !population.IsEnd(); individ = population.GetNextIndivid())
            {
                foreach (var mutGen in mutGenomNum)
                {
                    if (mutGen == populationList.Count)
                    {
                        do
                        {
                            List<int> genom = individ.GetGenom();
                            int mutGenNum = rnd.Next(0, 100 * genom.Count) % genom.Count;
                            Console.WriteLine("Mutation genom: " + mutGen.ToString());
                            Console.WriteLine("Mutation gen: " + mutGenNum.ToString());
                            if (genom[mutGenNum] == 0)
                            {
                                genom[mutGenNum] = 1;
                            }
                            else
                            {
                                genom[mutGenNum] = 0;
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
