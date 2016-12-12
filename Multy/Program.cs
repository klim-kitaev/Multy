﻿using System;
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
            TaskMethod("Main thread Task");
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
            WriteLine($"Result is: {result}");

            task = CreateTask("Task 2");
            task.RunSynchronously();
            result = task.Result;
            WriteLine($"Result is: {result}");

            task = CreateTask("Task 3");
            WriteLine(task.Status);
            task.Start();

            while (!task.IsCompleted)
            {
                WriteLine(task.Status);
                Sleep(TimeSpan.FromSeconds(0.5));
            }

            WriteLine(task.Status);
            result = task.Result;
            WriteLine($"Result is: {result}");
        }

       static int TaskMethod(string name)
        {
            WriteLine($"Task {name} is running on a thread id {CurrentThread.ManagedThreadId}. Is thread pool thread: {CurrentThread.IsThreadPoolThread}");
            Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }

        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }

    }

}
