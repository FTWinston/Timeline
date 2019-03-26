using System.Collections.Generic;
using Timeline.Data.Events;

namespace Timeline.Data.Model
{
    public class WorldConfiguration
    {
        public int Seed { get; }
        public Dictionary<string, Race> RacesByName { get; private set; }
        public ICollection<Race> Races { get { return RacesByName.Values; } }
        public List<Event> Events { get; private set; }

        public Map Map { get; set; }

        public WorldConfiguration(int seed)
        {
            Seed = seed;
            RacesByName = new Dictionary<string, Race>();
            Events = new List<Event>();
        }
    }
}
