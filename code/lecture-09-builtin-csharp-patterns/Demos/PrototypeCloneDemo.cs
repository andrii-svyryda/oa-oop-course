namespace Lecture09BuiltinCsharpPatterns.Demos;

public static class PrototypeCloneDemo
{
    class Engine
    {
        public int Power { get; set; }
    }

    class Car : ICloneable
    {
        public int Width { get; }
        public Engine Engine { get; set; }

        public Car(int width, Engine engine)
        {
            Width = width;
            Engine = engine;
        }

        public object Clone() => new Car(Width, Engine);

        public Car DeepClone() => new(Width, new Engine { Power = Engine.Power });
    }

    public static void Run()
    {
        Console.WriteLine("--- Prototype / ICloneable ---");
        var one = new Car(1695, new Engine { Power = 100 });
        var shallow = (Car)one.Clone();
        shallow.Engine.Power = 200;
        Console.WriteLine($"  after shallow clone change: original engine={one.Engine.Power} (same object)");

        var two = new Car(1695, new Engine { Power = 100 });
        var deep = two.DeepClone();
        deep.Engine.Power = 200;
        Console.WriteLine($"  after deep clone change: original engine={two.Engine.Power} (copy)");
        Console.WriteLine("MemberwiseClone / naive Clone copies references. Deep copy you write yourself.");
    }
}
