using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Случайный выбор родителей из популяции без повторений
    /// </summary>
    class RandomlyWithoutRepetitions : ASelectParent
    {
        public override void SelectParent(ref List<int> parentNumbers, ref Individ parentFirst, ref Individ parentSecond, IPopulation population, int matingPoolSize)
        {
            RNGCSP rngcsp = new RNGCSP();

            int index = rngcsp.GetRandomNum(0, parentNumbers.Count);
            int numParentFirst = parentNumbers[index];
            parentNumbers.RemoveAt(index);
            index = rngcsp.GetRandomNum(0, parentNumbers.Count);
            int numParentSecond = parentNumbers[index];
            parentNumbers.RemoveAt(index);

            /*Console.WriteLine("----------------");
            Console.WriteLine("numParentFirst: " + numParentFirst.ToString());
            Console.WriteLine("numParentSecond: " + numParentSecond.ToString());*/

            int count = 0;
            List<Individ> iteratorPopList = population.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                if (numParentFirst == count)
                {
                    parentFirst = individ;
                }

                if (numParentSecond == count)
                {
                    parentSecond = individ;
                }

                count++;
            }
        }
    }
}
