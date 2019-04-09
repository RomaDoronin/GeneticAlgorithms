﻿using System;
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
    abstract class ASelect
    {
        public abstract void Select(ref IPopulation population, ITask task, ref ResultPair max);
    }
}
