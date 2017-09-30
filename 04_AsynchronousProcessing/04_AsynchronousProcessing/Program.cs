using System;
using System.Threading;

namespace _04_AsynchronousProcessing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int min = int.Parse(Console.ReadLine());
            int max = int.Parse(Console.ReadLine());

            // prepare work for another thread
            var thread = new Thread(() =>
                PrintEvenNumbers(min, max)
            );

            // start another thread
            thread.Start();

            // wait for the other thread to finish
            thread.Join();

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
