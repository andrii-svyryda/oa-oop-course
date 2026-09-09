namespace Lecture04Solid.Demos;

public static class LspFileWriterDemo
{
    interface INoteWriter
    {
        string Write(string title, string body);
    }

    class PlainWriter : INoteWriter
    {
        public string Write(string title, string body) => $"{title}\n{body}";
    }

    class MarkdownWriter : INoteWriter
    {
        public string Write(string title, string body) => $"# {title}\n\n{body}";
    }

    class JsonWriter : INoteWriter
    {
        public string Write(string title, string body) =>
            $"{{\"title\":\"{title}\",\"body\":\"{body}\"}}";
    }

    public static void Run()
    {
        Console.WriteLine("--- LSP: write a note in any format ---");

        INoteWriter[] writers = [new PlainWriter(), new MarkdownWriter(), new JsonWriter()];
        foreach (var writer in writers)
        {
            Console.WriteLine($"{writer.GetType().Name}:");
            Console.WriteLine(writer.Write("SOLID", "Five rules."));
        }
    }
}
