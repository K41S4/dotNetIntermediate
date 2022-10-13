/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            OptionOne();
            Task.Delay(1000);
            OptionTwo();
            Task.Delay(1000);
            OptionThree();
            Task.Delay(1000);
            OptionFour();

            Console.ReadLine();
        }

        static void OptionOne()
        {
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");

            var antecedent = Task.Run(() => Console.WriteLine("I am antecedent. "));
            var continuation = antecedent.ContinueWith(ant => Console.WriteLine("I am continuation. "));
            continuation.Wait();
        }

        static void OptionTwo()
        {
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");

            var antecedent = Task.Run(() => throw new Exception());
            var continuation = antecedent.ContinueWith(ant => Console.WriteLine("I am continuation. "), TaskContinuationOptions.OnlyOnFaulted);
            continuation.Wait();
        }

        static void OptionThree()
        {
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            var antecedent = Task.Run(() => { Console.WriteLine($"I am antecedent on thread {Thread.CurrentThread.ManagedThreadId}. "); });
            var continuation = antecedent.ContinueWith(ant=> Console.WriteLine($"I am continuation on thread {Thread.CurrentThread.ManagedThreadId}. "), TaskContinuationOptions.ExecuteSynchronously);
            continuation.Wait();
        }

        static void OptionFour()
        {
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");

            var source = new CancellationTokenSource();
            var token = source.Token;

            var taskA = Task.Factory.StartNew(() => { Console.WriteLine($"I am antecedent on thread {Thread.CurrentThread.ManagedThreadId}. "); }, token);
            source.Cancel();
            var taskb = taskA.ContinueWith((antecedent) => Console.WriteLine($"I am continuation on thread {Thread.CurrentThread.ManagedThreadId}. "), TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
            taskb.Wait();
        }
    }
}
