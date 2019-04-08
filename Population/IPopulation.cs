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
        Individ GetFirstIndivid();
        Individ GetNextIndivid();
        bool IsEnd();

        int GetStartPopSize();
        void SetStartPopSize(int startPopSize);
        int GetSizeAfterSelect();
        void SetSizeAfterSelect(int sizeAfterSelect);
        void SetPopulationList(List<Individ> popList);

        void AddIndivid(Individ individ);
        void ClearOldPopulation();
        int GetCurrSize();
    }
}
