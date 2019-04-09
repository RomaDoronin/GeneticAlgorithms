using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Select
{
    /// <summary>
    /// Для каждой особи считается значение функции приспособленности и ставится в соответствие ранг
    /// Количество особей соответствующего ранга попадающих в итоговую выборку завилит от заданного графика
    /// В качестве графика можно плать гиперболу - Чем больше ранг, тем меньше особей попадет в итоговую выборку
    /// </summary>
    class RankSelection : ASelect
    {
        public override void Select(ref IPopulation population, ITask task, ref ResultPair max)
        {
            throw new NotImplementedException();
        }
    }
}
