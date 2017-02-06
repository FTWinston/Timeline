using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    /// <summary>
    /// Defines the ideal, that is the values that would apply assuming perfect health
    /// </summary>
    public class Race : IComparable<Race>
    {
        public Race(string name)
        {
            Name = name;
            FertilityChances = new Dictionary<Race, Tuple<int, Race>>();
        }

        public string Name { get; set; }

        public Distribution Lifespan { get; set; }
        public Distribution MinChildBearingAge { get; set; }
        public Distribution MaxChildBearingAge { get; set; }

        public Dictionary<Race, Tuple<int, Race>> FertilityChances { get; set; } // 1 in N chance of a woman having a child each year, with a father of the given race, for childbearing years only. Decreases with education?

        public int CompareTo(Race other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
