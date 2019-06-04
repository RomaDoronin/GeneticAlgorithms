using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Объекты внутри рюкзака
    /// </summary>
    struct Object
    {
        public int price;
        public int weight;

        public Object(int _price, int _weight)
        {
            price = _price;
            weight = _weight;
        }
    }

    /// <summary>
    /// Задача о рюкзаке
    /// </summary>
    class BackpackTask : ITask
    {
        private List<Object> _objectList;
        private int _maxWeight;
        private int _maxNumOfObject;
        private VectorSolutionDouble _solution;

        // Дополнительный функционал
        public BackpackTask()
        {
            _solution = new VectorSolutionDouble();
        }

        public void SetMaxWieght(int maxWeight) => _maxWeight = maxWeight;

        public void SetObjectList(List<Object> objectList) => _objectList = objectList;
        public int GetSize() => _objectList.Count;

        public void SetMaxNumOfObject(int maxNumOfObject) => _maxNumOfObject = maxNumOfObject;

        public VectorSolutionDouble GetSolution() => _solution;

        // Реализация интерфейса
        public Individ GenerateInitialSolution()
        {
            Individ individ;
            do
            {
                List<double> prechromosome = new List<double>();
                for (int i = 0; i < _objectList.Count; i++)
                {
                    prechromosome.Add(RNGCSP.GetRandomNum(0, 1000) % (_maxNumOfObject + 1));
                }
                VectorSolutionDouble solution = new VectorSolutionDouble();
                solution.SetResult(prechromosome);
                individ = Coder(solution);
            } while (!LimitationsFunction(individ));

            return individ;
        }

        public bool LimitationsFunction(Individ individ)
        {
            List<double> prechromosome = Decoder(individ).GetResult();
            double weightSum = 0.0;
            for (int i = 0; i < prechromosome.Count; i++)
            {
                weightSum += prechromosome[i] * _objectList[i].weight;
            }

            return weightSum <= _maxWeight;
        }

        public double TargetFunction(Individ individ)
        {
            List<double> prechromosome = Decoder(individ).GetResult();
            double priceSum = 0.0;
            for (int i = 0; i < prechromosome.Count; i++)
            {
                priceSum += prechromosome[i] * _objectList[i].price;
            }

            return priceSum;
        }

        public Individ Coder(VectorSolutionDouble solution)
        {
            List<Gen> chromosome = new List<Gen>();
            int lenghtOfGen = MathFunction.Log2(_maxNumOfObject) + 1;

            foreach (var val in solution.GetResult())
            {
                String genStr = CuclNumSys.CulcNumberSystem(val.ToString(), _maxNumOfObject + 1, 2);

                while (genStr.Length < lenghtOfGen)
                {
                    genStr = "0" + genStr;
                }

                List<short> alleleList = new List<short>();
                for (int i = 0; i < lenghtOfGen; i++)
                {
                    alleleList.Add(Int16.Parse(genStr.Substring(i, 1)));
                }
                Gen gen = new Gen();
                gen.SetAlleleList(alleleList);
                chromosome.Add(gen);
            }

            Individ individ = new Individ();
            individ.SetChromosome(chromosome);

            return individ;
        }

        public VectorSolutionDouble Decoder(Individ individ)
        {
            List<double> prechromosome = new List<double>();
            int lenghtOfGen = MathFunction.Log2(_maxNumOfObject) + 1;
            String inVal = "";

            foreach (var gen in individ.GetChromosome())
            {
                inVal = gen.ToString();
                
                String res = CuclNumSys.CulcNumberSystem(inVal, 2, _maxNumOfObject + 1);

                prechromosome.Add(Int32.Parse(res));
            }

            VectorSolutionDouble vectorSolution = new VectorSolutionDouble();
            vectorSolution.SetResult(prechromosome);

            return vectorSolution;
        }

        public void PrintResult()
        {
            _solution.PrintResult();
        }

        public bool CheckIndivid(Individ individ)
        {
            VectorSolutionDouble vectorSolution = Decoder(individ);

            foreach (var val in vectorSolution.GetResult())
            {
                if (val > _maxNumOfObject)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
