using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Дополнительные свойста для селекции:
    /// 1. Элитарная стратегия - Лучшие особи в любом случае попадают в итоговую выборку
    /// 2. Частичная замена популяции - Часть популяции не поддается изменениям: Мутации и сркещиванию
    /// </summary>
    abstract class ASelection
    {
        /// <summary>
        /// Отбирает особей и возращает так называемый родительский пул (mating pool)
        /// </summary>
        /// <param name="population"></param>
        /// <param name="task"></param>
        /// <param name="max">Переменная для отслеживания максимального значения в генетическом алгоритме</param>
        public abstract IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize);
    }
}
