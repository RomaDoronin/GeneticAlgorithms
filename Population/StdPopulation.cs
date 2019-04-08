using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class StdPopulation : IPopulation
    {
        private List<Individ> individList;
        private int sizeAfterSelect;
        private int startPopSize;
        private int iterator;
        private bool isEnd;

        public StdPopulation()
        {
            individList = new List<Individ>();
            sizeAfterSelect = 10;
            startPopSize = 20;
        }

        public void AddIndivid(Individ individ)
        {
            individList.Add(individ);
        }

        public void ClearOldPopulation()
        {
            List<Individ> _individList = new List<Individ>();

            for (int i = startPopSize; i < startPopSize + sizeAfterSelect; i++)
            {
                _individList.Add(individList[i]);
            }

            individList = _individList;
        }

        public int GetCurrSize()
        {
            return individList.Count;
        }

        public Individ GetFirstIndivid()
        {
            iterator = 0;
            isEnd = false;
            return individList[iterator];
        }

        public Individ GetNextIndivid()
        {
            if (iterator < individList.Count - 1)
            {
                iterator++;
            }
            else
            {
                isEnd = true;
            }

            return individList[iterator];
        }

        public int GetSizeAfterSelect()
        {
            return sizeAfterSelect;
        }

        public int GetStartPopSize()
        {
            return startPopSize;
        }

        public bool IsEnd()
        {
            return isEnd;
        }
    }
}
