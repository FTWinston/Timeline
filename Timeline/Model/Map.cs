using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class Map
    {
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Areas = new MapArea[width, height];
        }

        public void PopulateRandomly(int seed)
        {
            var random = new Random();
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Areas[x, y].Habitability = random.NextDouble();
        }

        private int Width, Height;
        public MapArea[,] Areas { get; private set; }
    }
}
