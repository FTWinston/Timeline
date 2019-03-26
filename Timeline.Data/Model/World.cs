using System.Collections.Generic;

namespace Timeline.Data.Model
{
    public class World : Randomizable
    {
        public GameTime Date { get; set; }

        public List<Person> LivingPeople { get; private set; }
        public List<Person> DeadPeople { get; private set; }
        public List<Person> NewPeople { get; private set; }

        public int NextEventIndex { get; set; }

        public World(WorldConfiguration configuration)
            : base(configuration, 0, 1)
        {
            Date = new GameTime() { Ticks = 0 };
            LivingPeople = new List<Person>();
            DeadPeople = new List<Person>();
            NewPeople = new List<Person>();
            NextEventIndex = 0;
        }
    }
}
