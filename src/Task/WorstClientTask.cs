using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// В задаче мы имеем кабельную сеть. Кадный кабель имеет свою скорость передачи данных. Требуется максимизировать минимальную скорость передачи среди всех клиентов/узлов/вершин
    /// </summary>
    class WorstClientTask : ITask
    {
        /// <summary>
        /// Входная матрица растояний сети
        /// </summary>
        private CMatrix _speedMatrix;
        /// <summary>
        /// Количество ребер
        /// </summary>
        private int _edgeSize;
        private int _vertexSize;

        public WorstClientTask(CMatrix speedMatrix)
        {
            _speedMatrix = speedMatrix;
            _vertexSize = speedMatrix.GetMatrixSize();
            _edgeSize = 0;
            for (int i = 0; i < _speedMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < _speedMatrix.GetMatrixSize(); j++)
                {
                    if (_speedMatrix.GetVal(i, j) != 0)
                    {
                        _edgeSize++;
                    }
                }
            }
        }

        /// <summary>
        /// Функция перевода данных из особи в матрицу расстояний
        /// </summary>
        /// <param name="individ"></param>
        /// <returns></returns>
        private CMatrix IndividToSpeedMatrix(Individ individ)
        {
            List<double> result = Decoder(individ).GetResult();
            int resultCount = 0;

            CMatrix speedMatrix = new CMatrix(_speedMatrix.GetMatrixSize());
            for (int i = 0; i < _speedMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < _speedMatrix.GetMatrixSize(); j++)
                {
                    double val = _speedMatrix.GetVal(i, j);
                    if (val != 0)
                    {
                        if (result[resultCount] == 1)
                        {
                            speedMatrix.SetSimetricVal(i, j, val);
                        }
                        resultCount++;
                    }
                }
            }

            return speedMatrix;
        }

        private int GetMatrixEdgeNum(CMatrix matrix)
        {
            int edgeCount = 0;

            for (int i = 0; i < matrix.GetMatrixSize(); i++)
            {
                for (int j = i + 1; j < matrix.GetMatrixSize(); j++)
                {
                    if (matrix.GetVal(i, j) == 1)
                    {
                        edgeCount++;
                    }
                }
            }

            return edgeCount;
        }

        // Реализация интерфейса ITask

        /// <summary>
        /// Данная задача в дополнительных проверках особи не нуждается
        /// </summary>
        public bool CheckIndivid(Individ individ)
        {
            return true;
        }

        /// <summary>
        /// Кодирование варианта решения в хромосому
        /// </summary>
        public Individ Coder(VectorSolutionDouble solution)
        {
            List<Gen> chromosome = new List<Gen>();

            foreach (var val in solution.GetResult())
            {
                Gen gen = new Gen();
                gen.SetAlleleList(new List<short>() { (short)val } );
                chromosome.Add(gen);
            }

            Individ individ = new Individ();
            individ.SetChromosome(chromosome);
            return individ;
        }

        /// <summary>
        /// Декодирование варианта решения в хромосому
        /// </summary>
        public VectorSolutionDouble Decoder(Individ individ)
        {
            List<double> result = new List<double>();

            foreach (var gen in individ.GetChromosome())
            {
                foreach (var allele in gen.GetAlleleList())
                {
                    result.Add(allele);
                }
            }

            VectorSolutionDouble vectorSolution = new VectorSolutionDouble();
            vectorSolution.SetResult(result);
            return vectorSolution;
        }

        private void InitRandList(ref List<int> randList)
        {
            for (int i = 0; i < GetSize(); i++)
            {
                randList.Add(i);
            }
        }

        /// <summary>
        /// Генерация решения
        /// </summary>
        public Individ GenerateInitialSolution()
        {
            //Console.WriteLine("GenerateInitialSolution start");
            Individ individ = new Individ();

            List<Gen> chromosome = new List<Gen>();
            for (int i = 0; i < GetSize(); i++)
            {
                Gen gen = new Gen();
                gen.SetAlleleList(new List<short>() { 0 });
                chromosome.Add(gen);
            }

            List<int> randList = new List<int>();
            InitRandList(ref randList);

            while (true)
            {
                Gen gen = new Gen();
                gen.SetAlleleList(new List<short>() { 1 });

                int randListIndex = RNGCSP.GetRandomNum(0, randList.Count);
                int randGenNum = randList[randListIndex];
                //Console.Write(randGenNum.ToString());
                randList.RemoveAt(randListIndex);
                chromosome[randGenNum] = gen;
                individ.SetChromosome(chromosome);

                CMatrix distancesMatrix = IndividToSpeedMatrix(individ);

                if (GraphOperation.CheckNoCyclesInGraph(distancesMatrix))
                {
                    //Console.Write(" +");
                    if (GraphOperation.CheckGraphIsConnected(distancesMatrix))
                    {
                        //Console.Write(" +");
                        break;
                    }
                }
                else
                {
                    //Console.Write(" -");
                    // Возможно надо будет вообще убирать ребро из пула выбора
                    gen.SetAlleleList(new List<short>() { 0 });
                    chromosome[randGenNum] = gen;
                }

                //Console.WriteLine();
            }

            return individ;
        }

        public int GetSize()
        {
            return _edgeSize;
        }

        public bool LimitationsFunction(Individ individ)
        {
            // Инициализируем новую матрицу растояний
            CMatrix distancesMatrix = IndividToSpeedMatrix(individ);

            if (GetMatrixEdgeNum(distancesMatrix) != (distancesMatrix.GetMatrixSize() - 1))
            {
                return false;
            }

            if (!GraphOperation.CheckNoCyclesInGraph(distancesMatrix) || !GraphOperation.CheckGraphIsConnected(distancesMatrix))
                return false;

            return true;
        }

        public void PrintResult()
        {
            Console.WriteLine("PrintResult +");
        }

        public double TargetFunction(Individ individ)
        {
            if (individ.GetFineToFitnessFunction() == 1)
            {
                return 0;
            }

            // Инициализируем новую матрицу скоростней
            CMatrix speedMatrix = IndividToSpeedMatrix(individ);

            // Считаем матрицу максимальных скоростей
            CMatrix maxSpeedMatrix = GraphOperation.GetMaxSpeedMatrix(speedMatrix);

            // Поиск минимума в матрице
            return MatrixOperation.FindMinValInMatrix(maxSpeedMatrix) * (1 - individ.GetFineToFitnessFunction());
        }
    }
}
