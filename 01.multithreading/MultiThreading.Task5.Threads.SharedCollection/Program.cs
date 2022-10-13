/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static AutoResetEvent waitForAddingHandle = new AutoResetEvent(false);
        static AutoResetEvent waitForWritingHandle = new AutoResetEvent(false);
        static bool _KeepWorking = true;

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var array = new List<int>();
            var addNumbersTask = new Thread(() => AddNumbersToArray(array));
            var printNumbersTask = new Thread(() => PrintAllNumbers(array));

            addNumbersTask.Start();
            printNumbersTask.Start();

            addNumbersTask.Join();
            printNumbersTask.Join();

            Console.ReadLine();
        }

        static void AddNumbersToArray(List<int> array)
        {
            for (int i = 0; i < 10; i++)
            {
                array.Add(i);
                waitForAddingHandle.Set();
                waitForWritingHandle.WaitOne();
            }
            _KeepWorking = false;
        }

        static void PrintAllNumbers(List<int> array)
        {
            while (_KeepWorking)
            {
                waitForAddingHandle.WaitOne();

                Console.Write("[");
                foreach (var item in array)
                {
                    Console.Write($"{item} ");
                }
                Console.Write("]");

                waitForWritingHandle.Set();
            }
        }
    }
}
