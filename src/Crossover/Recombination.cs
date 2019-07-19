using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Рекомбинация
    /// Если ген в одной позиции у родителей совпадают, то у ребенка будет такой же ген. Если геры различные по случайно выбирается один из них, который достанется ребенку
    /// </summary>
    class Recombination : ACrossover
    {
        /// <summary>
        /// Необходимое число значений "1" в геноме
        /// </summary>
        private int _numberOfOne;
        private const int ANY = -1;

        public Recombination()
        {
            _numberOfOne = ANY;
        }

        public Recombination(int numberOfOne)
        {
            _numberOfOne = numberOfOne;
        }

        protected override void DoCrossover(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childchromosomeFirst, ref List<Gen> childchromosomeSecond)
        {
            if (_numberOfOne == ANY)
            {
                for (int i = 0; i < chromosomeFirst.Count; i++)
                {
                    if (RNGCSP.GetRandomNum(0, 2) == 1)
                    {
                        childchromosomeFirst.Add(chromosomeFirst[i]);
                    }
                    else
                    {
                        childchromosomeFirst.Add(chromosomeSecond[i]);
                    }

                    if (RNGCSP.GetRandomNum(0, 2) == 1)
                    {
                        childchromosomeSecond.Add(chromosomeFirst[i]);
                    }
                    else
                    {
                        childchromosomeSecond.Add(chromosomeSecond[i]);
                    }
                }
            }
            else
            {
                int countPOne = 0;
                List<int> randIndexList = new List<int>();

                for (int i = 0; i < chromosomeFirst.Count; i++)
                {
                    for (int j = 0; j < chromosomeFirst[i].GetAlleleList().Count; j++)
                    {
                        if (chromosomeFirst[i].GetAlleleList()[j] == chromosomeSecond[i].GetAlleleList()[j])
                        {
                            if (chromosomeFirst[i].GetAlleleList()[j] == 1)
                            {
                                countPOne++;
                                childchromosomeFirst.Add(new Gen(1));
                            }
                            else
                            {

                                childchromosomeFirst.Add(new Gen(0));
                            }
                        }
                        else
                        {

                            childchromosomeFirst.Add(new Gen(-1));
                            randIndexList.Add(i * chromosomeFirst[i].GetAlleleList().Count + j);
                        }
                    }
                }

                childchromosomeSecond = ListOperation.Copy<Gen>(childchromosomeFirst);
                Settes.SetRandCell(countPOne, _numberOfOne, childchromosomeFirst, ListOperation.Copy<int>(randIndexList));
                Settes.SetRandCell(countPOne, _numberOfOne, childchromosomeSecond, ListOperation.Copy<int>(randIndexList));
            }
        }
    }
}
