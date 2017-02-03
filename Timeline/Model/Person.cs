using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class Person
    {
        public Person(World world, int seed, Race race, Gender gender, Person mother, Person father, GameTime birth)
        {
            World = world;
            Seed = seed;
            Random = new Random(seed);
            Race = race;
            Gender = gender;
            Mother = mother;
            Father = father;
            Birth = birth;

            Children = new List<Person>();
        }

        public World World { get; private set; }
        public int Seed { get; private set; }
        public Random Random { get; private set; }
        public Alignment Alignment { get; private set; }

        public string Name { get; set; }
        public Race Race { get; private set; }
        public Gender Gender { get; private set; }
        public Person Mother { get; set; }
        public Person Father { get; set; }
        public GameTime Birth { get; private set; }
        public GameTime? Death { get; set; }
        public GameTimeSpan Age { get { return World.Date - Birth; } }
        public bool IsDead { get { return Death.HasValue; } }

        public List<Person> Children { get; private set; }
    }
}
