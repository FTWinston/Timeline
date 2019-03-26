using System;
using System.Diagnostics;
using Timeline.Data.Model;
using Timeline.Data.Services;
using Timeline.Simulation.Services;

namespace Timeline.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var worldService = CreateWorld();

            SimulateWorld(worldService, 400);
        }

        private static WorldService CreateWorld()
        {
            WorldConfiguration configuration = ConfigurationService.LoadFromFile("WorldConfiguration.xml");

            var randomService = new RandomService(configuration.Seed);

            var world = new World(configuration);

            var worldService = new WorldService(world, randomService);

            return worldService;
        }

        private static void SimulateWorld(WorldService worldService, int numYears)
        {
            var world = worldService.World;

            var timer = new Stopwatch();
            timer.Start();

            for (int year = 1; year <= numYears; year++)
            {
                if (year % 10 == 0)
                {
                    Console.WriteLine($"Year {year} ({world.LivingPeople.Count} living, {world.DeadPeople.Count} dead)");
                }

                worldService.SimulateYear();
            }

            timer.Stop();

            Console.WriteLine($"Completed {world.Date.Ticks} years in {timer.Elapsed}");
            Console.WriteLine($"# Living: {world.LivingPeople.Count}, # dead: {world.DeadPeople.Count}");
            Console.ReadKey();
        }
    }
}
