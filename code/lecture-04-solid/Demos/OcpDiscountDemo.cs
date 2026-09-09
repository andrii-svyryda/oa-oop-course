namespace Lecture04Solid.Demos;

public static class OcpDiscountDemo
{
    enum CustomerType
    {
        Regular,
        Premium,
        Newbie,
    }

    class ClosedDiscountCalculator
    {
        public double CalculateDiscount(double price, CustomerType customerType) =>
            customerType switch
            {
                CustomerType.Regular => price * 0.1,
                CustomerType.Premium => price * 0.3,
                CustomerType.Newbie => price * 0.05,
                _ => throw new ArgumentOutOfRangeException(nameof(customerType)),
            };
    }

    interface IDiscountStrategy
    {
        double CalculateDiscount(double price);
    }

    class RegularDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price) => price * 0.1;
    }

    class PremiumDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price) => price * 0.3;
    }

    class NewbieDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double price) => price * 0.05;
    }

    class DiscountCalculator
    {
        private readonly IDiscountStrategy _strategy;
        public DiscountCalculator(IDiscountStrategy strategy) => _strategy = strategy;
        public double CalculateDiscount(double price) => _strategy.CalculateDiscount(price);
    }

    public static void Run()
    {
        Console.WriteLine("--- OCP: switch vs strategy ---");

        var closed = new ClosedDiscountCalculator();
        Console.WriteLine($"BAD switch Regular 100 → {closed.CalculateDiscount(100, CustomerType.Regular)}");
        Console.WriteLine("A new customer type means editing this method.");

        foreach (IDiscountStrategy strategy in new IDiscountStrategy[]
                 {
                     new RegularDiscount(),
                     new PremiumDiscount(),
                     new NewbieDiscount(),
                 })
        {
            var calc = new DiscountCalculator(strategy);
            Console.WriteLine($"GOOD {strategy.GetType().Name} 100 → {calc.CalculateDiscount(100)}");
        }

        Console.WriteLine("A new discount is a new class. Calculator stays closed.");
    }
}
