using System;

namespace asyn_await_test
{
    class Program
    {
        // delay
        static void DoSomething(int seconds, string mgs, ConsoleColor color)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{mgs} Start: ...");
                Console.ResetColor();
            }

            for (int i = 1; i <= seconds; i++)
            {
                lock(Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{mgs,10} {i,3}");
                    Console.ResetColor();
                }
                
                Thread.Sleep(1000);
            }

            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{mgs} End.");
                Console.ResetColor();
            }
        }

        static Task Task1()
        {
            Task t1 = new Task(() =>
            {
                DoSomething(5, "Task1: ", ConsoleColor.Red);
            });
            t1.Start();

            return t1;
        }

        static Task Task2()
        {
            Task t2 = new Task((object ob) =>
            {
                string tacvu = (string)ob;
                DoSomething(7, tacvu, ConsoleColor.Green);
            }, "Task2: ");
            t2.Start();

            return t2;
        }

        static Task Task3()
        {
            Task t3 = new Task(() =>
            {
                DoSomething(10, "Task3: ", ConsoleColor.Yellow);
            });
            t3.Start();

            return t3;
        }

        // Main
        static void Main(string[] args)
        {
            Task t1 = Task1();
            Task t2 = Task2();
            Task t3 = Task3();

            Task.WaitAll(t1, t2, t3);
            Console.WriteLine("Everything Done!! ");
        }
    }
}