using System;
using Timeline.Data.Events;
using Timeline.Data.Model;
using Timeline.Data.Services;

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

        public override void Perform(World world, RandomService randomService)
        {
            bool isFemale = true;
            for (int i = 0; i < Number; i++)
            {
                var randomStartPos = randomService.GetNextInt(world, 0, randomService.Length);
                var randomIncrement = randomService.GetNextInt(world, 1, 1000);

                var person = new Person(randomStartPos, randomIncrement, Race, isFemale ? Gender.Female : Gender.Male, null, null, world.Date);

                PersonService.SetMilestoneDates(world.Date, person);

                person.Location = Location;
                world.LivingPeople.Add(person);
                isFemale = !isFemale;
            }
        }
    }
}
