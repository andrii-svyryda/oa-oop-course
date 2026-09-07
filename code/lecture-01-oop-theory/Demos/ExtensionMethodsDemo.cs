namespace Lecture01OopTheory.Demos;

public static class StringExtension
{
    public static string[] ToWords(this string str) =>
        str.Split([' ', '.', '?'], StringSplitOptions.RemoveEmptyEntries);

    public static int WordsCount(this string str) => str.ToWords().Length;

    public static List<char> WordsFirstCharacters(this string str)
    {
        var characters = new List<char>();
        foreach (var word in str.ToWords())
        {
            characters.Add(word[0]);
        }

        return characters;
    }
}

public static class ExtensionMethodsDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Extension methods ---");

        const string sentence = "I am software developer? Am I?";
        var count = sentence.WordsCount();
        var firstLetters = string.Join(" ", sentence.WordsFirstCharacters());

        Console.WriteLine($"Words count: {count}");
        Console.WriteLine($"Words first letters: {firstLetters}");
    }
}
