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
        protected override void DoCross(List<Gen> genomFirst, List<Gen> genomSecond, ref List<Gen> childGenomFirst, ref List<Gen> childGenomSecond)
        {
            for (int i = 0; i < genomFirst.Count; i++)
            {
                RNGCSP rngcsp = new RNGCSP();
                if (rngcsp.GetRandomNum(0, 2) == 1)
                {
                    childGenomFirst.Add(genomFirst[i]);
                }
                else
                {
                    childGenomFirst.Add(genomSecond[i]);
                }

                if (rngcsp.GetRandomNum(0, 2) == 1)
                {
                    childGenomSecond.Add(genomFirst[i]);
                }
                else
                {
                    childGenomSecond.Add(genomSecond[i]);
                }
            }
        }
    }
}
