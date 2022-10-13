/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static Semaphore _pool;
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            ThreadsRecursive(10);

            _pool = new Semaphore(initialCount: 1, maximumCount: 1);
            ThreadpoolRecursive(10);
            
            Console.ReadLine();
        }

        static void ThreadsRecursive(int data)
        {
            if (data == 0) return;
            data--;
            Console.WriteLine(data);

            var thread = new Thread(() => ThreadsRecursive(data));

            thread.Start();
            thread.Join();
        }

        static void ThreadpoolRecursive(Object state)
        {
            _pool.WaitOne();

            var data = (int)state;
            if (data == 0) return;
            data--;
            Console.WriteLine(data);

            _pool.Release();

            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadpoolRecursive), data);            
        }
    }
}
