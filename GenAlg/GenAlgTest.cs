using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.GenAlg
{
    class GenAlgTest : AGenAlg
    {
        public override ISolution Solve(ITask task)
        {
            throw new NotImplementedException();
        }

        protected override IPopulation doBreed(IPopulation population)
        {
            throw new NotImplementedException();
        }

        protected override IPopulation doCreatePopulation()
        {
            throw new NotImplementedException();
        }

        protected override IPopulation doCross(IPopulation population)
        {
            throw new NotImplementedException();
        }

        protected override IPopulation doMutation(IPopulation population)
        {
            throw new NotImplementedException();
        }

        protected override IPopulation doSelect(IPopulation population)
        {
            throw new NotImplementedException();
        }

        protected override bool doStop(IPopulation population)
        {
            throw new NotImplementedException();
        }
    }
}
