﻿using System;
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
    class Recombination : ACrossover
    {
        protected override void DoCrossover(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childchromosomeFirst, ref List<Gen> childchromosomeSecond)
        {
            for (int i = 0; i < chromosomeFirst.Count; i++)
            {
                if (RNGCSP.GetRandomNum(0, 2) == 1)
                {
                    childchromosomeFirst.Add(chromosomeFirst[i]);
                }
                else
                {
                    childchromosomeFirst.Add(chromosomeSecond[i]);
                }

                if (RNGCSP.GetRandomNum(0, 2) == 1)
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
