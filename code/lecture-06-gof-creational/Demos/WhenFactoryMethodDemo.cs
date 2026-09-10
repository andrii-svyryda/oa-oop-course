using Microsoft.Extensions.DependencyInjection;

namespace Lecture06GofCreational.Demos;

public static class WhenFactoryMethodDemo
{
    interface IReport
    {
        string Format { get; }
        string Build();
        string Save();
    }

    class PdfReport : IReport
    {
        public string Format => "pdf";
        public string Build() => "built pdf";
        public string Save() => "saved pdf";
    }

    class ExcelReport : IReport
    {
        public string Format => "excel";
        public string Build() => "built excel";
        public string Save() => "saved excel";
    }

    class HtmlReport : IReport
    {
        public string Format => "html";
        public string Build() => "built html";
        public string Save() => "saved html";
    }

    static class ReportFactory
    {
        public static IReport Create(string type) => type switch
        {
            "pdf" => new PdfReport(),
            "excel" => new ExcelReport(),
            "html" => new HtmlReport(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }

    class ReportMailer
    {
        private readonly IReport _report;

        public ReportMailer(IReport report) => _report = report;

        public string Send() => $"mail {_report.Format}: {_report.Save()}";
    }

    abstract class ReportExporter
    {
        public string Export()
        {
            var report = Create();
            var built = report.Build();
            var saved = report.Save();
            return $"{built}, {saved}";
        }

        protected abstract IReport Create();
    }

    class PdfExporter : ReportExporter
    {
        protected override IReport Create() => new PdfReport();
    }

    class ExcelExporter : ReportExporter
    {
        protected override IReport Create() => new ExcelReport();
    }

    public static void Run()
    {
        Console.WriteLine("--- When factory method is worth it ---");

        Console.WriteLine("1) Simple factory: one switch, no extra factory classes");
        Console.WriteLine($"  {ReportFactory.Create("pdf").Save()}");
        Console.WriteLine($"  {ReportFactory.Create("excel").Save()}");
        Console.WriteLine("  New format = edit this switch (OCP). Still cheaper than a class per format.");

        Console.WriteLine("2) DI: client depends on IReport, container creates it");
        var services = new ServiceCollection();
        services.AddTransient<IReport, PdfReport>();
        services.AddTransient<ReportMailer>();
        using var provider = services.BuildServiceProvider();
        Console.WriteLine($"  {provider.GetRequiredService<ReportMailer>().Send()}");
        Console.WriteLine("  No ReportFactory at all. Same idea as lecture 05.");

        Console.WriteLine("3) Factory method: base class owns the steps, subclass only picks the product");
        ReportExporter exporter = new PdfExporter();
        Console.WriteLine($"  {exporter.Export()}");
        exporter = new ExcelExporter();
        Console.WriteLine($"  {exporter.Export()}");
        Console.WriteLine("  If the base class had only Create() and nothing else, this hierarchy would be extra.");
    }
}
