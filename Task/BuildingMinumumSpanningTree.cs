using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms.Task
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
                    if (_distancesMatrix.GetVal(i,j) != 0)
                    {
                        edgeSize++;
                    }
                }
            }
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

        public Individ GenerateInitialSolution()
        {
            List<Gen> chromosome = new List<Gen>();

            for (int i = 0; i < GetSize(); i++)
            {
                Gen gen = new Gen();
                gen.SetAlleleList(new List<short>() { (short)RNGCSP.GetRandomNum(0,2) });
                chromosome.Add(gen);
            }

            Individ individ = new Individ();
            individ.SetChromosome(chromosome);
            return individ;
        }

        public int GetSize()
        {
            return edgeSize;
        }

        public bool LimitationsFunction(Individ individ)
        {
            // Проверка на то что решение образует остовное дерево

            throw new NotImplementedException();
        }

        public void PrintResult()
        {
            Console.WriteLine("PrintResult +");
        }

        public int TargetFunction(Individ individ)
        {
            List<int> result = Decoder(individ).GetResult();
            int resultCount = 0;

            // Инициализируем новую матрицу растояний
            SymmetricMatrix distancesMatrix = new SymmetricMatrix(GetSize());
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

            // Считаем матрицу минимальных путей
            SymmetricMatrix shortestDistancesMatrix = DijkstraAlgorithm.GetShortestDistancesMatrix(distancesMatrix);

            // Поиск минимума в матрице
            return MatrixOperation.FindMinValInMatrix(shortestDistancesMatrix);
        }
    }
}
