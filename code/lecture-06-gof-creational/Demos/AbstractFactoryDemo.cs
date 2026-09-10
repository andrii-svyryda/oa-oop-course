namespace Lecture06GofCreational.Demos;

public static class AbstractFactoryDemo
{
    interface IButton
    {
        string Draw();
    }

    interface ICheckbox
    {
        string Draw();
    }

    interface IUiFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    class WinButton : IButton
    {
        public string Draw() => "Win button";
    }

    class WinCheckbox : ICheckbox
    {
        public string Draw() => "Win checkbox";
    }

    class MacButton : IButton
    {
        public string Draw() => "Mac button";
    }

    class MacCheckbox : ICheckbox
    {
        public string Draw() => "Mac checkbox";
    }

    class WinFactory : IUiFactory
    {
        public IButton CreateButton() => new WinButton();
        public ICheckbox CreateCheckbox() => new WinCheckbox();
    }

    class MacFactory : IUiFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();
    }

    static string Paint(IUiFactory ui) =>
        $"{ui.CreateButton().Draw()} + {ui.CreateCheckbox().Draw()}";

    public static void Run()
    {
        Console.WriteLine("--- Abstract factory ---");
        Console.WriteLine($"  {Paint(new WinFactory())}");
        Console.WriteLine($"  {Paint(new MacFactory())}");
        Console.WriteLine("One factory = one family. Client never mixes Win button with Mac checkbox.");
    }
}
