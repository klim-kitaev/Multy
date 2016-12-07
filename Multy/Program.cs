using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace Multy
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = new ThreadSample();

            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";
            threadOne.Priority = ThreadPriority.Highest;

            threadTwo.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTwo.Start();
            Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }


    }

    class ThreadSample
    {
        private bool _isStoped = false;

        public void Stop()
        {
            _isStoped = true;
        }

        public void CountNumbers()
        {
            long counter = 0;

            while (!_isStoped)
            {
                counter++;
            }

            Console.WriteLine($"{CurrentThread.Name} with {CurrentThread.Priority,11} priority has a count {counter,13:N0}");
        }
    }
}
