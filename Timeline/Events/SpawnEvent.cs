using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;
using Timeline.Services;

namespace Timeline.Events
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

                world.LivingPeople.Add(new Person(world, randomStartPos, randomIncrement, Race, isFemale ? Gender.Female : Gender.Male, null, null, world.Date));
                isFemale = !isFemale;
            }
        }

        public override void Reverse(World world)
        {
            throw new NotImplementedException();
        }
    }
}
