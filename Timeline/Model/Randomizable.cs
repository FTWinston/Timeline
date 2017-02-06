using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public abstract class Randomizable
    {
        protected Randomizable(WorldConfiguration configuration, int randomStart, int randomInc)
        {
            Configuration = configuration;
            RandomStart = randomStart;
            RandomIncrement = randomInc;
        }

        public WorldConfiguration Configuration { get; private set; }
        public int RandomStart { get; private set; }
        public int RandomIncrement { get; private set; }
        
        public int CurrentRandomPos { get; set; }
    }
}
