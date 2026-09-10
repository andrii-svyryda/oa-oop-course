using Microsoft.Extensions.DependencyInjection;

namespace Lecture05DependencyInjection.Demos;

public static class WhenToChooseDemo
{
    public class PriceCalculator
    {
        public decimal WithTax(decimal net) => net * 1.2m;
    }

    public class CurrentUser
    {
        public string Name { get; } = "Anna";
    }

    public class AppConfig
    {
        public string Title { get; } = "OOP course";
    }

    public class Checkout
    {
        private readonly PriceCalculator _prices;
        private readonly CurrentUser _user;
        private readonly AppConfig _config;

        public Checkout(PriceCalculator prices, CurrentUser user, AppConfig config)
        {
            _prices = prices;
            _user = user;
            _config = config;
        }

        public string Pay(decimal net) =>
            $"{_config.Title}: {_user.Name} pays {_prices.WithTax(net)}";
    }

    public static void Run()
    {
        Console.WriteLine("--- When to choose a lifetime ---");

        var services = new ServiceCollection();
        services.AddTransient<PriceCalculator>();
        services.AddScoped<CurrentUser>();
        services.AddSingleton<AppConfig>();
        services.AddScoped<Checkout>();
        using var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var checkout = scope.ServiceProvider.GetRequiredService<Checkout>();
        Console.WriteLine(checkout.Pay(100));
        Console.WriteLine("Transient = calculator, Scoped = current user, Singleton = config.");
    }
}
