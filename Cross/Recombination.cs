using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Рекомбинация
    /// Если ген в одной позиции у родителей совпадают, то у ребенка будет такой же ген. Если геры различные по случайно выбирается один из них, который достанется ребенку
    /// </summary>
    class Recombination : ACross
    {
        protected override void DoCross(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childchromosomeFirst, ref List<Gen> childchromosomeSecond)
        {
            for (int i = 0; i < chromosomeFirst.Count; i++)
            {
                RNGCSP rngcsp = new RNGCSP();
                if (rngcsp.GetRandomNum(0, 2) == 1)
                {
                    childchromosomeFirst.Add(chromosomeFirst[i]);
                }
                else
                {
                    childchromosomeFirst.Add(chromosomeSecond[i]);
                }

                if (rngcsp.GetRandomNum(0, 2) == 1)
                {
                    childchromosomeSecond.Add(chromosomeFirst[i]);
                }
                else
                {
                    childchromosomeSecond.Add(chromosomeSecond[i]);
                }
            }
        }
    }
}
