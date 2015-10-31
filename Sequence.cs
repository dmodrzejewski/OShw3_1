using System.Collections.Generic;
using System;
namespace Homework3
{
    internal static class Sequence
    {
        public static Object tart = new object();
        public static IEnumerable<long> Create(long lowerBound, long upperBound)
        {
            lock (tart)
            {
                for (var i = lowerBound; i < upperBound; i++)
                {
                    yield return i;
                }
            }
        }
    }
}
