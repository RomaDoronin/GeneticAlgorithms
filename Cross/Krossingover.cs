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
    class Krossingover : ACross
    {
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
            foreach (var breakPoint in breakPointList)
            {
                _breakPointList.Add(breakPoint);
            }
            _breakPointList.Add(1);
        }        

        protected override void DoCross(List<Gen> genomFirst, List<Gen> genomSecond, ref List<Gen> childGenomFirst, ref List<Gen> childGenomSecond)
        {
            int size = genomFirst.Count;

            // Кросинговер
            for (int i = 0; i < _breakPointList.Count - 1; i++)
            {
                int startPart = (int)(_breakPointList[i] * size);
                int endPart = (int)(_breakPointList[i + 1] * size);

                for (int j = startPart; j < endPart; j++)
                {
                    if (i % 2 == 0)
                    {
                        childGenomFirst.Add(genomFirst[j]);
                        childGenomSecond.Add(genomSecond[j]);
                    }
                    else
                    {
                        childGenomFirst.Add(genomSecond[j]);
                        childGenomSecond.Add(genomFirst[j]);
                    }
                }
            }
        }
    }
}
