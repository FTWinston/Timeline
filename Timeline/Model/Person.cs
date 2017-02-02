using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Model
{
    public class Person
    {
        public Person(int seed, Race race, Gender gender, Person mother, Person father, Time birth)
        {
            // for continuity's sake, the seed should be dependent on: mother's seed. Father's seed. # of child of this particular pairing
            Random = new Random(seed);
            Race = race;
            Gender = gender;
            Mother = mother;
            Father = father;
            Birth = birth;
        }

        public Random Random { get; private set; }
        public Alignment Alignment { get; private set; }

        public string Name { get; set; }
        public Race Race { get; private set; }
        public Gender Gender { get; private set; }
        public Person Mother { get; set; }
        public Person Father { get; set; }
        public Time Birth { get; private set; }
        public Time? Death { get; private set; }
    }
}
