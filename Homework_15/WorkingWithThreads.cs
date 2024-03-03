
namespace Homework_15;

public static class WorkingWithThreads
{
    static async Task<string> DelayAndReturnAsync(int value, string message)
    {
        await Task.Delay(TimeSpan.FromSeconds(value));

        var complexMessage = $"{message} получено из потока: {Thread.CurrentThread.ManagedThreadId}";

        return complexMessage;
    }
    static async Task AwaitAndProcessAsync(Task<string> task)
    {
        string result = await task;
        Console.WriteLine(result);
    }
    
    public static async Task ProcessTasksAsync()
    {
        Task<string> taskA = DelayAndReturnAsync(2, "Какое то сообщение из первой задачи");
        Task<string> taskB = DelayAndReturnAsync(3, "Какое то сообщение из второй задачи");
        Task<string> taskC = DelayAndReturnAsync(1, "Какое то сообщение из третьей задачи");

        Task<string> taskD = DelayAndReturnAsync(2, "Какое то сообщение из четвертой задачи");
        Task<string> taskE = DelayAndReturnAsync(3, "Какое то сообщение из пятой задачи");
        Task<string> taskF = DelayAndReturnAsync(1, "Какое то сообщение из шестой задачи");

        Task<string>[] tasks = new[] { taskA, taskB, taskC, taskD, taskE, taskF };
        IEnumerable<Task> taskQuery = from t in tasks select AwaitAndProcessAsync(t);
        Task[] processingTasks = taskQuery.ToArray();
        // Ожидать завершения всей обработки
        await Task.WhenAll(processingTasks);
    }

}


