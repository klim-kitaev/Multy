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
            new Thread(() => PlayMusic("the guitarist", "play anamazing solo", 5)).Start();
            new Thread(() => PlayMusic("the singer", "sing his song", 2)).Start();
        }

        static Barrier _barrier = new Barrier(2,b => WriteLine($"End of phase {b.CurrentPhaseNumber + 1}"));

        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 0; i < 3; i++)
            {
                WriteLine("----------------------------------------------");
                Sleep(TimeSpan.FromSeconds(seconds));
                WriteLine($"{name} starts to {message}");
                Sleep(TimeSpan.FromSeconds(seconds));
                WriteLine($"{name} finishes to {message}");
                _barrier.SignalAndWait();
            }
        }
    }

    


}
