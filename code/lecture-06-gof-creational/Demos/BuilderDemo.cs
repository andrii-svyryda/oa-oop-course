namespace Lecture06GofCreational.Demos;

public static class BuilderDemo
{
    class SqlQuery
    {
        public required string CommandText { get; init; }
        public required int Take { get; init; }
    }

    class SqlBuilder
    {
        private string _table = "";
        private string _orderBy = "";
        private int _take = 0;

        public static SqlBuilder Select<T>() => new() { _table = typeof(T).Name };

        public SqlBuilder OrderBy(string column)
        {
            _orderBy = column;
            return this;
        }

        public SqlBuilder Take(int n)
        {
            _take = n;
            return this;
        }

        public SqlQuery Build() => new()
        {
            CommandText = $"SELECT * FROM {_table} ORDER BY {_orderBy} LIMIT {_take}",
            Take = _take
        };
    }

    class User
    {
        public DateTime RegistrationDate { get; set; }
    }

    public static void Run()
    {
        Console.WriteLine("--- Builder ---");
        var sql = SqlBuilder.Select<User>()
            .OrderBy("RegistrationDate")
            .Take(10)
            .Build();
        Console.WriteLine($"  {sql.CommandText}");
        Console.WriteLine("Same steps, another builder class could emit PostgreSQL or MS SQL.");
    }
}
