using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особи разбиваются на подгруппы по 2-3 особи с последующим выбором лучших из подгрупп
    /// Разбивать можно случайно - любая особь может попасть в любую подгруппу сколько угодно раз,
    /// можно детерминированно - одна особь попадает ровно в две подгруппы
    /// </summary>
    class TournamentSelection : ASelect
    {
        public override void Select(ref IPopulation population, ITask task, ref ResultPair max)
        {
            throw new NotImplementedException();
        }
    }
}
