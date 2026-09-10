namespace Lecture10NewCsharpFeatures.Demos;

public static class OperatorsDemo
{
    readonly struct Meters
    {
        public double Value { get; }
        public Meters(double value) => Value = value;
        public static implicit operator Meters(double v) => new(v);
        public static explicit operator double(Meters m) => m.Value;
    }

    public static void Run()
    {
        Console.WriteLine("--- implicit / explicit ---");
        Meters m = 1.5;
        double raw = (double)m;
        Console.WriteLine($"  meters={m.Value}, raw={raw}");
        Console.WriteLine("This is not boxing. Boxing is value type into object (lecture 02).");
    }
}
