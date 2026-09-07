namespace Lecture01OopTheory.Demos;

public static class InterfacesDemo
{
    interface IShapeAreaCalculator
    {
        double GetArea();
        string WhoAmI { get; }
    }

    class Square : IShapeAreaCalculator
    {
        private readonly int sideSize;

        public Square(int size) => sideSize = size;

        public string WhoAmI => GetType().ToString();

        public double GetArea() => sideSize * sideSize;
    }

    class Circle : IShapeAreaCalculator
    {
        private readonly int radius;

        public Circle(int radius) => this.radius = radius;

        public string WhoAmI => GetType().ToString();

        public double GetArea() => Math.PI * Math.Pow(radius, 2);
    }

    public static void Run()
    {
        Console.WriteLine("--- Interfaces ---");

        IShapeAreaCalculator circle = new Circle(10);
        IShapeAreaCalculator square = new Square(10);

        Console.WriteLine($"Circle area: {circle.GetArea():F2}");
        Console.WriteLine($"Square area: {square.GetArea()}");

        const int arraySize = 5;
        var shapes = new IShapeAreaCalculator[arraySize];
        var rnd = new Random(42);

        for (var i = 0; i < arraySize; i++)
        {
            shapes[i] = rnd.Next(10) % 2 != 0 ? new Square(i) : new Circle(i);
        }

        var totalArea = 0.0;
        foreach (var item in shapes)
        {
            Console.WriteLine($"Shape: {item.WhoAmI}, area={item.GetArea():F2}");
            totalArea += item.GetArea();
        }

        Console.WriteLine($"Total area: {totalArea:F2}");
    }
}
