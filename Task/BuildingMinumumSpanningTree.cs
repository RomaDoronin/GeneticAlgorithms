using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class BuildingMinumumSpanningTree : ITask
    {
        private SymmetricMatrix _distancesMatrix;
        private int edgeSize;

        public BuildingMinumumSpanningTree(SymmetricMatrix distancesMatrix)
        {
            _distancesMatrix = distancesMatrix;
            edgeSize = 0;
            for (int i = 0; i < _distancesMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < _distancesMatrix.GetMatrixSize(); j++)
                {
                    if (_distancesMatrix.GetVal(i, j) != 0)
                    {
                        edgeSize++;
                    }
                }
            }
        }

        private SymmetricMatrix IndividToDistancesMatrix(Individ individ)
        {
            List<int> result = Decoder(individ).GetResult();
            int resultCount = 0;

            SymmetricMatrix distancesMatrix = new SymmetricMatrix(_distancesMatrix.GetMatrixSize());
            for (int i = 0; i < _distancesMatrix.GetMatrixSize(); i++)
            {
                for (int j = i; j < _distancesMatrix.GetMatrixSize(); j++)
                {
                    int val = _distancesMatrix.GetVal(i, j);
                    if (val != 0)
                    {
                        if (result[resultCount] == 1)
                        {
                            distancesMatrix.SetVal(i, j, val);
                        }
                        resultCount++;
                    }
                }
            }

            return distancesMatrix;
        }

        // Реализация интерфейса ITask
        public bool CheckIndivid(Individ individ)
        {
            return true;
        }

        public Individ Coder(VectorSolution solution)
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

        public VectorSolution Decoder(Individ individ)
        {
            List<int> result = new List<int>();

            foreach (var gen in individ.GetChromosome())
            {
                foreach (var allele in gen.GetAlleleList())
                {
                    result.Add(allele);
                }
            }

            VectorSolution vectorSolution = new VectorSolution();
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

                SymmetricMatrix distancesMatrix = IndividToDistancesMatrix(individ);

                if (GraphOperation.CheckGraphIsTree(distancesMatrix))
                {
                    //Console.Write(" +");
                    if (GraphOperation.CheckGraphIsSkeleton(distancesMatrix))
                    {
                        //Console.Write(" +");
                        if (GraphOperation.CheckGraphIsConnected(distancesMatrix))
                        {
                            //Console.Write(" +");
                            break;
                        }
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
            return edgeSize;
        }

        public bool LimitationsFunction(Individ individ)
        {
            // Проверка на то что решение образует остовное дерево

            // Инициализируем новую матрицу растояний
            SymmetricMatrix distancesMatrix = IndividToDistancesMatrix(individ);
            
            if (!GraphOperation.CheckGraphIsTree(distancesMatrix) || !GraphOperation.CheckGraphIsSkeleton(distancesMatrix) || !GraphOperation.CheckGraphIsConnected(distancesMatrix))
                return false;

            return true;
        }

        public void PrintResult()
        {
            Console.WriteLine("PrintResult +");
        }

        public int TargetFunction(Individ individ)
        {
            // Инициализируем новую матрицу растояний
            SymmetricMatrix distancesMatrix = IndividToDistancesMatrix(individ);

            // Считаем матрицу минимальных путей
            SymmetricMatrix shortestDistancesMatrix = GraphOperation.GetShortestDistancesMatrix(distancesMatrix);

            // Поиск минимума в матрице
            return MatrixOperation.FindMinValInMatrix(shortestDistancesMatrix);
        }
    }
}
