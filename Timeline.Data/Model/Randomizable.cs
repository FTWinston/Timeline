namespace Timeline.Data.Model
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
