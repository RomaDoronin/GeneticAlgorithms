using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Settes
    {
        /// <summary>
        /// Заполнение списка натуральных чисел
        /// </summary>
        /// <param name="naturalList"></param>
        /// <param name="size"></param>
        public static void InitNaturalList(List<int> naturalList, int size)
        {
            for (int i = 0; i < size; i++)
            {
                naturalList.Add(i);
            }
        }

        public static void SetRandCell(int countPOne, int numberOfOne, List<Gen> genList, List<int> randIndexList)
        {
            while (countPOne != numberOfOne)
            {
                int index = RNGCSP.GetRandomNum(0, randIndexList.Count);
                genList[randIndexList[index]] = new Gen(1);
                randIndexList.RemoveAt(index);
                countPOne++;
            }

            foreach (var index in randIndexList)
            {
                genList[index] = new Gen(0);
            }
        }
    }
}
