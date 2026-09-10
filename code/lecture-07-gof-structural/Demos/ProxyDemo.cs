namespace Lecture07GofStructural.Demos;

public static class ProxyDemo
{
    interface IImage
    {
        void Draw();
    }

    class HeavyImage : IImage
    {
        public HeavyImage() => Console.WriteLine("  loaded heavy image from disk");

        public void Draw() => Console.WriteLine("  drew heavy image");
    }

    class ImageProxy : IImage
    {
        private HeavyImage? _real;

        public void Draw()
        {
            _real ??= new HeavyImage();
            _real.Draw();
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Proxy ---");
        IImage image = new ImageProxy();
        Console.WriteLine("proxy created, file not loaded yet");
        image.Draw();
        image.Draw();
        Console.WriteLine("Second Draw reuses the same HeavyImage.");
    }
}
