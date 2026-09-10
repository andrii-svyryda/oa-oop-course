namespace Lecture08GofBehavioral.Demos;

public static class StrategyDemo
{
    interface IDiscount
    {
        decimal Apply(decimal price);
    }

    class NoDiscount : IDiscount
    {
        public decimal Apply(decimal price) => price;
    }

    class PercentOff : IDiscount
    {
        private readonly decimal _percent;

        public PercentOff(decimal percent) => _percent = percent;

        public decimal Apply(decimal price) => price * (1 - _percent / 100);
    }

    class Cart
    {
        private readonly IDiscount _discount;

        public Cart(IDiscount discount) => _discount = discount;

        public decimal Pay(decimal price) => _discount.Apply(price);
    }

    public static void Run()
    {
        Console.WriteLine("--- Strategy ---");
        Console.WriteLine($"  no discount: {new Cart(new NoDiscount()).Pay(100)}");
        Console.WriteLine($"  20% off: {new Cart(new PercentOff(20)).Pay(100)}");
        Console.WriteLine("Client picks the algorithm. Same idea as discounts in lecture 04.");
    }
}
