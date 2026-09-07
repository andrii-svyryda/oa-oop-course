namespace Lecture01OopTheory.Demos;

public static class PolymorphismDemo
{
    class SimpleCalculator
    {
        public int Add(int a, int b, int c) => a + b + c;
        public int Add(int a, int b) => a + b;
        public double Add(double a, double b) => a + b;
    }

    class Shape
    {
        protected ConsoleColor BorderColor { get; set; }

        public virtual void Draw()
        {
            Console.WriteLine($"Base class drawing with color: {BorderColor}");
        }
    }

    class Rectangle : Shape
    {
        public int Height { get; }
        public int Width { get; }

        public Rectangle(ConsoleColor borderColor)
        {
            Height = 3;
            Width = 6;
            BorderColor = borderColor;
        }

        public override void Draw()
        {
            Console.ForegroundColor = BorderColor;
            for (var y = 0; y < Height; y++)
            {
                Console.WriteLine(new string('*', Width));
            }

            Console.ResetColor();
            base.Draw();
        }
    }

    class Triangle : Shape
    {
        public Triangle(ConsoleColor borderColor) => BorderColor = borderColor;

        public override void Draw()
        {
            Console.ForegroundColor = BorderColor;
            Console.WriteLine("  *");
            Console.WriteLine(" ***");
            Console.WriteLine("*****");
            Console.ResetColor();
            base.Draw();
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Polymorphism (overloading + virtual/override) ---");

        var calc = new SimpleCalculator();
        Console.WriteLine($"Add(1,2,3)={calc.Add(1, 2, 3)}");
        Console.WriteLine($"Add(2,5)={calc.Add(2, 5)}");
        Console.WriteLine($"Add(2.1,5.7)={calc.Add(2.1, 5.7)}");

        List<Shape> shapes =
        [
            new Rectangle(ConsoleColor.Blue),
            new Triangle(ConsoleColor.Yellow),
        ];

        foreach (var shape in shapes)
        {
            shape.Draw();
        }
    }
}
