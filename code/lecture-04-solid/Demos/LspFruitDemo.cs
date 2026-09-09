namespace Lecture04Solid.Demos;

public static class LspFruitDemo
{
    class Apple
    {
        public virtual string GetColor() => "Red";
    }

    class OrangeAsApple : Apple
    {
        public override string GetColor() => "Orange";
    }

    interface IFruit
    {
        string GetColor();
    }

    class AppleFruit : IFruit
    {
        public string GetColor() => "Red";
    }

    class OrangeFruit : IFruit
    {
        public string GetColor() => "Orange";
    }

    public static void Run()
    {
        Console.WriteLine("--- LSP: Orange is not an Apple ---");

        Apple lying = new OrangeAsApple();
        Console.WriteLine($"BAD Apple variable, Orange object → {lying.GetColor()} (you asked for an apple color)");

        IFruit fruit = new OrangeFruit();
        Console.WriteLine($"GOOD IFruit Orange → {fruit.GetColor()}");
        fruit = new AppleFruit();
        Console.WriteLine($"GOOD IFruit Apple → {fruit.GetColor()}");
    }
}
