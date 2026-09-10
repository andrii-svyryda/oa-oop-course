namespace Lecture08GofBehavioral.Demos;

public static class TemplateMethodDemo
{
    abstract class DataImporter
    {
        public string Run() => $"{Open()} {Read()} {Parse()} {Close()}";

        protected virtual string Open() => "open";

        protected abstract string Read();

        protected virtual string Parse() => "parse";

        protected virtual string Close() => "close";
    }

    class CsvImporter : DataImporter
    {
        protected override string Read() => "read-csv";
    }

    class JsonImporter : DataImporter
    {
        protected override string Read() => "read-json";
    }

    public static void Run()
    {
        Console.WriteLine("--- Template method ---");
        Console.WriteLine($"  {new CsvImporter().Run()}");
        Console.WriteLine($"  {new JsonImporter().Run()}");
        Console.WriteLine("Base class owns the steps. Subclass fills in Read.");
    }
}
