using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Events;

namespace Timeline.Model
{
    public class WorldConfiguration
    {
        public int[] Randomness { get; set; }

        public Dictionary<string, Race> RacesByName { get; private set; }
        public ICollection<Race> Races { get { return RacesByName.Values; } }
        public List<Event> Events { get; private set; }

        public Map Map { get; set; }

        public WorldConfiguration()
        {
            RacesByName = new Dictionary<string, Race>();
            Events = new List<Event>();
        }
    }
}
