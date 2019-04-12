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
        private int _sizeAfterSelect;
        private int _startPopSize;
        private int _iterator;
        private bool _isEnd;

        // Дополнительный функционал
        public StdPopulation()
        {
            _individList = new List<Individ>();
            _sizeAfterSelect = 10;
            _startPopSize = 20;
        }

        // Реализация интерфейса
        public Individ GetFirstIndivid()
        {
            _iterator = 0;
            _isEnd = false;
            Individ individ = new Individ(_individList[_iterator]);
            return individ;
        }

        public Individ GetNextIndivid()
        {
            if (_iterator < _individList.Count - 1)
            {
                _iterator++;
            }
            else
            {
                _isEnd = true;
            }

            Individ individ = new Individ(_individList[_iterator]);
            return individ;
        }

        public bool IsEnd() => _isEnd;

        public int GetStartPopSize() => _startPopSize;
        public void SetStartPopSize(int startPopSize) => _startPopSize = startPopSize;
        public int GetSizeAfterSelect() => _sizeAfterSelect;
        public void SetSizeAfterSelect(int sizeAfterSelect) => _sizeAfterSelect = sizeAfterSelect;
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

        public void ClearOldPopulation()
        {
            List<Individ> _individList = new List<Individ>();

            for (int i = _startPopSize; i < this._individList.Count; i++)
            {
                _individList.Add(this._individList[i]);
            }

            this._individList = _individList;
        }

        public int GetCurrSize() => _individList.Count;
    }
}
