using System;
using System.Linq;
using System.Threading;

namespace RaceCondition
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var numbers = Enumerable.Range(1, 10).ToList();

            for (int i = 0; i < 4; i++)
            {
                var thread = new Thread(() =>
                {
                    // lock reference to heap memory for 1 thread access at a time
                    lock (numbers)
                    {
                        while (numbers.Count > 0)
                        {
                            numbers.RemoveAt(numbers.Count - 1);
                            Console.WriteLine($"Number deleted by thread: {Thread.CurrentThread.ManagedThreadId}");
                        }
                    }
                });

                thread.Start();
            }

            try
            {
                var thread = new Thread(() => ThrowAsyncException());

                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown: {ex.Message}");
            }
        }

        private static void ThrowAsyncException()
        {
            // wont be catched in try-catch block when process is on another thread
            throw new Exception();
        }
    }
}
