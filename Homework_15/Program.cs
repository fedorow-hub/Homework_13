
using Homework_15;

// вариант с использованием пула потоков
WorkingWithThreads.ProcessTasksAsync();

#region вариант с ручным созданием потоков
List<Thread> workerThreads = new List<Thread>();
static void ThreadMethod(string message, int timeout)
{
    Console.WriteLine($"{message} получено из потока: {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(timeout);
}

static List<Thread> ProcessThread()
{
    List<Thread> workerThreads = new List<Thread>();
    workerThreads.Add(new Thread(() => ThreadMethod("Сообщение из потока, созданного первым", 4000)));
    workerThreads.Add(new Thread(() => ThreadMethod("Сообщение из потока, созданного вторым", 1000)));
    workerThreads.Add(new Thread(() => ThreadMethod("Сообщение из потока, созданного третьим", 3000)));
    workerThreads.Add(new Thread(() => ThreadMethod("Сообщение из потока, созданного четвертым", 2000)));

    return workerThreads;
}

foreach (Thread thread in ProcessThread())
{
    thread.Start();
    thread.Join();
}

#endregion

Console.ReadLine();