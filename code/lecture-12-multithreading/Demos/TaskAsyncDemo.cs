namespace Lecture12Multithreading.Demos;

public static class TaskAsyncDemo
{
    public static async Task RunAsync()
    {
        Console.WriteLine("--- Task / async ---");
        var result = await Task.Run(() => 2 + 2);
        Console.WriteLine($"  Task.Run result={result}");
        Console.WriteLine("OS owns Thread. The runtime owns Task. Prefer async/await over raw threads.");
    }
}
