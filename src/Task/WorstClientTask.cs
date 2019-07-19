using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    public struct SEdge
    {
        public int row;
        public int col;

        public SEdge(int _row, int _col)
        {
            row = _row;
            col = _col;
        }
    }

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
        /// <summary>
        /// Количество вершин
        /// </summary>
        private int _vertexSize;
        /// <summary>
        /// Дерево решений для кодирования остовных деревьев
        /// </summary>
        private List<List<long>> _decisionTree;
        /// <summary>
        /// Количество листьев в дереве решений - количество получаемых графов с помощью дерева решений
        /// </summary>
        private static long _numVariantInDecisionTree;
        private static List<SEdge> _edgeList;

        public WorstClientTask(CMatrix speedMatrix)
        {
            _speedMatrix = speedMatrix;
            _vertexSize = speedMatrix.GetMatrixSize();
            _edgeList = new List<SEdge>();
            for (int i = 0; i < _speedMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < _speedMatrix.GetMatrixSize(); j++)
                {
                    if (_speedMatrix.GetVal(i, j) != 0)
                    {
                        _edgeList.Add(new SEdge(i, j));
                    }
                }
            }
            _edgeSize = _edgeList.Count;

            int l = _edgeSize - _vertexSize + 2;
            List<long> RefList = new List<long>();
            for (int i = 0; i < _vertexSize - 2; i++)
            {
                RefList.Add(1);
            }
            _decisionTree = new List<List<long>>();
            _decisionTree.Add(RefList);
            _numVariantInDecisionTree = DecisionTree.CreateDecisionTree(_vertexSize, l, _decisionTree);
        }

        /////////////////////////////////
        /// Реализация интерфейса ITask

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
            // Перевести набор ребер в остатки
            int l = _edgeSize - _vertexSize + 2;
            List<int> remnantsSet = Remnants.EdgeToRemnants(solution, l, _edgeSize);
            //Printers.PrintListWithTitleInOneLine(remnantsSet, "Remainder");

            // Ходим по отрезкам
            List<List<long>> modDecisionTree = new List<List<long>>();
            for (int count = 0; count < _decisionTree.Count; count++)
            {
                modDecisionTree.Add(new List<long>() { 1 });

                foreach (var leaf in _decisionTree[count])
                {
                    modDecisionTree[count].Add(leaf);
                }
            }

            // Выбираем первое ребро
            int row = modDecisionTree.Count - 1 - remnantsSet[0];
            int col = modDecisionTree[0].Count - 1;
            long leftBound = 0;
            for (int count = 0; count < remnantsSet[0]; count++)
            {
                leftBound += modDecisionTree[modDecisionTree.Count - 1 - count][col];
            }
            remnantsSet.RemoveAt(0);
            long codeNumber = Remnants.DescentBySegments(remnantsSet, modDecisionTree, row, col, leftBound);

            List<short> result = Binary.NumToBinary(codeNumber, _numVariantInDecisionTree);

            List<Gen> chromosome = new List<Gen>();

            foreach (var val in result)
            {
                Gen gen = new Gen();
                gen.SetAlleleList(new List<short>() { val } );
                chromosome.Add(gen);
            }

            Individ individ = new Individ();
            individ.SetChromosome(chromosome);
            return individ;
        }

        /// <summary>
        /// Декодирование варианта решения из хромосомы
        /// </summary>
        public VectorSolutionDouble Decoder(Individ individ)
        {
            List<short> binary = new List<short>();
            foreach (var gen in individ.GetChromosome())
            {
                binary.Add(gen.GetAlleleList()[0]);
            }

            long codeNumber = Binary.BinaryToNum(binary, _numVariantInDecisionTree);

            List<List<long>> modDecisionTree = new List<List<long>>();
            for (int count = 0; count < _decisionTree.Count; count++)
            {
                modDecisionTree.Add(new List<long>() { 1 });

                foreach (var leaf in _decisionTree[count])
                {
                    modDecisionTree[count].Add(leaf);
                }
            }

            List<int> adjacencyMatrix = new List<int>();
            for (int i = 0; i < _vertexSize * _vertexSize; i++)
            {
                adjacencyMatrix.Add(0);
            }

            int l = modDecisionTree.Count;

            List<int> result = new List<int>();
            long resSum = 0;
            int rowEl = l - 1;
            int colEl = _vertexSize - 2;

            for (int count = 0; count < l; count++)
            {
                rowEl = l - 1 - count;
                resSum += modDecisionTree[rowEl][colEl];
                if (codeNumber <= resSum)
                {
                    result.Add(count);
                    break;
                }
            }

            Remnants.FindingResidualSequence(modDecisionTree, rowEl, colEl, resSum - modDecisionTree[rowEl][colEl], codeNumber, result, 0);

            //Printers.PrintListWithTitleInOneLine(result, "Remainder");

            List<double> resultDouble = Remnants.RemnantsToVertex(result, _edgeSize, l);

            //Printers.PrintListWithTitleInOneLine(resultDouble, "Vertex");

            VectorSolutionDouble vectorSolution = new VectorSolutionDouble();
            vectorSolution.SetResult(resultDouble);
            return vectorSolution;
        }
        
        /// <summary>
        /// Генерация решения
        /// Строим действительно остовные деревья с помощью алгоритма построения
        /// </summary>
        public Individ GenerateInitialSolution()
        {
            List<double> edgeList = new List<double>();
            List<int> randList = new List<int>();
            Settes.InitNaturalList(randList, _edgeSize);

            while (true)
            {
                // Генерим значения из списка чтобы ребра не повторялись
                int randListIndex = RNGCSP.GetRandomNum(0, randList.Count);
                int randEdgeNum = randList[randListIndex];
                randList.RemoveAt(randListIndex);

                edgeList.Add(randEdgeNum);
                
                // Переводим набор ребер в матрицу скоростей для проверки на наличие циклов и связанность графа
                CMatrix speedMatrix = SpeedMatrix.EdgeListToSpeedMatrix(edgeList, _edgeList, _speedMatrix);

                if (GraphOperation.CheckNoCyclesInGraph(speedMatrix))
                {
                    if (GraphOperation.CheckGraphIsConnected(speedMatrix))
                    {
                        break;
                    }
                }
                else
                {
                    // Если мы не брейкнулись, удаляем новое ребро
                    edgeList.RemoveAt(edgeList.Count - 1);
                }
                
                // Повторяем до тех пор пока не получим связный граф без циклов
            }

            VectorSolutionDouble vectorSolution = new VectorSolutionDouble();
            vectorSolution.SetResult(edgeList);
            vectorSolution.Sort();
            return Coder(vectorSolution);
        }

        public int GetSize()
        {
            return (int)MathFunction.Log2(_numVariantInDecisionTree) + 1;
        }

        public bool LimitationsFunction(Individ individ)
        {
            // Инициализируем новую матрицу растояний
            CMatrix distancesMatrix = SpeedMatrix.IndividToSpeedMatrix(individ, this, _edgeList, _speedMatrix);

            if (MatrixOperation.GetMatrixEdgeNum(distancesMatrix) != (distancesMatrix.GetMatrixSize() - 1))
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
            CMatrix speedMatrix = SpeedMatrix.IndividToSpeedMatrix(individ, this, _edgeList, _speedMatrix);

            // Считаем матрицу максимальных скоростей
            CMatrix maxSpeedMatrix = GraphOperation.GetMaxSpeedMatrix(speedMatrix);

            // Поиск минимума в матрице
            return MatrixOperation.FindMinValInMatrix(maxSpeedMatrix) * (1 - individ.GetFineToFitnessFunction());
        }
    }
}
