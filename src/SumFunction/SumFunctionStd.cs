using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class SumFunctionStd : ASumFunction
    {
        public override double SumFunc(double valFirst, double valSecond)
        {
            return valFirst + valSecond;
        }
    }
}
