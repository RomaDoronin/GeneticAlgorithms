using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    delegate double FitnessFunctionDel(Individ individ);
    delegate bool LimitationsFunctionDel(Individ individ);

    interface ITask
    {
        Individ GenerateInitialSolution();
        bool LimitationsFunction(Individ individ);
        int GetSize();
        double TargetFunction(Individ individ);
        Individ Coder(VectorSolutionInt solution);
        VectorSolutionInt Decoder(Individ individ);
        void PrintResult();
        bool CheckIndivid(Individ individ);
    }
}
