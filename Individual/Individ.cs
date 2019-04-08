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
        private List<int> _genom;

        public Individ()
        {
            _genom = new List<int>();
        }

        public void SetGenom(List<int> genom)
        {
            _genom.Clear();
            foreach (var gen in genom)
            {
                _genom.Add(gen);
            }
        }

        public List<int> GetGenom()
        {
            List<int> genom = new List<int>();
            foreach (var gen in _genom)
            {
                genom.Add(gen);
            }

            return genom;
        }
    }
}
