using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class WorldConfiguration
    {
        public int Seed { get; private set; }
        public List<Race> Races { get; private set; }

        public WorldConfiguration(int seed)
        {
            Seed = seed;
            Races = new List<Race>();
        }
    }
}
