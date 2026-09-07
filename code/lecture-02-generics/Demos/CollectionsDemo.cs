namespace Lecture02Generics.Demos;

public static class CollectionsDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Generic collections (Stack, Queue, List) ---");

        var stack = new Stack<string>();
        stack.Push("one");
        stack.Push("two");
        stack.Push("three");

        Console.Write("Stack (top last): ");
        Console.WriteLine(string.Join(", ", stack));
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Peek: {stack.Peek()}");

        var queue = new Queue<string>();
        queue.Enqueue("one");
        queue.Enqueue("two");
        queue.Enqueue("three");

        Console.Write("Queue: ");
        Console.WriteLine(string.Join(", ", queue));
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Peek: {queue.Peek()}");

        var list = new List<int> { 3, 1, 4, 1, 5 };
        list.Sort();
        Console.WriteLine($"Sorted List<int>: {string.Join(", ", list)}");
    }
}
