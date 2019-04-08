using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GeneticAlgorithms
{
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

    class BackpackTask : ITask
    {
        private List<Object> _objectList;
        private int _maxWeight;
        private int _maxNumOfObject;
        private VectorSolution _solution;

        // Дополнительный функционал
        public BackpackTask() => _solution = new VectorSolution();

        public void SetMaxWieght(int maxWeight) => _maxWeight = maxWeight;

        public void SetObjectList(List<Object> objectList) => _objectList = objectList;

        public void SetMaxNumOfObject(int maxNumOfObject) => _maxNumOfObject = maxNumOfObject;

        public VectorSolution GetSolution() => _solution;

        // Реализация интерфейса
        public Individ GenerateInitialSolution()
        {
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);
            
            Individ individ;
            do
            {
                List<int> preGenom = new List<int>();
                for (int i = 0; i < _objectList.Count; i++)
                {
                    preGenom.Add(rnd.Next(0, 1000) % (_maxNumOfObject + 1));
                }
                VectorSolution solution = new VectorSolution();
                solution.SetResult(preGenom);
                individ = Coder(solution);
            } while (!LimitationsFunction(individ));

            Thread.Sleep(100);
            return individ;
        }

        public bool LimitationsFunction(Individ individ)
        {
            List<int> preGenom = Decoder(individ).GetResult();
            int weightSum = 0;
            for (int i = 0; i < preGenom.Count; i++)
            {
                weightSum += preGenom[i] * _objectList[i].weight;
            }

            return weightSum <= _maxWeight;
        }

        public int GetSize() => _objectList.Count;

        public int TargetFunction(Individ individ)
        {
            List<int> preGenom = Decoder(individ).GetResult();
            int priceSum = 0;
            for (int i = 0; i < preGenom.Count; i++)
            {
                priceSum += preGenom[i] * _objectList[i].price;
            }

            return priceSum;
        }

        public Individ Coder(VectorSolution solution)
        {
            List<int> genom = new List<int>();
            MathFunction mathFunction = new MathFunction();
            int lenghtOfGen = mathFunction.Log2(_maxNumOfObject) + 1;

            foreach (var preGen in solution.GetResult())
            {
                CuclNumSys cuclNumSys = new CuclNumSys();
                String res = cuclNumSys.CulcNumberSystem(preGen.ToString(), _maxNumOfObject + 1, 2);

                while (res.Length < lenghtOfGen)
                {
                    res = "0" + res;
                }

                for (int i = 0; i < lenghtOfGen; i++)
                {
                    genom.Add(Int32.Parse(res.Substring(i, 1)));
                }
            }

            Individ individ = new Individ();
            individ.SetGenom(genom);

            return individ;
        }

        public VectorSolution Decoder(Individ individ)
        {
            List<int> preGenom = new List<int>();
            MathFunction mathFunction = new MathFunction();
            int lenghtOfGen = mathFunction.Log2(_maxNumOfObject) + 1;
            String inVal = "";

            foreach (var gen in individ.GetGenom())
            {
                inVal += gen.ToString();
                if (inVal.Length < lenghtOfGen)
                {
                    continue;
                }

                CuclNumSys cuclNumSys = new CuclNumSys();
                String res = cuclNumSys.CulcNumberSystem(inVal, 2, _maxNumOfObject + 1);

                preGenom.Add(Int32.Parse(res));

                inVal = "";
            }

            VectorSolution vectorSolution = new VectorSolution();
            vectorSolution.SetResult(preGenom);

            return vectorSolution;
        }

        public void PrintResult()
        {
            _solution.PrintResult();
        }
    }
}
