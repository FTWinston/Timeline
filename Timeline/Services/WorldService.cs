﻿using System;
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
                    world.LivingPeople.Add(new Person(world, world.Configuration.Random.Next(world.Configuration.Randomness.Length), world.Configuration.Random.Next(1, 1000), race, Gender.Female, null, null, world.Date));
                    world.LivingPeople.Add(new Person(world, world.Configuration.Random.Next(world.Configuration.Randomness.Length), world.Configuration.Random.Next(1, 1000), race, Gender.Male, null, null, world.Date));
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
            if (world.Date.Ticks % 5 == 0)
                Console.WriteLine("Year " + world.Date.Ticks);

            var eligibleBachelors = DetermineEligibleBachelors(world);

            world.LivingPeople.AsParallel().ForAll(p => PersonService.SimulateYear(p, eligibleBachelors));

            world.LivingPeople.RemoveAll(p => p.IsDead);
            world.LivingPeople.AddRange(world.NewPeople);
            world.NewPeople.Clear();

            world.Date += new GameTimeSpan() { Ticks = 1 };
        }

        private static Queue<Person> DetermineEligibleBachelors(World world)
        {
            var people = world.LivingPeople
                .Where(candidate => candidate.Gender == Gender.Male && PersonService.IsChildBearingAge(candidate));

            return new Queue<Person>(people);
        }
    }
}
