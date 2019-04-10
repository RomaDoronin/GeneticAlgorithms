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
        private int _sizeAfterSelect;
        private int _startPopSize;
        private int _iterator;
        private bool _isEnd;

        // Дополнительный функционал
        public StdPopulation()
        {
            individList = new List<Individ>();
            _sizeAfterSelect = 10;
            _startPopSize = 20;
        }

        // Реализация интерфейса
        public Individ GetFirstIndivid()
        {
            _iterator = 0;
            _isEnd = false;
            Individ individ = new Individ(individList[_iterator]);
            return individ;
        }

        public Individ GetNextIndivid()
        {
            if (_iterator < individList.Count - 1)
            {
                _iterator++;
            }
            else
            {
                _isEnd = true;
            }

            Individ individ = new Individ(individList[_iterator]);
            return individ;
        }

        public bool IsEnd() => _isEnd;

        public int GetStartPopSize() => _startPopSize;
        public void SetStartPopSize(int startPopSize) => _startPopSize = startPopSize;
        public int GetSizeAfterSelect() => _sizeAfterSelect;
        public void SetSizeAfterSelect(int sizeAfterSelect) => _sizeAfterSelect = sizeAfterSelect;
        public void SetPopulationList(List<Individ> popList) => individList = popList;

        public void AddIndivid(Individ individ) => individList.Add(individ);

        public void ClearOldPopulation()
        {
            List<Individ> _individList = new List<Individ>();

            for (int i = _startPopSize; i < individList.Count; i++)
            {
                _individList.Add(individList[i]);
            }

            individList = _individList;
        }

        public int GetCurrSize() => individList.Count;
    }
}
