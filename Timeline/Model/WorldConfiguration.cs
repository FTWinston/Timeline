using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class WorldConfiguration
    {
        public Random Random { get; private set; }
        public List<Race> Races { get; private set; }

        public WorldConfiguration(int seed)
        {
            Races = new List<Race>();

            Random = new Random(seed);
            Randomness = new int[10000];
            for (int i = 0; i < Randomness.Length; i++)
                Randomness[i] = Random.Next(int.MinValue, int.MaxValue);
        }


        public int[] Randomness { get; private set; }
    }
}
