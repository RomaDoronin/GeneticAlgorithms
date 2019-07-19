using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class ListOperation
    {
        public static void Swap<T>(List<T> list, int firstIndex, int secondIndex)
        {
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

        public static List<T> Copy<T>(List<T> list)
        {
            List<T> c_list = new List<T>();
            foreach (var val in list)
            {
                c_list.Add(val);
            }

            return c_list;
        }

        /// <summary>
        /// Example [1,7,4] -> sum = 1 / (1/1 + 1/7 + 1/4)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double InverseSumInverseNumbers(List<double> list)
        {
            if (list.Count == 1)
            {
                return list[0];
            }

            double sum = 0.0;

            for (int i = 0; i < list.Count; i++)
            {
                sum += 1 / list[i];
            }

            return 1 / sum;
        }
    }
}
