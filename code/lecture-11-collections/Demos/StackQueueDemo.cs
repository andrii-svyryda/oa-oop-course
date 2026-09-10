namespace Lecture11Collections.Demos;

public static class StackQueueDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Stack / Queue ---");
        var undo = new Stack<string>();
        undo.Push("type A");
        undo.Push("type B");
        Console.WriteLine($"  Stack Pop (LIFO): {undo.Pop()}");

        var jobs = new Queue<string>();
        jobs.Enqueue("first");
        jobs.Enqueue("second");
        Console.WriteLine($"  Queue Dequeue (FIFO): {jobs.Dequeue()}");
        Console.WriteLine("Use Stack<T> and Queue<T>, not the old non-generic types.");
    }
}
