using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;

namespace Multy
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));

            firstTask.ContinueWith(
                t => WriteLine($"The first answer is {t.Result}. Thread id  {CurrentThread.ManagedThreadId}. Is thread pool thread: {CurrentThread.IsThreadPoolThread}"));

            firstTask.Start();
            secondTask.Start();

            Sleep(TimeSpan.FromSeconds(4));

            Task contination= secondTask.ContinueWith(
                t => WriteLine($"The second answer is {t.Result}. Thread id  {CurrentThread.ManagedThreadId}. Is thread pool thread: {CurrentThread.IsThreadPoolThread}"),
                TaskContinuationOptions.ExecuteSynchronously
            );

            contination.GetAwaiter().OnCompleted(
                ()=> WriteLine($"Continuation Task Completed! Thread id {CurrentThread.ManagedThreadId}. Is thread pool thread: {CurrentThread.IsThreadPoolThread}"));

            Sleep(TimeSpan.FromSeconds(2));
            WriteLine();
            
            firstTask = new Task<int>(() =>
                  {
                      var innerTask = Task.Factory.StartNew(() => TaskMethod("Second Task", 5), TaskCreationOptions.AttachedToParent);
                      innerTask.ContinueWith(t => TaskMethod("Third Task", 2), TaskContinuationOptions.AttachedToParent);
                      return TaskMethod("First Task", 2);
                  });

            firstTask.Start();

            while (!firstTask.IsCompleted)
            {
                WriteLine(firstTask.Status);
                Sleep(TimeSpan.FromSeconds(0.5));
            }
            WriteLine(firstTask.Status);
            Sleep(TimeSpan.FromSeconds(10));
            
        }

       static int TaskMethod(string name, int seconds)
        {
            WriteLine($"Task {name} is running on a thread id {CurrentThread.ManagedThreadId}. Is thread pool thread: {CurrentThread.IsThreadPoolThread}");
            Sleep(TimeSpan.FromSeconds(seconds));
            return 42*seconds;
        }

      

    }

}
