using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    abstract class AGenAlg
    {
        protected VectorSolution _solution;
        protected IPopulation _population;
        protected ITask _task;

        protected AMutation _mutation;

        public virtual void SetPopulation(ref IPopulation population) => _population = population;

        public void SetMutation(AMutation mutation) => _mutation = mutation;

        public abstract void Solve(ref ITask task);

        // Создание начальной популяции
        protected abstract void CreatePopulation();

        // Отбор
        protected abstract void Select();

        // Проверка на достижение результата
        protected abstract bool Stop();

        // Отбор родителей
        protected abstract void SelectParent(ref List<int> parentNumbers, ref Individ parent1, ref Individ parent2);

        // Скрещивание
        protected abstract void Сross();

        // Мутация
        protected virtual void Mutation()
        {
            _mutation.Mutation(ref _population, _task);
        }
    }
}
