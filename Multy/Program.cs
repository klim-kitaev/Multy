using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace Multy
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Press 'Enter' to stop the timer...");
            DateTime start = DateTime.Now;
            using (_timer = new Timer(_ => TimerOperation(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)))
            {
                Sleep(TimeSpan.FromSeconds(6));
                _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4));
                ReadLine();
            }
        }

        static Timer _timer;

        static void TimerOperation(DateTime start)
        {
            TimeSpan elapsed = DateTime.Now - start;
            WriteLine($"{elapsed.Seconds} seconds from {start}. " + $"Timer thread pool thread id: { CurrentThread.ManagedThreadId}");
        }

    }

}
