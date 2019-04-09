using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    struct ResultPair
    {
        public int maxVal;
        public Individ individ;
    }

    abstract class AGenAlg
    {
        protected ResultPair _max;

        protected VectorSolution _solution;
        protected IPopulation _population;
        protected ITask _task;

        protected AMutation _mutation;
        protected ASelect _select;
        protected ACross _cross;

        public virtual void SetPopulation(ref IPopulation population) => _population = population;

        public void SetMutation(AMutation mutation) => _mutation = mutation;

        public void SetSelect(ASelect select) => _select = select;

        public void SetCross(ACross cross) => _cross = cross;

        public abstract void Solve(ref ITask task);

        // Создание начальной популяции
        protected abstract void CreatePopulation();

        // Отбор
        protected virtual void Select()
        {
            _select.Select(ref _population, _task, ref _max);
        }

        // Проверка на достижение результата
        protected abstract bool Stop();

        // Отбор родителей
        protected abstract void SelectParent(ref List<int> parentNumbers, ref Individ parent1, ref Individ parent2);

        // Скрещивание
        protected virtual void Сross()
        {
            _cross.Cross();
        }

        // Мутация
        protected virtual void Mutation()
        {
            _mutation.Mutation(ref _population, _task);
        }
    }
}
