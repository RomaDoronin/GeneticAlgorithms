using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class VectorSol : ISolution
    {
        private List<int> result;

        public void PrintResult()
        {
            Console.Write("Result: [");
            foreach (var res in result)
            {
                Console.Write(res.ToString() + ", ");
            }
            Console.Write("]\n");
        }

        public void SetResult(Individ individ)
        {
            result = individ.GetGenom();
        }
    }
}
