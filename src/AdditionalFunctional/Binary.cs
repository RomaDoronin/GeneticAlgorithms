using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Binary
    {
        public static List<short> NumToBinary(long codeNum, long numVariantInDecisionTree)
        {
            int size = (int)MathFunction.Log2(numVariantInDecisionTree) + 1;
            List<short> result = new List<short>();
            for (int i = 0; i < size; i++)
            {
                result.Add(-1);
            }

            for (int i = 0; i < size; i++)
            {
                result[size - 1 - i] = ((short)(codeNum % 2));
                codeNum /= 2;
            }

            return result;
        }

        public static long BinaryToNum(List<short> binary, long numVariantInDecisionTree)
        {
            long size = MathFunction.Log2(numVariantInDecisionTree);
            long codeNum = 0;

            foreach (var bit in binary)
            {
                codeNum += (long)Math.Pow(2, size) * (long)bit;
                size--;
            }

            return codeNum;
        }
    }
}
