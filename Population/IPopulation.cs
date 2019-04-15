using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    interface IPopulation
    {
        // Итератор
        //Individ GetFirstIndivid();
        //Individ GetNextIndivid();
        //bool IsEnd();

        List<Individ> GetPopulationList();
        void SetPopulationList(List<Individ> popList);

        void AddIndivid(Individ individ);
        int GetCurrSize();

        IPopulation GetInterfaceCopy();
    }
}
