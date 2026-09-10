namespace Lecture12Multithreading.Demos;

public static class ThreadPoolDemo
{
    public static void Run()
    {
        Console.WriteLine("--- ThreadPool ---");
        using var done = new ManualResetEventSlim(false);
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Console.WriteLine($"  pool thread {Environment.CurrentManagedThreadId}");
            done.Set();
        });
        done.Wait();
        Console.WriteLine("Reuse a pool thread. Do not new Thread for every small job.");
    }
}
