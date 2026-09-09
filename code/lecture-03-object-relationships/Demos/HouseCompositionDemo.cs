namespace Lecture03ObjectRelationships.Demos;

public static class HouseCompositionDemo
{
    class Room
    {
        public string Name { get; }
        public Room(string name) => Name = name;
    }

    class House
    {
        private readonly List<Room> _rooms = [];

        public House()
        {
            _rooms.Add(new Room("Kitchen"));
            _rooms.Add(new Room("Bedroom"));
        }

        public IEnumerable<string> RoomNames => _rooms.Select(r => r.Name);
    }

    public static void Run()
    {
        Console.WriteLine("--- Composition: house and rooms ---");

        var house = new House();
        Console.WriteLine($"House created its own rooms: {string.Join(", ", house.RoomNames)}");
        Console.WriteLine("Rooms are private. They are not handed out. Tear down the house — rooms go with it.");
    }
}
