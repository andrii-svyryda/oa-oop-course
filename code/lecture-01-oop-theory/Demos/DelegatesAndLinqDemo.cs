namespace Lecture01OopTheory.Demos;

public static class DelegatesAndLinqDemo
{
    class CardNumberMask
    {
        public string Mask(string cardNumber, Func<string, List<string>>? parseCardNumber = null)
        {
            List<string> groups;

            if (parseCardNumber != null)
            {
                groups = parseCardNumber(cardNumber);
            }
            else
            {
                groups = [];
                const int groupSize = 4;
                for (var i = 0; i < cardNumber.Length; i += groupSize)
                {
                    groups.Add(cardNumber.Substring(i, groupSize));
                }
            }

            return $"{groups[0]} **** **** {groups[^1]}";
        }
    }

    static IEnumerable<int> GetPairs(int max)
    {
        for (var i = 1; i <= max; i++)
        {
            if (i % 2 == 0)
            {
                yield return i;
            }
        }
    }

    public static void Run()
    {
        Console.WriteLine("--- Delegates, yield, LINQ ---");

        var mask = new CardNumberMask();
        Console.WriteLine(mask.Mask("1111222233334444"));
        Console.WriteLine(mask.Mask("1121 2322 3637 5555", n =>
            n.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()));
        Console.WriteLine(mask.Mask("2111-2222-3637-5555", n =>
            n.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList()));

        Func<int, int> square = x => x * x;
        Console.WriteLine($"square(5)={square(5)}");

        Console.Write("Even numbers up to 10: ");
        Console.WriteLine(string.Join(", ", GetPairs(10)));

        var array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 41, 33, 37, 89 };
        var filtered = array.Where(x => x % 2 == 0).Where(x => x % 10 == 0);
        Console.WriteLine($"LINQ (even and divisible by 10): {string.Join(", ", filtered)}");
    }
}
