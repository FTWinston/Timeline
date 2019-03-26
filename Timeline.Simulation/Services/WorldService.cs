using System;
using System.Collections.Generic;
using System.Linq;
using Timeline.Data.Model;

namespace Timeline.Simulation.Services
{
    public static class WorldService
    {
        public static void Initialize(World world)
        {
            world.LivingPeople.Clear();
            world.DeadPeople.Clear();
        }

        public static void SimulateYears(World world, int numYears)
        {
            for (int i = 0; i < numYears; i++)
                SimulateYear(world);
        }

        public static void SimulateYear(World world)
        {
            TriggerEvents(world);

            var eligibleBachelors = DetermineEligibleBachelors(world);

            world.LivingPeople.ForEach(p => PersonService.SimulateYear(p, eligibleBachelors));

            world.LivingPeople.RemoveAll(p => p.IsDead);
            world.LivingPeople.AddRange(world.NewPeople);
            world.NewPeople.Clear();

            world.Date += new GameTimeSpan() { Ticks = 1 };
        }

        private static void TriggerEvents(World world)
        {
            var events = world.Configuration.Events;

            while (true)
            {
                if (world.NextEventIndex >= events.Count)
                    break;

                var nextEvent = events[world.NextEventIndex];
                if (nextEvent.OccursAt > world.Date)
                    break;

                world.NextEventIndex++;
                Console.WriteLine("Triggering event #" + world.NextEventIndex);
                nextEvent.Perform(world);
            }
        }

        private static Queue<Person> DetermineEligibleBachelors(World world)
        {
            var people = world.LivingPeople
                .Where(candidate => candidate.Gender == Gender.Male && PersonService.IsChildBearingAge(candidate));

            return new Queue<Person>(people);
        }
    }
}
