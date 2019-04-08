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
        int GetSizeAfterSelect();
        int GetCurrSize();

        void AddIndivid(Individ individ);
        void ClearOldPopulation();
    }
}
