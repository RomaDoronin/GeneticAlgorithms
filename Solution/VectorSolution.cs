using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class VectorSolution
    {
        private List<int> _vector;

        public void PrintResult()
        {
            Console.Write("Result: [");
            for (int i = 0; i < _vector.Count - 1; i++)
            {
                Console.Write(_vector[i].ToString() + ", ");
            }
            
            Console.Write(_vector[_vector.Count - 1].ToString() + "]\n");
        }

        public void SetResult(List<int> result) => _vector = result;

        public List<int> GetResult()
        {
            List<int> vector = new List<int>();
            foreach(var val in _vector)
            {
                vector.Add(val);
            }

            return vector;
        }

        public override string ToString()
        {
            String outStr = "[ ";

            foreach (var val in _vector)
            {
                outStr += val.ToString() + " ";
            }
            outStr += "]";

            return outStr;
        }
    }
}
