using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Printers
    {
        public static void PrintListWithTitleInOneLine<T>(List<T> list, string title)
        {
            Console.Write(title + ": [ ");
            foreach (var val in list)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine("]");
        }
    }
}
