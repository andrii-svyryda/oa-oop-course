namespace Lecture12Multithreading.Demos;

public static class RaceLockDemo
{
    static int UnsafeAdd()
    {
        var n = 0;
        var t1 = new Thread(() => { for (var i = 0; i < 10_000; i++) n++; });
        var t2 = new Thread(() => { for (var i = 0; i < 10_000; i++) n++; });
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        return n;
    }

    static int SafeAdd()
    {
        var n = 0;
        var sync = new object();
        var t1 = new Thread(() => { for (var i = 0; i < 10_000; i++) lock (sync) n++; });
        var t2 = new Thread(() => { for (var i = 0; i < 10_000; i++) lock (sync) n++; });
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        return n;
    }

    public static void Run()
    {
        Console.WriteLine("--- Race vs lock ---");
        Console.WriteLine($"  without lock: {UnsafeAdd()} (often not 20000)");
        Console.WriteLine($"  with lock: {SafeAdd()}");
        Console.WriteLine("lock(this) is a bad habit. Keep a private readonly object instead.");
    }
}
