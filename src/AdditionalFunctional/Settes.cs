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
    }
}
