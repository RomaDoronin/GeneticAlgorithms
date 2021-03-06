﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особь
    /// Может обладать одной хромосомой или несколькими
    /// </summary>
    class Individ
    {
        /// <summary>
        /// Локус - позиция гена в хромосоме
        /// Хромосома
        /// </summary>
        private List<Gen> _chromosome;
        /// <summary>
        /// Штраф к финтесфункции за нарушение функции ограничений
        /// </summary>
        private double _fineToFitnessFunction;

        public Individ()
        {
            _chromosome = new List<Gen>();
            _fineToFitnessFunction = 0;
        }

        public Individ(Individ individ)
        {
            _chromosome = new List<Gen>();

            foreach (var gen in individ.GetChromosome())
            {
                _chromosome.Add(gen);
            }

            _fineToFitnessFunction = individ._fineToFitnessFunction;
        }

        public void SetChromosome(List<Gen> chromosome)
        {
            _chromosome.Clear();
            foreach (var gen in chromosome)
            {
                _chromosome.Add(gen);
            }
        }

        public List<Gen> GetChromosome()
        {
            List<Gen> chromosome = new List<Gen>();
            foreach (var gen in _chromosome)
            {
                Gen newGen = new Gen(gen);
                chromosome.Add(newGen);
            }

            return chromosome;
        }

        public void SetFineToFitnessFunction(double fineToFitnessFunction)
        {
            _fineToFitnessFunction = fineToFitnessFunction;
        }

        public double GetFineToFitnessFunction()
        {
            return _fineToFitnessFunction;
        }

        public override string ToString()
        {
            String outStr = " ";

            foreach(var gen in _chromosome)
            {
                outStr += gen.ToString() + " ";
            }

            return outStr;
        }
    }
}
