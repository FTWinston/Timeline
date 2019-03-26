using System;
using System.Collections.Generic;
using System.Linq;
using Timeline.Data.Model;
using Timeline.Data.Services;

namespace Timeline.Simulation.Services
{
    public class WorldService
    {
        public World World { get; }
        RandomService RandomService { get; }
        PersonService PersonService { get; }
        BreedingService BreedingService { get; }

        public WorldService(World world, RandomService randomService)
        {
            RandomService = randomService;
            BreedingService = new BreedingService(RandomService);
            PersonService = new PersonService(RandomService, BreedingService);

            World = world;
            world.LivingPeople.Clear();
            world.DeadPeople.Clear();
        }

        public void SimulateYears(int numYears)
        {
            for (int i = 0; i < numYears; i++)
                SimulateYear();
        }

        public void SimulateYear()
        {
            TriggerEvents();

            var eligibleBachelors = DetermineEligibleBachelors(World.Date);

            World.LivingPeople.ForEach(person => PersonService.SimulateYear(World, person, eligibleBachelors));

            World.LivingPeople.RemoveAll(p => p.IsDead);
            World.LivingPeople.AddRange(World.NewPeople);
            World.NewPeople.Clear();

            World.Date += new GameTimeSpan(1);
        }

        private void TriggerEvents()
        {
            var events = World.Configuration.Events;

            while (true)
            {
                if (World.NextEventIndex >= events.Count)
                    break;

                var nextEvent = events[World.NextEventIndex];
                if (nextEvent.OccursAt > World.Date)
                    break;

                World.NextEventIndex++;
                Console.WriteLine($"Triggering event #{World.NextEventIndex}");
                nextEvent.Perform(World, RandomService);
            }
        }

        private Queue<Person> DetermineEligibleBachelors(GameTime date)
        {
            var people = World.LivingPeople
                .Where(candidate => candidate.Gender == Gender.Male && PersonService.IsChildBearingAge(date, candidate));

            return new Queue<Person>(people);
        }
    }
}
