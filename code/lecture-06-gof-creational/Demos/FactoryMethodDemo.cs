namespace Lecture06GofCreational.Demos;

public static class FactoryMethodDemo
{
    interface IButton
    {
        string Draw();
    }

    class WinButton : IButton
    {
        public string Draw() => "Win button";
    }

    class MacButton : IButton
    {
        public string Draw() => "Mac button";
    }

    abstract class Dialog
    {
        public abstract IButton CreateButton();

        public string Render() => CreateButton().Draw();
    }

    class WinDialog : Dialog
    {
        public override IButton CreateButton() => new WinButton();
    }

    class MacDialog : Dialog
    {
        public override IButton CreateButton() => new MacButton();
    }

    public static void Run()
    {
        Console.WriteLine("--- Factory method ---");
        Dialog dialog = new WinDialog();
        Console.WriteLine($"  {dialog.Render()}");
        dialog = new MacDialog();
        Console.WriteLine($"  {dialog.Render()}");
        Console.WriteLine("Base class runs the scene. Subclass decides which product.");
    }
}
