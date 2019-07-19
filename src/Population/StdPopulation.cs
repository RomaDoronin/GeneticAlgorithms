using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class StdPopulation : IPopulation
    {
        private List<Individ> _individList;
        
        public StdPopulation()
        {
            _individList = new List<Individ>();
        }

        /////////////////////////////////
        /// Реализация интерфейса

        public List<Individ> GetPopulationList()
        {
            List<Individ> individList = new List<Individ>();
            foreach (var individ in _individList)
            {
                individList.Add(new Individ(individ));
            }

            return individList;
        }

        public void SetPopulationList(List<Individ> popList) => _individList = popList;

        public void AddIndivid(Individ individ) => _individList.Add(individ);

        public int GetCurrSize() => _individList.Count;

        public IPopulation GetInterfaceCopy()
        {
            List<Individ> individList = new List<Individ>();
            foreach (var individ in _individList)
            {
                individList.Add(individ);
            }

            StdPopulation population = new StdPopulation();
            population.SetPopulationList(individList);
            return population;
        }
    }
}
