using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    delegate int TargetFunction(Individ individ);
    delegate bool LimitationsFunction(Individ individ);

    interface ITask
    {
        Individ GenerateIndivid();
        LimitationsFunction GetLimitationsFunction();
        int GetSize();
        TargetFunction GetTargetFunction();
    }
}
