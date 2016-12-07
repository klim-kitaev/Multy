using System;
using System.Threading;
using static System.Console;


namespace Multy
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            t.Join();
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
