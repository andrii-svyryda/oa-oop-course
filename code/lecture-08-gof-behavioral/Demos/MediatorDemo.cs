namespace Lecture08GofBehavioral.Demos;

public static class MediatorDemo
{
    interface IChatRoom
    {
        void Register(Member member);
        void SendAll(string from, string text);
    }

    class GeneralChatRoom : IChatRoom
    {
        private readonly List<Member> _members = [];

        public void Register(Member member)
        {
            _members.Add(member);
            member.Room = this;
        }

        public void SendAll(string from, string text)
        {
            foreach (var member in _members)
            {
                if (member.Name != from)
                    member.Receive(from, text);
            }
        }
    }

    class Member
    {
        public string Name { get; }
        public IChatRoom? Room { get; set; }

        public Member(string name) => Name = name;

        public void SendAll(string text) => Room?.SendAll(Name, text);

        public void Receive(string from, string text) =>
            Console.WriteLine($"  {Name} heard {from}: {text}");
    }

    public static void Run()
    {
        Console.WriteLine("--- Mediator ---");
        var room = new GeneralChatRoom();
        var walter = new Member("Walter");
        var jessy = new Member("Jessy");
        room.Register(walter);
        room.Register(jessy);
        walter.SendAll("hello");
        Console.WriteLine("Members do not hold each other. Room routes the message.");
    }
}
