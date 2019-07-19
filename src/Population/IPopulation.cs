using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    interface IPopulation
    {
        List<Individ> GetPopulationList();
        void SetPopulationList(List<Individ> popList);
        void AddIndivid(Individ individ);
        int GetCurrSize();
        IPopulation GetInterfaceCopy();
    }
}
