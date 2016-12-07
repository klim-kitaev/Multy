using System;
using System.Threading;
using static System.Console;


namespace Multy
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            WriteLine("A thread has been aborted");
            ReadLine();
        }

        static void PrintNumbersWithDelay()
        {
            WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                WriteLine(i);
            }
        }
    }
}
