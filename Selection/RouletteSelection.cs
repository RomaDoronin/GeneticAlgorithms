using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    struct IndividPlusInt
    {
        private Individ _individ;
        private int _sumVal;

        public Individ GetIndivid() => new Individ(_individ);
        public int GetSumVal() => _sumVal;
        public void SetSumVal(int sumVal) => _sumVal = sumVal;

        public IndividPlusInt(Individ individ, int sumVal)
        {
            _individ = individ;
            _sumVal = sumVal;
        }
    }

    /// <summary>
    /// Особи для размножения выбираются случайно, но вероятность выбора особи зависит от ее значения функции приспособленности
    /// </summary>
    class RouletteSelection : ASelection
    {
        private bool _isRemoveSelectIndividFromSelectPool;

        public RouletteSelection(bool isRemoveSelectIndividFromSelectPool)
        {
            _isRemoveSelectIndividFromSelectPool = isRemoveSelectIndividFromSelectPool;
        }

        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            List<IndividPlusInt> valueFitnessFunction = new List<IndividPlusInt>();

            int valueFitnessFunctionSum = 0;

            List<Individ> iteratorPopList = currPopulation.GetPopulationList();
            foreach (var individ in iteratorPopList)
            {
                int targetFunctionRes = FitnessFunction(individ);
                if (max.maxVal < targetFunctionRes)
                {
                    max.maxVal = targetFunctionRes;
                    max.individ = individ;
                }
                valueFitnessFunctionSum += targetFunctionRes;
                valueFitnessFunction.Add(new IndividPlusInt(individ, valueFitnessFunctionSum));
            }
            
            List<Individ> popList = new List<Individ>();

            do
            {
                int rnd = RNGCSP.GetRandomNum(0, valueFitnessFunction[valueFitnessFunction.Count - 1].GetSumVal());
                List<IndividPlusInt> newValueFitnessFunction = new List<IndividPlusInt>();
                bool isFind = false;
                int additive = 0;
                int previousVal = 0;

                foreach (var indPI in valueFitnessFunction)
                {
                    if (!isFind)
                    {
                        if (indPI.GetSumVal() > rnd)
                        {
                            popList.Add(indPI.GetIndivid());
                            additive = indPI.GetSumVal() - previousVal;
                            isFind = true;
                        }
                        else
                        {
                            previousVal = indPI.GetSumVal();
                            if (_isRemoveSelectIndividFromSelectPool)
                                newValueFitnessFunction.Add(indPI);
                        }
                    }
                    else
                    {
                        if (_isRemoveSelectIndividFromSelectPool)
                            newValueFitnessFunction.Add(new IndividPlusInt(indPI.GetIndivid(), indPI.GetSumVal() - additive));
                    }
                }

                if (_isRemoveSelectIndividFromSelectPool)
                    valueFitnessFunction = newValueFitnessFunction;

            } while (popList.Count < matingPoolSize);

            currPopulation.SetPopulationList(popList);
            return currPopulation;
        }
    }
}
