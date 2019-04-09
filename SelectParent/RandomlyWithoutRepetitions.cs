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
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);

            int numParentFirst = rnd.Next(0, parentNumbers.Count);
            parentNumbers.RemoveAt(numParentFirst);
            int numParentSecond = rnd.Next(0, parentNumbers.Count);
            parentNumbers.RemoveAt(numParentSecond);

            /*Console.WriteLine("----------------");
            Console.WriteLine("numParentFirst: " + numParentFirst.ToString());
            Console.WriteLine("numParentSecond: " + numParentSecond.ToString());*/

            int count = 0;
            for (Individ individ = population.GetFirstIndivid(); ; individ = population.GetNextIndivid())
            {
                if (numParentFirst == count) parentFirst = individ;
                if (numParentSecond == count) parentSecond = individ;

                count++;
                if (count >= population.GetSizeAfterSelect()) break;
            }
        }
    }
}
