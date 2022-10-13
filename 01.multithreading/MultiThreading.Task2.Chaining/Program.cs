/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task.Run(CreateValues)
                .ContinueWith(array => MultiplyValues(array))
                .ContinueWith(array => SortArray(array))
                .ContinueWith(array => CountAverageValue(array))
                .Wait();

            Console.ReadLine();
        }

        static int[] CreateValues()
        {
            Console.WriteLine($"The first task:");
            Random random = new Random();
            int[] array = new int[10];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 100);
                Console.WriteLine($"Create value – {array[i]}");
            }

            return array;
        }

        static int[] MultiplyValues(Task<int[]> arr)
        {
            Console.WriteLine($"The second task:");
            Random random = new Random();
            int multiplier = random.Next(1, 100);
            Console.WriteLine($"Multiplier – {multiplier}");

            var array = arr.Result;
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] * multiplier;;
                Console.WriteLine($"Multiply value – {array[i]}");
            }

            return array;
        }

        static int[] SortArray(Task<int[]> arr)
        {
            var array = arr.Result;
            Array.Sort(array);
            Console.WriteLine($"Sort array: ");
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            return array;
        }

        static void CountAverageValue(Task<int[]> arr)
        {
            var array = arr.Result;
            double avg = Enumerable.Average(array.AsEnumerable());
            Console.WriteLine($"Average - {avg}");
        }
    }
}
