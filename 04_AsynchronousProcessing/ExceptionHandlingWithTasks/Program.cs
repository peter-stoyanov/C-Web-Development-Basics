using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionHandlingWithTasks
{
    internal class Program
    {
        private static int result;

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter Command:");
            try
            {
                // to catch exceptions from task => Wait !
                Task.WaitAll(Task.Run(() => CalculateSlowly()));
            }
            catch (AggregateException agEx)
            {
                // aggregate exception holds all thrown exceptions in the other thread
                foreach (var ex in agEx.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                string command = Console.ReadLine();
                if (command == "show")
                {
                    if (result == 0)
                    {
                        Console.WriteLine("Still calculating ...");
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }

                if (command == "exit") { break; }
            }
        }

        private static void CalculateSlowly()
        {
            Thread.Sleep(10000);
            throw new Exception("exceptio inside task is thrown.");
            result = 42;
        }
    }
}
