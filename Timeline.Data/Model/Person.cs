using System.Collections.Generic;

namespace Timeline.Data.Model
{
    public class Person : Randomizable
    {
        public Person(int randomStart, int randomIncrement, Race race, Gender gender, Person mother, Person father, GameTime birth)
            : base(randomStart, randomIncrement)
        {
            Race = race;
            Gender = gender;
            Mother = mother;
            Father = father;
            Birth = birth;

            Children = new List<Person>();
        }

        public MapArea Location { get; set; }
        public Alignment Alignment { get; private set; }
        public string Name { get; set; }
        public Race Race { get; private set; }
        public Gender Gender { get; private set; }
        public Person Mother { get; set; }
        public Person Father { get; set; }
        public GameTime Birth { get; private set; }
        public GameTime? Death { get; set; }
        public GameTimeSpan GetAgeAt(GameTime date) => date - Birth;
        public bool IsDead { get { return Death.HasValue; } }

        public List<Person> Children { get; private set; }
    }
}
