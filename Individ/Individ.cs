using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особь
    /// </summary>
    class Individ
    {
        private List<Gen> _genom;

        public Individ()
        {
            _genom = new List<Gen>();
        }

        public void SetGenom(List<Gen> genom)
        {
            _genom.Clear();
            foreach (var gen in genom)
            {
                _genom.Add(gen);
            }
        }

        public List<Gen> GetGenom()
        {
            List<Gen> genom = new List<Gen>();
            foreach (var gen in _genom)
            {
                genom.Add(gen);
            }

            return genom;
        }

        public override string ToString()
        {
            String outStr = "";

            foreach(var gen in _genom)
            {
                outStr += gen.ToString() + " ";
            }

            return outStr;
        }
    }
}
