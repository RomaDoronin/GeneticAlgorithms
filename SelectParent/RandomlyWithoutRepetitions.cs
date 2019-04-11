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
        public override void SelectParent(ref List<int> parentNumbers, ref Individ parentFirst, ref Individ parentSecond, IPopulation population)
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
            for (Individ individ = population.GetFirstIndivid(); ; individ = population.GetNextIndivid())
            {
                if (numParentFirst == count) parentFirst = individ;
                if (numParentSecond == count) parentSecond = individ;

                if (count >= population.GetSizeAfterSelect()) break;
                count++;
            }
        }
    }
}
