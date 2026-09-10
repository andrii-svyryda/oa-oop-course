using Microsoft.Extensions.DependencyInjection;

namespace Lecture05DependencyInjection.Demos;

public static class CaptiveDependencyDemo
{
    public interface IOrderDb
    {
        Guid RequestId { get; }
    }

    public class OrderDb : IOrderDb
    {
        public Guid RequestId { get; } = Guid.NewGuid();
    }

    public class OrderCache
    {
        private readonly IOrderDb _db;
        public OrderCache(IOrderDb db) => _db = db;
        public Guid CapturedDb => _db.RequestId;
    }

    public static void Run()
    {
        Console.WriteLine("--- Captive dependency (do not copy this) ---");

        var services = new ServiceCollection();
        services.AddScoped<IOrderDb, OrderDb>();
        services.AddSingleton<OrderCache>();
        using var provider = services.BuildServiceProvider();

        Guid first;
        Guid second;
        using (var scope = provider.CreateScope())
        {
            first = scope.ServiceProvider.GetRequiredService<OrderCache>().CapturedDb;
        }

        using (var scope = provider.CreateScope())
        {
            second = scope.ServiceProvider.GetRequiredService<OrderCache>().CapturedDb;
        }

        Console.WriteLine($"OrderCache is Singleton and captured IOrderDb {Short(first)}");
        Console.WriteLine($"Next scope still sees the same Db {Short(second)} — captive, not a new request context");
    }

    static string Short(Guid id) => id.ToString("N")[..8];
}
