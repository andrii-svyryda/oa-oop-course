namespace Lecture03ObjectRelationships.Demos;

public static class ExamQuestionsDemo
{
    public static void Run()
    {
        Console.WriteLine("--- How to tell them apart (exam) ---");
        Console.WriteLine("Who creates the object?");
        Console.WriteLine("  Company.HireManager → new Manager inside owner → composition");
        Console.WriteLine("  Project.Add(existingWorker) → part already lived → aggregation");
        Console.WriteLine();
        Console.WriteLine("Can you move the part to another whole?");
        Console.WriteLine("  Worker can join another project → aggregation / association");
        Console.WriteLine("  Room is created inside House and stays there → composition");
        Console.WriteLine();
        Console.WriteLine("UML: filled diamond = composition, empty diamond = aggregation");
    }
}
