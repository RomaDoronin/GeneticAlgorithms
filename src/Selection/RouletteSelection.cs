using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    struct IndividPlusDouble
    {
        private Individ _individ;
        private double _sumVal;

        public Individ GetIndivid() => new Individ(_individ);
        public double GetSumVal() => _sumVal;
        public void SetSumVal(int sumVal) => _sumVal = sumVal;

        public IndividPlusDouble(Individ individ, double sumVal)
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

        private const int DIFFERENCE_ORDER = 1000000;

        public RouletteSelection(bool isRemoveSelectIndividFromSelectPool)
        {
            _isRemoveSelectIndividFromSelectPool = isRemoveSelectIndividFromSelectPool;
        }

        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            List<IndividPlusDouble> valueFitnessFunction = new List<IndividPlusDouble>();

            double valueFitnessFunctionSum = 0.0;

            List<Individ> iteratorPopList = currPopulation.GetPopulationList();
            double minDiff = Program.INFINITY;
            double maxDiff = -1;
            foreach (var individ in iteratorPopList)
            {
                double targetFunctionRes = FitnessFunction(individ);
                if (max.maxVal < targetFunctionRes)
                {
                    max.maxVal = targetFunctionRes;
                    max.individ = individ;
                }
                valueFitnessFunctionSum += targetFunctionRes;
                valueFitnessFunction.Add(new IndividPlusDouble(individ, valueFitnessFunctionSum));

                if (valueFitnessFunction.Count > 1)
                {
                    double diff = valueFitnessFunction[valueFitnessFunction.Count - 1].GetSumVal() - valueFitnessFunction[valueFitnessFunction.Count - 2].GetSumVal();

                    if (minDiff > diff)
                    {
                        minDiff = diff;
                    }

                    if (maxDiff < diff)
                    {
                        maxDiff = diff;
                    }
                }
            }
            
            List<Individ> popList = new List<Individ>();

            // Проверяем разницу между минимумом и максимумом для избежания проблем
            if (maxDiff > minDiff * DIFFERENCE_ORDER)
            {
                Console.WriteLine("RouletteSelection::Selection : maxDiff > minDiff * DIFFERENCE_ORDER");
                throw new InvalidProgramException();
            }

            int multiplier = 1;
            while (minDiff > 10) // 10 - "2" -> количество знаков в целых числах рулетки
            {
                multiplier *= 10;
                minDiff *= 10;
            }

            do
            {
                int rnd = RNGCSP.GetRandomNum(0, (int)(valueFitnessFunction[valueFitnessFunction.Count - 1].GetSumVal() * multiplier));
                List<IndividPlusDouble> newValueFitnessFunction = new List<IndividPlusDouble>();
                bool isFind = false;
                double additive = 0.0;
                double previousVal = 0.0;

                foreach (var indPI in valueFitnessFunction)
                {
                    if (!isFind)
                    {
                        if ((int)(indPI.GetSumVal() * multiplier) > rnd)
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
                            newValueFitnessFunction.Add(new IndividPlusDouble(indPI.GetIndivid(), indPI.GetSumVal() - additive));
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
