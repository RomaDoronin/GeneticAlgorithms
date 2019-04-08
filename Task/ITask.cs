﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    interface ITask
    {
        Individ GenerateInitialSolution();
        bool LimitationsFunction(Individ individ);
        int GetSize();
        int TargetFunction(Individ individ);
        Individ Coder(VectorSolution solution);
        VectorSolution Decoder(Individ individ);
        void PrintResult();
    }
}