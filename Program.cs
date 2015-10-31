using System;
using System.IO;
using System.Threading;

namespace Homework3 {
    class Program {
        public static Object thisLock=new Object();
        public static Object llock=new Object();
        public static Object uuLock = new Object();
        /*  
         * Homework #3
         * 
         * For this assignment, you will alter this program to make it thread-safe.
         * Use whatever approach you prefer.
         * 
         * The current code is not thread safe. If you run it without modifications, it will probably crash.
         * 
         * A successful homework will produce the correct number of primes between 
         * one million and one hundred million and make use of all of the processor 
         * cores on the computer. A single threaded solution is not acceptable.
         * 
         * To run the program, you will first need to create an input data file. To do so, run
         * this program like this:
         * 
         * Homework3.exe --createDataFile numbers.txt
         * 
         * To run the computation, run the program like this:
         * 
         * Homework3.exe --processDataFile numbers.txt
         * 
         */
       /* public void lockThis(int name)
        {
            lock (thisLock)
            {


                //System.Threading.Thread.Sleep(10000);
            }
        }*/
        static void Main(string[] args) {

            //lock (uuLock)
            //{

                AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

                if (args.Length == 2)
                {
                    var fileName = new FileInfo(args[1]);
                    /*NumberWriter numwrite = new NumberWriter(fileName);
                    Thread lolThread = new Thread(new ThreadStart(numwrite.NumberWriter));*/

                    if (args[0] == "--createDataFile")
                    {
                        using (var writer = new NumberWriter(fileName))
                        {

                            writer.WriteIntegers(Sequence.Create(Constants.LowerBound, Constants.UpperBound));

                        }
                    }
                    else if (args[0] == "--processDataFile")
                    {
                        var startTime = DateTime.Now;
                        using (var reader = new NumberReader(fileName))
                        {
                            new Calculator().Run(reader);
                        }
                        var totalSeconds = (DateTime.Now - startTime).TotalSeconds;
                        Console.WriteLine("Program took {0} seconds to run", totalSeconds);
                        Console.ReadLine();
                    }
                    else
                    {
                        PrintUsage();
                    }
                }
                else
                {
                    PrintUsage();
                }
            //}
                Console.WriteLine("Hi");
        }

        private static void PrintUsage() {
           // lock (thisLock)
           // {
                Console.WriteLine("Usage:");
                Console.WriteLine("Homework3 --createDataFile [file name]");
                Console.WriteLine("Homework3 --processDataFile [file name]");
                Environment.ExitCode = -1;

            //}
        }

        private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //lock (llock)
            //{
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    Console.WriteLine("Unhandled exception: ");
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                    Environment.ExitCode = -2;
                }
            //}
        }
        
    }

    static class Constants {
    
        public const long LowerBound = 1000000;
        public const long UpperBound = 5000000;
    

    }
}
