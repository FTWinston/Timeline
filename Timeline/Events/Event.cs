using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;

namespace Timeline.Events
{
    public abstract class Event
    {
        protected Event(GameTime occursAt, MapArea location)
        {
            OccursAt = occursAt;
            Location = location;
        }

        public GameTime OccursAt { get; private set; }
        public MapArea Location { get; private set; }

        public abstract void Perform(World world);
        public abstract void Reverse(World world);
    }
}
