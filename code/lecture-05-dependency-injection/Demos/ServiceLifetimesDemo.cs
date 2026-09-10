using Microsoft.Extensions.DependencyInjection;

namespace Lecture05DependencyInjection.Demos;

public static class ServiceLifetimesDemo
{
    public interface IOperation
    {
        Guid Id { get; }
    }

    public interface ITransientOp : IOperation;

    public interface IScopedOp : IOperation;

    public interface ISingletonOp : IOperation;

    public class Operation : ITransientOp, IScopedOp, ISingletonOp
    {
        public Guid Id { get; } = Guid.NewGuid();
    }

    public static void Run()
    {
        Console.WriteLine("--- Service lifetimes ---");

        var services = new ServiceCollection();
        services.AddTransient<ITransientOp, Operation>();
        services.AddScoped<IScopedOp, Operation>();
        services.AddSingleton<ISingletonOp, Operation>();
        using var provider = services.BuildServiceProvider();

        PrintScope("scope 1", provider);
        PrintScope("scope 2", provider);
    }

    static void PrintScope(string name, IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var sp = scope.ServiceProvider;

        var t1 = sp.GetRequiredService<ITransientOp>().Id;
        var t2 = sp.GetRequiredService<ITransientOp>().Id;
        var s1 = sp.GetRequiredService<IScopedOp>().Id;
        var s2 = sp.GetRequiredService<IScopedOp>().Id;
        var n1 = sp.GetRequiredService<ISingletonOp>().Id;
        var n2 = sp.GetRequiredService<ISingletonOp>().Id;

        Console.WriteLine(name);
        Console.WriteLine($"  Transient  {Short(t1)}  {Short(t2)}  {(t1 == t2 ? "same" : "different")}");
        Console.WriteLine($"  Scoped     {Short(s1)}  {Short(s2)}  {(s1 == s2 ? "same" : "different")}");
        Console.WriteLine($"  Singleton  {Short(n1)}  {Short(n2)}  {(n1 == n2 ? "same" : "different")}");
    }

    static string Short(Guid id) => id.ToString("N")[..8];
}
