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

        private List<T> ListCopy<T>(List<T> list)
        {
            List<T> c_list = new List<T>();
            foreach (var val in list)
            {
                c_list.Add(val);
            }

            return c_list;
        }

        private Gen GetGen(short val)
        {
            Gen gen = new Gen();
            gen.SetAlleleList(new List<short>() { val });

            return gen;
        }

        private void SetRandCell(int countPOne, List<Gen> genList, List<int> randIndexList)
        {
            while (countPOne != _numberOfOne)
            {
                int index = RNGCSP.GetRandomNum(0, randIndexList.Count);
                genList[randIndexList[index]] = GetGen(1);
                randIndexList.RemoveAt(index);
                countPOne++;
            }

            foreach (var index in randIndexList)
            {
                genList[index] = GetGen(0);
            }
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
                                childchromosomeFirst.Add(GetGen(1));
                            }
                            else
                            {

                                childchromosomeFirst.Add(GetGen(0));
                            }
                        }
                        else
                        {

                            childchromosomeFirst.Add(GetGen(-1));
                            randIndexList.Add(i * chromosomeFirst[i].GetAlleleList().Count + j);
                        }
                    }
                }

                childchromosomeSecond = ListCopy<Gen>(childchromosomeFirst);
                SetRandCell(countPOne, childchromosomeFirst, ListCopy<int>(randIndexList));
                SetRandCell(countPOne, childchromosomeSecond, ListCopy<int>(randIndexList));
            }
        }
    }
}
