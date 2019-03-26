using System;
using System.Diagnostics;
using Timeline.Data.Model;
using Timeline.Simulation.Services;

namespace Timeline.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = CreateWorld();
            SimulateWorld(world, 400);
        }

        private static World CreateWorld()
        {
            WorldConfiguration configuration = ConfigurationService.LoadFromFile("WorldConfiguration.xml");
            World world = new World(configuration);

            WorldService.Initialize(world);
            return world;
        }

        private static void SimulateWorld(World world, int numYears)
        {
            var timer = new Stopwatch();
            timer.Start();

            for (int year = 1; year <= numYears; year++)
            {
                if (world.Date.Ticks % 10 == 0)
                {
                    Console.WriteLine($"Year {world.Date.Ticks} ({world.LivingPeople.Count} living, {world.DeadPeople.Count} dead)");
                }

                WorldService.SimulateYear(world);
            }

            timer.Stop();

            Console.WriteLine($"Completed {world.Date.Ticks} years in {timer.Elapsed}");
            Console.WriteLine($"# Living: {world.LivingPeople.Count}, # dead: {world.DeadPeople.Count}");
            Console.ReadKey();
        }
    }
}
