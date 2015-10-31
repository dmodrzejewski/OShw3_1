using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace Homework3
{
    internal class Calculator
    {
        public Object tart = new Object();
        public void Run(NumberReader reader)
        {


            var results = new List<long>();
            var numbersToCheck = new Queue<long>();
            StartComputationThreads(results, numbersToCheck);
            var progressMonitor = new ProgressMonitor(results);
            new Thread(progressMonitor.Run) { IsBackground = true }.Start();
            //lock (tart) {
            foreach (var value in reader.ReadIntegers())
            {
                numbersToCheck.Enqueue(value);
            }
            //  }
            lock (tart)
            {
                while (numbersToCheck.Count > 0)
                {
                    Thread.Sleep(100); // wait for the computation to complete.
                }
            }
            Console.WriteLine("{0} of the numbers were prime", progressMonitor.TotalCount);
        }
        public static Object thisLock = new Object();
        private static void StartComputationThreads(List<long> results, Queue<long> numbersToCheck)
        {
            lock (thisLock)
            {
                var threads = CreateThreads(results, numbersToCheck);
                threads.ForEach(thread => thread.Start());
            }
        }

        private static List<Thread> CreateThreads(List<long> results, Queue<long> numbersToCheck)
        {
            var threadCount = Environment.ProcessorCount * 2;
            Console.WriteLine("Using {0} compute threads and 1 I/O thread", threadCount);
            var threads =
                (from threadNumber in Sequence.Create(0, threadCount)
                 let calculator = new IsNumberPrimeCalculator(results, numbersToCheck)
                 let newThread =
                     new Thread(calculator.CheckIfNumbersArePrime)
                     {
                         IsBackground = true,
                         Priority = ThreadPriority.BelowNormal
                     }
                 select newThread).ToList();
            return threads;
        }
    }
}
