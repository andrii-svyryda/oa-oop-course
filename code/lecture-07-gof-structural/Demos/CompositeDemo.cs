namespace Lecture07GofStructural.Demos;

public static class CompositeDemo
{
    interface IGraphic
    {
        void Draw(string indent = "");
    }

    class Circle : IGraphic
    {
        public void Draw(string indent = "") => Console.WriteLine($"{indent}circle");
    }

    class Group : IGraphic
    {
        private readonly List<IGraphic> _items = [];

        public void Add(IGraphic g) => _items.Add(g);

        public void Draw(string indent = "")
        {
            Console.WriteLine($"{indent}group");
            foreach (var g in _items)
                g.Draw(indent + "  ");
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Composite ---");
        var root = new Group();
        root.Add(new Circle());
        var nested = new Group();
        nested.Add(new Circle());
        nested.Add(new Circle());
        root.Add(nested);
        root.Draw();
        Console.WriteLine("Leaf and group share IGraphic. Client calls Draw on both.");
    }
}
