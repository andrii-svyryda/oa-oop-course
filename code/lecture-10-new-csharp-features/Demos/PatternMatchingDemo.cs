namespace Lecture10NewCsharpFeatures.Demos;

public static class PatternMatchingDemo
{
    interface IShape;

    record Circle(double Radius) : IShape;

    record Square(double Side) : IShape;

    static string Label(IShape s) => s switch
    {
        Circle c => $"circle r={c.Radius}",
        Square sq => $"square {sq.Side}",
        _ => "unknown"
    };

    public static void Run()
    {
        Console.WriteLine("--- Pattern matching ---");
        Console.WriteLine($"  {Label(new Circle(3))}");
        Console.WriteLine($"  {Label(new Square(4))}");
        Console.WriteLine("Good for reading data. New behavior you plan to extend still belongs in a type hierarchy.");
    }
}
