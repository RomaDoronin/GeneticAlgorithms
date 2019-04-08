using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AGenAlg
    {
        protected ISolution _solution;
        protected IPopulation _population;
        protected ITask _task;

        public virtual void SetSolution(ref ISolution solution)
        {
            _solution = solution;
        }

        public virtual void SetPopulation(ref IPopulation population)
        {
            _population = population;
        }

        public abstract void Solve(ITask task);

        // Создание начальной популяции
        protected abstract void CreatePopulation();

        // Отбор
        protected abstract void Select();

        // Проверка на достижение результата
        protected abstract bool Stop();

        // Отбор родителей
        protected abstract void SelectParent(ref Individ parent1, ref Individ parent2);

        // Скрещивание
        protected abstract void Сross();

        // Мутация
        protected abstract void Mutation();
    }
}
