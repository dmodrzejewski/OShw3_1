namespace Homework3
{
    internal class ProgressMonitor
    {
        public Object tart = new Object();
        private readonly List<long> _results;
        public long TotalCount = 0;
        public ProgressMonitor(List<long> results)
        {
            _results = results;
        }
        public void Run()
        {
            //lock (tart) { 
            while (true)
            {
                {
                    Thread.Sleep(100); // wait for 1/10th of a second
                    var currentcount = _results.Count;
                    TotalCount += currentcount;
                    _results.Clear(); // Clear out the current primes to save some memory
                    if (currentcount > 0)
                    {
                        //lock (tart)
                        //{
                        Console.WriteLine("{0} primes found so Far", TotalCount);
                        //}
                    }
                    //}
                }
            }
        }
    }
}
