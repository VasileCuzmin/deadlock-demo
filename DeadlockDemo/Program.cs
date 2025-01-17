﻿namespace DeadlockDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object lock1 = new object();
            object lock2 = new object();

            Console.WriteLine("Starting...");


            var task1 = Task.Run(() =>
            {
                lock (lock1)
                {
                    Thread.Sleep(100);
                    lock (lock2)
                    {
                        Console.WriteLine("Finished Thread 1");
                    }
                }
            });

            var task2 = Task.Run(() =>
            {
                lock (lock2)
                {
                    Thread.Sleep(100);
                    lock (lock1)
                    {
                        Console.WriteLine("Finished Thread 2");
                    }
                }
            });

            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished...");

            Console.Read();
        }
    }
}