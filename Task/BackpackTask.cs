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

        public void SetMaxWieght(int maxWeight)
        {
            _maxWeight = maxWeight;
        }

        public void SetObjectList(List<Object> objectList)
        {
            _objectList = objectList;
        }

        public void SetMaxNumOfObject(int maxNumOfObject)
        {
            _maxNumOfObject = maxNumOfObject;
        }

        private int Log2(int x)
        {
            int count = 0;
            while (x /2 != 0)
            {
                count++;
                x /= 2;
            }

            return count;
        }

        private List<int> GetGenom(List<int> preGenom)
        {
            List<int> genom = new List<int>();
            int lenghtOfGen = Log2(_maxNumOfObject) + 1;

            foreach (var preGen in preGenom)
            {
                CuclNumSys cuclNumSys = new CuclNumSys();
                String res = cuclNumSys.CulcNumberSystem(preGen.ToString(), _maxNumOfObject + 1, 2);

                while(res.Length < lenghtOfGen)
                {
                    res = "0" + res;
                }

                for (int i = 0; i < lenghtOfGen; i++)
                {
                    genom.Add(Int32.Parse(res.Substring(i, 1)));
                }                
            }

            return genom;
        }

        private List<int> GetPreGenom(List<int> genom)
        {
            List<int> preGenom = new List<int>();
            int lenghtOfGen = Log2(_maxNumOfObject) + 1;
            String inVal = "";

            foreach (var gen in genom)
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

            return preGenom;
        }

        public bool LimitationsFunctionBackpackTask(Individ individ)
        {
            List<int> genom = individ.GetGenom();
            List<int> preGenom = GetPreGenom(genom);
            int weightSum = 0;
            for (int i = 0; i < preGenom.Count; i++)
            {
                weightSum += preGenom[i] * _objectList[i].weight;
            }

            return weightSum <= _maxWeight;
        }

        private int TargetFunctionBackpackTask(Individ individ)
        {
            List<int> genom = individ.GetGenom();
            List<int> preGenom = GetPreGenom(genom);
            int priceSum = 0;
            for (int i = 0; i < preGenom.Count; i++)
            {
                priceSum += preGenom[i] * _objectList[i].price;
            }

            return priceSum;
        }

        /// <summary>
        /// Генерация решения
        /// </summary>
        /// <returns>Особь</returns>
        public Individ GenerateIndivid()
        {
            Individ individ = new Individ();
            List<int> preGenom = new List<int>();
            Random rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Millisecond);

            do
            {
                preGenom.Clear();
                for (int i = 0; i < _objectList.Count; i++)
                {
                    preGenom.Add(rnd.Next(0, 1000) % (_maxNumOfObject + 1));
                }
                individ.SetGenom(GetGenom(preGenom));
            } while (!LimitationsFunctionBackpackTask(individ));

            Thread.Sleep(100);
            return individ;
        }

        public LimitationsFunction GetLimitationsFunction()
        {
            return LimitationsFunctionBackpackTask;
        }

        public int GetSize()
        {
            return _objectList.Count;
        }

        public TargetFunction GetTargetFunction()
        {
            return TargetFunctionBackpackTask;
        }
    }
}
