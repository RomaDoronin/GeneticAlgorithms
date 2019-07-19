using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class BestInListByFitnessFunction
    {
        private const int DIFFERENCE_ORDER = 1000000;

        public static Individ BestInList(FitnessFunctionDel FitnessFunction, List<Individ> tournamentGroup, ref ResultPair max, bool isDeterministicChoice)
        {
            Individ bestIndivid = tournamentGroup[0];
            double maxValFitnessFunction = FitnessFunction(tournamentGroup[0]);

            if (isDeterministicChoice)
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
                while (minDiff > 10) // 10 - "2" -> количество знаков в целых числах рулетки
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
    }
}
