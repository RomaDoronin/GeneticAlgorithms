using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.GenAlg
{
    abstract class AGenAlg
    {
        public abstract ISolution Solve(ITask task);

        // Создание начальной популяции
        protected abstract IPopulation doCreatePopulation();

        // Отбор
        protected abstract IPopulation doSelect(IPopulation population);

        // Проверка на достижение результата
        protected abstract bool doStop(IPopulation population);

        // Отбор родителей
        protected abstract IPopulation doBreed(IPopulation population);

        // Скрещивание
        protected abstract IPopulation doCross(IPopulation population);

        // Мутация
        protected abstract IPopulation doMutation(IPopulation population);
    }
}
