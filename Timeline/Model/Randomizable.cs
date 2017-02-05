using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public abstract class Randomizable
    {
        protected Randomizable(World world, int randomStart, int randomInc)
        {
            World = world;
            RandomStart = randomStart;
            RandomIncrement = randomInc;
        }

        public World World { get; private set; }
        public int RandomStart { get; private set; }
        public int RandomIncrement { get; private set; }
        
        public int CurrentRandomPos { get; set; }
    }
}
