using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GeneticAlgorithms
{
    class RNGCSP
    {
        private static RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();

        public int GetRandomNum(int minValue, int maxValue)
        {
            //Получаем наш случайный байт
            byte[] randombyte = new byte[1];
            rnd.GetBytes(randombyte);
            //превращаем его в число от 0 до 1
            double random_multiplyer = (randombyte[0] / 255d);
            //получаем разницу между минимальным и максимальным значением 
            int difference = maxValue - minValue - 1;
            //прибавляем к минимальному значение число от 0 до difference
            int result = (int)(minValue + Math.Floor(random_multiplyer * difference));

            return result;
        }
    }
}
