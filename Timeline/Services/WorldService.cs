using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Model;

namespace Timeline.Services
{
    static class WorldService
    {
        public static void Initialize(World world)
        {
            world.Random = new Random(world.Configuration.Seed);
            world.LivingPeople.Clear();
            world.DeadPeople.Clear();

            CreateInitialPopulation(world, 5);
        }

        private static void CreateInitialPopulation(World world, int numPerGender)
        {
            foreach (var race in world.Configuration.Races)
            {
                for (int i = 0; i < numPerGender; i++)
                {
                    world.LivingPeople.Add(new Person(world, world.Random.Next(), race, Gender.Female, null, null, world.Date));
                    world.LivingPeople.Add(new Person(world, world.Random.Next(), race, Gender.Male, null, null, world.Date));
                }
            }
        }

        public static void SimulateYears(World world, int numYears)
        {
            for (int i = 0; i < numYears; i++)
                SimulateYear(world);
        }

        public static void SimulateYear(World world)
        {
            foreach (var person in world.LivingPeople)
                PersonService.SimulateYear(person);

            var newlyDead = world.LivingPeople.Where(p => p.IsDead);
            world.DeadPeople.AddRange(newlyDead);
            world.LivingPeople.RemoveAll(p => p.IsDead);

            world.LivingPeople.AddRange(world.NewPeople);
            world.NewPeople.Clear();

            world.Date += new GameTimeSpan() { Ticks = 1 };
        }
    }
}
