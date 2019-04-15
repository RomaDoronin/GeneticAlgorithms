using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class PopulationGroup : IPopulation
    {
        private List<IPopulation> _populationList = new List<IPopulation>();

        //private int _iterator;
        //private bool _isEnd;

        // Новый функционал
        public void AddPopulation(ref IPopulation population)
        {
            _populationList.Add(population);
        }

        // Поддержка интерфейса IPopulation
        public void AddIndivid(Individ individ)
        {
            throw new NotImplementedException();
        }

        public int GetCurrSize()
        {
            int sizeSum = 0;
            foreach(var population in _populationList)
            {
                sizeSum += population.GetCurrSize();
            }

            return sizeSum;
        }

        //public Individ GetFirstIndivid()
        //{
        //    _iterator = 0;
        //    _isEnd = false;
        //    return _populationList[_iterator].GetFirstIndivid();
        //}

        //public Individ GetNextIndivid()
        //{
        //    Individ individ;
        //    if (!_populationList[_iterator].IsEnd())
        //    {
        //        individ = _populationList[_iterator].GetNextIndivid();

        //        if ((_iterator == _populationList.Count - 1) && (_populationList[_iterator].IsEnd()))
        //        {
        //            _isEnd = true;
        //        }
        //    }
        //    else
        //    {
        //        _iterator++;
        //        individ = _populationList[_iterator].GetFirstIndivid();
        //    }

        //    return individ;
        //}

        //public bool IsEnd() =>_isEnd;

        public List<Individ> GetPopulationList()
        {
            List<Individ> popList = new List<Individ>();

            foreach (var population in _populationList)
            {
                List<Individ> iteratorPopList = population.GetPopulationList();
                foreach (var individ in iteratorPopList)
                {
                    popList.Add(individ);
                }
            }

            return popList;
        }

        public void SetPopulationList(List<Individ> popList)
        {
            int popListCount = 0;

            for (int j = 0; j < _populationList.Count; j++)
            {
                List<Individ> popListLoc = new List<Individ>();
                for (int i = 0; i < _populationList[j].GetCurrSize(); i++)
                {
                    popListLoc.Add(popList[popListCount]);
                    popListCount++;
                }
                _populationList[j].SetPopulationList(popListLoc);
            }
        }

        public IPopulation GetInterfaceCopy()
        {
            PopulationGroup populationGroup = new PopulationGroup();

            for (int i = 0; i <_populationList.Count; i++)
            {
                IPopulation population = _populationList[i];
                populationGroup.AddPopulation(ref population);
            }

            return populationGroup;
        }
    }
}
