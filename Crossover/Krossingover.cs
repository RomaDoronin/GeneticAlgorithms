using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Кроссинговер - делим геном попалам и отдаем по половине потомкам от двух родителей
    /// 1. Точки разрыва - задавать в каком месте разобьется геном, возможно рандомно
    /// 2. Задавать множество точек разрыва
    /// </summary>
    class Krossingover : ACrossover
    {
        public const int RAND_SET_BREAK_POINT = -1;

        private List<double> _breakPointList;

        public Krossingover()
        {
            _breakPointList = new List<double>() { 0.0, 0.5, 1.0 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="breakPointList">([x1, x2, x3, ...]) xi ∈ (0,1)</param>
        public void SetBreakPoint(List<double> breakPointList)
        {
            _breakPointList.Clear();
            _breakPointList.Add(0);

            if (breakPointList[0] == RAND_SET_BREAK_POINT)
            {
                int rnd = RNGCSP.GetRandomNum(1, 100);
                _breakPointList.Add(rnd / 100.0);
            }
            else
            {
                foreach (var breakPoint in breakPointList)
                {
                    _breakPointList.Add(breakPoint);
                }
            }
            
            _breakPointList.Add(1);
        }        

        protected override void DoCrossover(List<Gen> chromosomeFirst, List<Gen> chromosomeSecond, ref List<Gen> childChromosomeFirst, ref List<Gen> childChromosomeSecond)
        {
            int size = chromosomeFirst.Count;

            // Кросинговер
            for (int i = 0; i < _breakPointList.Count - 1; i++)
            {
                int startPart = (int)(_breakPointList[i] * size);
                int endPart = (int)(_breakPointList[i + 1] * size);

                for (int j = startPart; j < endPart; j++)
                {
                    if (i % 2 == 0)
                    {
                        childChromosomeFirst.Add(chromosomeFirst[j]);
                        childChromosomeSecond.Add(chromosomeSecond[j]);
                    }
                    else
                    {
                        childChromosomeFirst.Add(chromosomeSecond[j]);
                        childChromosomeSecond.Add(chromosomeFirst[j]);
                    }
                }
            }
        }
    }
}
