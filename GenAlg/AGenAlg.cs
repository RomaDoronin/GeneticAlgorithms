using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    public enum OPERATION_TARGET
    {
        CHILDREN = 1,
        PARENTS = 2,
        ALL = 3
    }

    struct ResultPair
    {
        public int maxVal;
        public Individ individ;
    }

    abstract class AGenAlg
    {
        protected ResultPair _max;
        protected int _maxIterNum;

        public void SetMaxIterNum(int maxIterNum) => _maxIterNum = maxIterNum;

        protected VectorSolution _solution;
        protected ITask _task;
        protected IPopulation _population;

        public void SetPopulation(IPopulation population) => _population = population;

        private int _populationSize;
        private int _matingPoolSize;
        private int _childSize;
        
        public void SetPoolsSize(int populationSize, int matingPoolSize, int childSize)
        {
            _populationSize = populationSize;
            _matingPoolSize = matingPoolSize;
            _childSize = childSize;
        }

        protected int GetPopulationSize() => _populationSize;
        protected int GetMatingPoolSize() => _matingPoolSize;
        protected int GetChildSize() => _childSize;

        private AMutation _mutation;
        private ASelection _selection;
        private ACrossover _crossover;
        private ASelectParent _selectParent;
        private AFormationNewPopulation _formationNewPopulation;

        public void SetMutation(AMutation mutation) => _mutation = mutation;
        public void SetSelect(ASelection selection) => _selection = selection;
        public void SetCross(ACrossover crossover) => _crossover = crossover;
        public void SetSelectParent(ASelectParent selectParent) => _selectParent = selectParent;
        public void SetFormationNewPopulation(AFormationNewPopulation formationNewPopulation) => _formationNewPopulation = formationNewPopulation;

        public abstract void Solve(ref ITask task);

        protected int FitnessFunction(Individ individ)
        {
            return _task.TargetFunction(individ);
        }

        // Создание начальной популяции
        protected abstract IPopulation CreatePopulation();

        // Отбор пула родителей
        protected IPopulation Selection(IPopulation currPopulation)
        {
            return _selection.Selection(currPopulation, _task.TargetFunction, ref _max, _matingPoolSize);
        }

        // Проверка на достижение результата
        protected abstract bool Stop();

        /// <summary>
        /// Отбор двух родителей
        /// </summary>
        /// <param name="parentNumbers"></param>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        protected void SelectParent(ref List<int> parentNumbers, ref Individ parent1, ref Individ parent2)
        {
            _selectParent.SelectParent(ref parentNumbers, ref parent1, ref parent2, _population, _matingPoolSize);
        }
        
        /// <summary>
        /// Скрещивание
        /// </summary>
        protected IPopulation Сrossover(IPopulation matingPool)
        {
            return _crossover.Crossover(matingPool, _task.LimitationsFunction, _selectParent, _childSize);
        }
        
        /// <summary>
        /// Мутация
        /// </summary>
        protected void Mutation(ref IPopulation matingPool, ref IPopulation children)
        {
            _mutation.Mutation(ref matingPool, ref children, _task);
        }

        /// <summary>
        /// Формирование нового покаления
        /// </summary>
        /// <param name="matingPool"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        protected IPopulation FormationNewPopulation(IPopulation matingPool, IPopulation children)
        {
            return _formationNewPopulation.FormationNewPopulation(matingPool, children, _task.TargetFunction, _populationSize);
        }
    }
}
