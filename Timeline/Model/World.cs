using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class World
    {
        public WorldConfiguration Configuration { get; private set; }
        public GameTime Date { get; set; }
        public Random Random { get; internal set; }

        public List<Person> LivingPeople { get; private set; }
        public List<Person> DeadPeople { get; private set; }
        public List<Person> NewPeople { get; private set; }

        public World(WorldConfiguration configuration)
        {
            Configuration = configuration;
            Date = new GameTime() { Ticks = 0 };
            LivingPeople = new List<Person>();
            DeadPeople = new List<Person>();
            NewPeople = new List<Person>();
        }
    }
}
