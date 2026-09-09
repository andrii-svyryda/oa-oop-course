namespace Lecture04Solid.Demos;

public static class SrpDemo
{
    class EmailAddressValidator
    {
        public bool IsValid(string address) =>
            address.Contains('@') && address.Contains('.');
    }

    class SubjectValidator
    {
        public bool IsValid(string subject) =>
            !string.IsNullOrWhiteSpace(subject) && subject.Length <= 80;
    }

    class EmailSender
    {
        public void Send(string to, string subject, string body) =>
            Console.WriteLine($"SEND to={to} subject={subject} body={body}");
    }

    public static void Run()
    {
        Console.WriteLine("--- SRP: email vs validators ---");

        var address = new EmailAddressValidator();
        var subject = new SubjectValidator();
        var sender = new EmailSender();

        var to = "anna@academy.ua";
        var title = "Lab 1";
        if (address.IsValid(to) && subject.IsValid(title))
        {
            sender.Send(to, title, "Please open the repo.");
        }

        Console.WriteLine("Change spam rules → SubjectValidator. Change SMTP → EmailSender. Two reasons, two types.");
    }
}
