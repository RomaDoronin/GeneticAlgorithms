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
        //private int _iterator;
        //private bool _isEnd;

        // Дополнительный функционал
        public StdPopulation()
        {
            _individList = new List<Individ>();
        }

        // Реализация интерфейса

        // Внешний итератор
        //public Individ GetFirstIndivid()
        //{
        //    _iterator = 0;
        //    _isEnd = false;
        //    Individ individ = new Individ(_individList[_iterator]);
        //    return individ;
        //}        
        //public Individ GetNextIndivid()
        //{
        //    _iterator++;
        //    Individ individ;

        //    if (_iterator == _individList.Count)
        //    {
        //        _isEnd = true;
        //        individ = new Individ();
        //    }
        //    else
        //    {
        //        individ = new Individ(_individList[_iterator]);
        //    }

             
        //    return individ;
        //}
        //public bool IsEnd() => _isEnd;
        
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
