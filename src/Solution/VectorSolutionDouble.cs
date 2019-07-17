using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class VectorSolutionDouble
    {
        private List<double> _vector;

        public void PrintResult()
        {
            Console.Write("Result: [");
            for (int i = 0; i < _vector.Count - 1; i++)
            {
                Console.Write(_vector[i].ToString() + ", ");
            }

            Console.Write(_vector[_vector.Count - 1].ToString() + "]\n");
        }

        public void SetResult(List<double> result) => _vector = result;

        public List<double> GetResult()
        {
            List<double> vector = new List<double>();
            foreach (var val in _vector)
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

        public void Sort()
        {
            SortRec(0, _vector.Count - 1);
        }

        private void SortRec(int first, int last)
        {
            int i = first, j = last;
            double x = _vector[(first + last) / 2];
            
            do
            {
                while (_vector[i] < x)
                {
                    i++;
                }

                while (_vector[j] > x)
                {
                    j--;
                }

                if (i <= j)
                {
                    if (_vector[i] > _vector[j])
                    {
                        ListOperation.Swap(_vector, i, j);
                    }
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < last)
            {
                SortRec(i, last);
            }
            if (first < j)
            {
                SortRec(first, j);
            }
        }
    }
}
