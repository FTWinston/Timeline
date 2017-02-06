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

        private int Width, Height;
        public MapArea[,] Areas { get; private set; }
    }
}
