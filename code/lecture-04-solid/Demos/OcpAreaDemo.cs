namespace Lecture04Solid.Demos;

public static class OcpAreaDemo
{
    interface IShape
    {
        double Area();
    }

    class Circle : IShape
    {
        public Circle(double radius) => Radius = radius;
        public double Radius { get; }
        public double Area() => Math.PI * Radius * Radius;
    }

    class Square : IShape
    {
        public Square(double side) => Side = side;
        public double Side { get; }
        public double Area() => Side * Side;
    }

    class AreaCalculator
    {
        public double Total(IEnumerable<IShape> shapes) => shapes.Sum(s => s.Area());
    }

    public static void Run()
    {
        Console.WriteLine("--- OCP: area calculator ---");

        IShape[] shapes = [new Circle(2), new Square(3)];
        var total = new AreaCalculator().Total(shapes);
        Console.WriteLine($"circle r=2 + square 3 → {total:0.00}");
        Console.WriteLine("New shape = new class. AreaCalculator does not get a new case.");
    }
}
