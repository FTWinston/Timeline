using Timeline.Data.Model;

namespace Timeline.Data.Events
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
