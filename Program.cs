﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithms
{
    class Program
    {
        private static ITask CreateBackpackTask()
        {
            BackpackTask backpackTask = new BackpackTask();

            List<Object> objectList = new List<Object>();

            /* 0-1 Рюкзак | Max = 1125
            objectList.Add(new Object(106,12)); objectList.Add(new Object(122,11));
            objectList.Add(new Object(191,16)); objectList.Add(new Object(137,12));
            objectList.Add(new Object(167,12)); objectList.Add(new Object(153,19));
            objectList.Add(new Object(114,12)); objectList.Add(new Object(138,20));
            objectList.Add(new Object(194,16)); objectList.Add(new Object(161,10));

            backpackTask.SetObjectList(objectList);
            backpackTask.SetMaxWieght(100);
            backpackTask.SetMaxNumOfObject(1);
            */

            /* 0-3 Рюкзак | Max = 3343 */
            objectList.Add(new Object(152, 15)); objectList.Add(new Object(124, 14));
            objectList.Add(new Object(169, 19)); objectList.Add(new Object(125, 10));
            objectList.Add(new Object(125, 18)); objectList.Add(new Object(120, 18));
            objectList.Add(new Object(158, 12)); objectList.Add(new Object(200, 19));
            objectList.Add(new Object(145, 11)); objectList.Add(new Object(139, 10));

            backpackTask.SetObjectList(objectList);
            backpackTask.SetMaxWieght(300);
            backpackTask.SetMaxNumOfObject(3);

            return backpackTask;
        }

        static void Main(string[] args)
        {
            ITask task = CreateBackpackTask();
            IPopulation population = new StdPopulation();
            population.SetStartPopSize(20);
            population.SetSizeAfterSelect(10);

            AGenAlg genAlg = new BinaryGeneticAlgorithm();
            genAlg.SetPopulation(ref population);
            RandAlleleMutation randAlleleMutation = new RandAlleleMutation();
            randAlleleMutation.SetNumOfMutGen(2);
            genAlg.SetMutation(randAlleleMutation);
            genAlg.SetSelect(new CuttingSelection());
            genAlg.Solve(ref task);
            //task.PrintResult();
        }
    }
}
