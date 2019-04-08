using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class CuclNumSys
    {
        private static Dictionary<char, int> moreNum = new Dictionary<char, int>
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'A', 10 },
            { 'B', 11 },
            { 'C', 12 },
            { 'D', 13 },
            { 'E', 14 },
            { 'F', 15 },
        };

        private static Dictionary<int, char> moreChar = new Dictionary<int, char>
        {
            { 0, '0' },
            { 1, '1' },
            { 2, '2' },
            { 3, '3' },
            { 4, '4' },
            { 5, '5' },
            { 6, '6' },
            { 7, '7' },
            { 8, '8' },
            { 9, '9' },
            { 10, 'A' },
            { 11, 'B' },
            { 12, 'C' },
            { 13, 'D' },
            { 14, 'E' },
            { 15, 'F' },
        };

        private static String ConvertNto10(String inVal, int firstNumSys)
        {
            int outVal = 0;

            for (int count = 0; inVal.Length != count; count++)
            {
                outVal += (int)Math.Pow(firstNumSys, count) * moreNum[inVal[inVal.Length - 1 - count]];
            }

            return outVal.ToString();
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static String Convert10toN(String inVal, int secondNumSys)
        {
            String outVal = "";

            for (int curr = Int32.Parse(inVal); curr != 0; curr /= secondNumSys)
            {
                outVal += moreChar[curr % secondNumSys];
            }

            return Reverse(outVal);
        }

        private void DeleteFirstZero(ref String str)
        {
            String resStr = "";
            bool first = false;
            foreach (var ch in str)
            {
                if (ch != '0')
                {
                    first = true;
                }

                if (first)
                {
                    resStr += ch;
                }
            }

            str = resStr;
        }

        public String CulcNumberSystem(String inVal, int firstNumSys, int secondNumSys)
        {
            DeleteFirstZero(ref inVal);
            if (inVal.Length == 0)
            {
                return "0";
            }

            String outVal = inVal;

            if (firstNumSys != 10)
            {
                outVal = ConvertNto10(inVal, firstNumSys);
            }

            if (secondNumSys != 10)
            {
                outVal = Convert10toN(outVal, secondNumSys);
            }

            return outVal;
        }

        private static bool CheckForSegment(int numSys, int min, int max)
        {
            return (numSys > min && numSys < max);
        }

        private static bool CheckForCorrectNum(String val, int numSys)
        {
            foreach (var ch in val)
            {
                if (moreNum[ch] >= numSys)
                {
                    return false;
                }
            }

            return true;
        }

        public int Culc(int inVal, int firstNumSys, int secondNumSys)
        {
            String inValStr = inVal.ToString();

            String outValStr = CulcNumberSystem(inValStr, firstNumSys, secondNumSys);

            return Int32.Parse(outValStr);
        }
    }
}
