namespace Lecture01OopTheory.Demos;

public static class AbstractShapeDemo
{
    abstract class Shape
    {
        public abstract int GetArea();
    }

    class Square : Shape
    {
        private readonly int sideSize;

        public Square(int size) => sideSize = size;

        public override int GetArea() => sideSize * sideSize;
    }

    public static void Run()
    {
        Console.WriteLine("--- Abstract classes ---");

        var square = new Square(5);
        Console.WriteLine($"Square side=5, area={square.GetArea()}");
    }
}
