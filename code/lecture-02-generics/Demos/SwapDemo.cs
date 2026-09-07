namespace Lecture02Generics.Demos;

public static class SwapDemo
{
    public static void Swap<T>(ref T one, ref T second)
    {
        (one, second) = (second, one);
    }

    public static void SwapStruct<T>(ref T one, ref T second) where T : struct
    {
        (one, second) = (second, one);
    }

    public static void Run()
    {
        Console.WriteLine("--- Generic Swap ---");

        var a = 1;
        var b = 2;
        Swap(ref a, ref b);
        Console.WriteLine($"int: a={a}, b={b}");

        var c = 1.1f;
        var d = 2.3f;
        Swap(ref c, ref d);
        Console.WriteLine($"float: c={c}, d={d}");

        var x = 10;
        var y = 20;
        SwapStruct(ref x, ref y);
        Console.WriteLine($"struct constraint: x={x}, y={y}");
    }
}
