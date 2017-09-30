using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start main");
            Console.WriteLine($"Main method on thread id: {Thread.CurrentThread.ManagedThreadId}");
            var task = Task.Run(async () =>
            {
                Console.WriteLine($"Task.Run method on thread id: {Thread.CurrentThread.ManagedThreadId}");
                var x = CalculateSlowly1Async();
                var y = CalculateSlowly3Async();
                Console.WriteLine("Waiting for long running tasks to finish.");
                Console.WriteLine(await x + await y);
            });
            task.Wait();
            Console.WriteLine($"Main method on thread id: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("End Main");
        }

        private static async Task<int> CalculateSlowly1Async()
        {
            await Task.Delay(5000);
            Console.WriteLine($"Calc1 method on thread id: {Thread.CurrentThread.ManagedThreadId}");
            return 1;
        }

        private static async Task<int> CalculateSlowly3Async()
        {
            await Task.Delay(5000);
            Console.WriteLine($"Calc3 method on thread id: {Thread.CurrentThread.ManagedThreadId}");
            return 3;
        }
    }
}
