namespace Lecture12Multithreading.Demos;

public static class ThreadDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Thread ---");
        var finished = false;
        var worker = new Thread(() => finished = true);
        worker.Start();
        worker.Join();
        Console.WriteLine($"  worker finished: {finished}");
        Console.WriteLine("Start does not block Main. Join waits until the worker finishes.");
    }
}
