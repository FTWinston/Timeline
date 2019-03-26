namespace Timeline.Data.Model
{
    public class Map
    {
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Areas = new MapArea[width, height];
        }

        private int Width, Height;
        public MapArea[,] Areas { get; private set; }
    }
}
