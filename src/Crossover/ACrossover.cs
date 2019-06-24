using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class ACrossover
    {
        /// <summary>
        /// Вероятность скрещивания
        /// Значение располагается от 0 до 100
        /// </summary>
        private int _crossoverProbability;

        public void SetCrossoverProbability(int crossoverProbability) => _crossoverProbability = crossoverProbability;

        public ACrossover()
        {
            _crossoverProbability = 100; // %
        }

        protected abstract void DoCrossover(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childchromosomeFirst, ref List<Gen> childchromosomeSecond);

        public virtual IPopulation Crossover(IPopulation matingPool, LimitationsFunctionDel LimitationsFunction, ASelectParent selectParent, int childSize)
        {
            List<int> parentNumbers = new List<int>();
            
            for (int i = 0; i < matingPool.GetCurrSize(); i++)
            {
                parentNumbers.Add(i);
            }

            List<Individ> popList = new List<Individ>();

            int missing_count = 0;
            int child_count = 0;
            Console.WriteLine();

            while (popList.Count < childSize)
            {
                Individ parentFirst = new Individ();
                Individ parentSecond = new Individ();
                selectParent.SelectParent(ref parentNumbers, ref parentFirst, ref parentSecond, matingPool, matingPool.GetCurrSize());

                List<Gen> chromosomeFirst = parentFirst.GetChromosome();
                List<Gen> chromosomeSecond = parentSecond.GetChromosome();

                List<Gen> childchromosomeFirst = new List<Gen>();
                List<Gen> childchromosomeSecond = new List<Gen>();

                DoCrossover(chromosomeFirst, chromosomeSecond, ref childchromosomeFirst, ref childchromosomeSecond);

                Individ childFirst = new Individ();
                childFirst.SetChromosome(childchromosomeFirst);
                if (LimitationsFunction(childFirst))
                {
                    popList.Add(childFirst);
                    child_count++;
                    //Console.Write("\nChild: " + child_count + "\n");
                }
                else
                {
                    missing_count++;
                    //Console.Write("\rMissing: " + missing_count);
                }

                Individ childSecond = new Individ();
                childSecond.SetChromosome(childchromosomeSecond);
                if (LimitationsFunction(childSecond))
                {
                    popList.Add(childSecond);
                    child_count++;
                    //Console.Write("\nChild: " + child_count + "\n");
                }
                else
                {
                    missing_count++;
                    //Console.Write("\rMissing: " + missing_count);
                }

                if (parentNumbers.Count == 0)
                {
                    for (int i = 0; i < matingPool.GetCurrSize(); i++)
                    {
                        parentNumbers.Add(i);
                    }
                }
            }
            
            IPopulation children = matingPool.GetInterfaceCopy();
            children.SetPopulationList(popList);
            return children;
        }
    }
}
