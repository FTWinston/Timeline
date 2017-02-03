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
    public class Race
    {
        public string Name { get; set; }

        public Distribution Lifespan { get; set; }
        public Distribution MinChildBearingAge { get; set; }
        public Distribution MaxChildBearingAge { get; set; }

        // TODO: should fertility rate be for a racial PAIRING rather than individual race?
        public double FertilityRate { get; set; } // children per woman per year, for childbearing years only. Decreases with education?
    }
}
