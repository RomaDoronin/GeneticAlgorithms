using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Sum{x1,x2} = 1 / (1 / x1 + 1 / x2)
    /// </summary>
    class SumFunctionSpeed : ASumFunction
    {
        public override double SumFunc(double valFirst, double valSecond)
        {
            if (valFirst == 0)
                return valSecond;
            if (valSecond == 0)
                return valFirst;

            return 1 / (1 / valFirst + 1 / valSecond);
        }
    }
}
