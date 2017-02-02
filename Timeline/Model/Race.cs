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
        public string Name { get; private set; }

        public Distribution Lifespan { get; private set; }
        public Distribution MinChildbearingAge { get; private set; }
        public Distribution MaxChildbearingAge { get; private set; }

        // TODO: should fertility rate be for a racial PAIRING rather than individual race?
        public Distribution FertilityRate { get; private set; } // children per woman ... across a lifetime. Decreases with education.
    }
}
