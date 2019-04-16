using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class MathFunction
    {
        public static int Log2(int x)
        {
            int count = 0;
            while (x / 2 != 0)
            {
                count++;
                x /= 2;
            }

            return count;
        }
    }
}
