namespace Timeline.Data.Model
{
    public abstract class Randomizable
    {
        protected Randomizable(int randomStart, int randomInc)
        {
            RandomStart = randomStart;
            RandomIncrement = randomInc;
        }

        public int RandomStart { get; private set; }
        public int RandomIncrement { get; private set; }
        
        public int CurrentRandomPos { get; set; }
    }
}
