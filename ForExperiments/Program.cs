using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using System.Runtime.CompilerServices;
using System.Threading;
using System.Net;


namespace ForExperiments
{
    internal class Program
    {
        private static bool _ThreadUpdate = true;

        static void Main(string[] args)
        {

            //Thread.CurrentThread.Name = "Main thread";

            //var clock_thread = new Thread(ThreadMethod);
            //clock_thread.Name = "Other thread";
            //clock_thread.IsBackground = true;
            //clock_thread.Priority = ThreadPriority.AboveNormal;

            //clock_thread.Start(45);

            //var msg = "Hello";
            //var count = 5;
            //var timeOut = 150;
            //new Thread(() => PrintMethod(msg, count, timeOut))
            //{ IsBackground = true }.Start();

            //CheckThread();

            //for (var i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine(i);
            //}
            //var values = new List<int>();
            //var threads = new Thread[10];
            //object lock_object = new object();

            //for (var i = 0; i < threads.Length; i++)
            //{
            //    threads[i] = new Thread(() =>
            //    {
            //        for(var j = 0; j < 10; j++)
            //            lock(lock_object)
            //                values.Add(Thread.CurrentThread.ManagedThreadId);
            //    });
            //}

            ManualResetEvent manaManualResetEvent = new ManualResetEvent(false);
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            //объект синхронизации потоков
            EventWaitHandle threadGuidance = autoResetEvent;

            var testThreads = new Thread[10];

            for (var i = 0; i < testThreads.Length; i++)
            {
                var local_i = i;
                testThreads[i] = new Thread(() =>
                {
                    Console.WriteLine("Поток id:{0} - стартовал", Thread.CurrentThread.ManagedThreadId);
                    threadGuidance.WaitOne();
                    Console.WriteLine("Value: {0}", local_i);
                    Console.WriteLine("Поток id:{0} - завершился", Thread.CurrentThread.ManagedThreadId);
                });
                testThreads[i].Start();
            }
            Console.WriteLine("Готов к запуску");
            Console.ReadLine();

            threadGuidance.Set();


            //foreach (var thred in threads)
            //{
            //    thred.Start();
            //}

            Console.ReadLine();
           //Console.WriteLine(string.Join(",", values));
            Console.ReadLine();
        }
        [MethodImpl()]
        private static void PrintMethod(string Message, int Count, int Timeout)
        {
            for (var i = 0; i < Count; i++)
            {
                Console.WriteLine(Message);
                Thread.Sleep(Timeout);
            }
        }

        private static void ThreadMethod(object parameter)
        {
            var value = (int)parameter;
            Console.WriteLine(value);
            CheckThread();

            while (_ThreadUpdate)
            {
                Thread.Sleep(100);
                Console.Title = DateTime.Now.ToString();
            }
        }
        
        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("{0}:{1}", thread.ManagedThreadId, thread.Name);
        }
    }
}
