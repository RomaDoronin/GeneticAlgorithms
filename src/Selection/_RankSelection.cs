using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Для каждой особи считается значение функции приспособленности и ставится в соответствие ранг
    /// Количество особей соответствующего ранга попадающих в итоговую выборку завилит от заданного графика
    /// В качестве графика можно плать гиперболу - Чем больше ранг, тем меньше особей попадет в итоговую выборку
    /// </summary>
    class RankSelection : ASelection
    {
        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            throw new NotImplementedException();
        }
    }
}
