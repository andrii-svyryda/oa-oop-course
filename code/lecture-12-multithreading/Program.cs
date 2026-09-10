using Lecture12Multithreading.Demos;

Console.WriteLine("=== Lecture 12: multithreading ===\n");

ThreadDemo.Run();
Console.WriteLine();

ThreadPoolDemo.Run();
Console.WriteLine();

RaceLockDemo.Run();
Console.WriteLine();

await TaskAsyncDemo.RunAsync();
