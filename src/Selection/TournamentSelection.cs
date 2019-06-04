using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Особи разбиваются на подгруппы по 2-3 особи с последующим выбором лучших из подгрупп
    /// Различается два способа выбрать: Детерминированый и случайный:
    /// 1. Детерминированный - Выбор осуществляется с вероятностью 1
    /// 2. Случайный с вероятность меньше единицы
    /// </summary>
    class TournamentSelection : ASelection
    {
        private int _tournamentSize;
        private bool _isDeterministicChoice;

        private const int DIFFERENCE_ORDER = 1000000;

        public TournamentSelection(int tournamentSize, bool isDeterministicChoice)
        {
            _tournamentSize = tournamentSize;
            _isDeterministicChoice = isDeterministicChoice;
        }

        private Individ BestInList(FitnessFunctionDel FitnessFunction, List<Individ> tournamentGroup, ref ResultPair max)
        {
            Individ bestIndivid = tournamentGroup[0];
            double maxValFitnessFunction = FitnessFunction(tournamentGroup[0]);

            if (_isDeterministicChoice)
            {
                foreach (var individ in tournamentGroup)
                {
                    double fitnessFunctionRes = FitnessFunction(individ);

                    if (fitnessFunctionRes > maxValFitnessFunction)
                    {
                        maxValFitnessFunction = fitnessFunctionRes;
                        bestIndivid = individ;
                    }
                }
            }
            else
            {
                List<double> sumValFitnessFunctionList = new List<double>();
                double sumValFitnessFunction = 0;
                double minDiff = Program.INFINITY;
                double maxDiff = -1;
                foreach (var individ in tournamentGroup)
                {
                    sumValFitnessFunction += FitnessFunction(individ);
                    sumValFitnessFunctionList.Add(sumValFitnessFunction);

                    if (sumValFitnessFunctionList.Count > 1)
                    {
                        double diff = sumValFitnessFunctionList[sumValFitnessFunctionList.Count - 1] - sumValFitnessFunctionList[sumValFitnessFunctionList.Count - 2];

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

                // Проверяем разницу между минимумом и максимумом для избежания проблем
                if (maxDiff > minDiff * DIFFERENCE_ORDER)
                {
                    Console.WriteLine("TournamentSelection::BestInList : maxDiff > minDiff * DIFFERENCE_ORDER");
                    throw new InvalidProgramException();
                }

                int multiplier = 1;
                while(minDiff > 10) // 10 - "2" -> количество знаков в целых числах рулетки
                {
                    multiplier *= 10;
                    minDiff *= 10;
                }

                int rnd = RNGCSP.GetRandomNum(0, (int)(sumValFitnessFunction * multiplier));

                for (int i = 0; i < sumValFitnessFunctionList.Count; i++)
                {
                    if ((int)(sumValFitnessFunctionList[i] * multiplier) >= rnd)
                    {
                        if (i != 0)
                            maxValFitnessFunction = sumValFitnessFunctionList[i] - sumValFitnessFunctionList[i - 1];
                        else
                            maxValFitnessFunction = sumValFitnessFunctionList[i];
                        bestIndivid = tournamentGroup[i];
                    }
                }
            }

            if (maxValFitnessFunction > max.maxVal)
            {
                max.maxVal = maxValFitnessFunction;
                max.individ = bestIndivid;
            }

            return bestIndivid;
        }

        public override IPopulation Selection(IPopulation currPopulation, FitnessFunctionDel FitnessFunction, ref ResultPair max, int matingPoolSize)
        {
            List<Individ> populationList = new List<Individ>();
            List<Individ> popList = currPopulation.GetPopulationList();

            do
            {
                List<Individ> tournamentGroup = new List<Individ>();
                for (int i = 0; i < _tournamentSize; i++)
                {
                    int index = RNGCSP.GetRandomNum(0, popList.Count);
                    tournamentGroup.Add(popList[index]);
                    popList.Remove(popList[index]);
                }

                populationList.Add(BestInList(FitnessFunction, tournamentGroup, ref max));

            } while (populationList.Count != matingPoolSize);

            IPopulation population = currPopulation.GetInterfaceCopy();
            population.SetPopulationList(populationList);
            return population;
        }
    }
}
