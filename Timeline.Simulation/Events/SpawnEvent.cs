using System;
using Timeline.Data.Events;
using Timeline.Data.Model;
using Timeline.Simulation.Services;

namespace Timeline.Simulation.Events
{
    public class SpawnEvent : Event
    {
        public SpawnEvent(Race race, int number, GameTime occursAt, MapArea location)
            : base(occursAt, location)
        {
            Race = race;
            Number = number;
        }

        public Race Race { get; private set; }
        public int Number { get; private set; }

        public override void Perform(World world)
        {
            bool isFemale = true;
            for (int i = 0; i < Number; i++)
            {
                var randomStartPos = RandomService.GetNextInt(world, 0, world.Configuration.Randomness.Length);
                var randomIncrement = RandomService.GetNextInt(world, 1, 1000);

                var person = new Person(world, randomStartPos, randomIncrement, Race, isFemale ? Gender.Female : Gender.Male, null, null, world.Date);
                person.Location = Location;
                world.LivingPeople.Add(person);
                isFemale = !isFemale;
            }
        }

        public override void Reverse(World world)
        {
            throw new NotImplementedException();
        }
    }
}
