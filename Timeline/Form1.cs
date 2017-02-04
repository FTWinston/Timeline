using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timeline.Model;
using Timeline.Services;

namespace Timeline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            World world = CreateWorld();
            SimulateWorld(world);
        }

        private World CreateWorld()
        {
            Random random = new Random();

            var seed = random.Next(int.MinValue, int.MaxValue);
            var map = new Map(100, 100);
            WorldConfiguration configuration = new WorldConfiguration(seed, map);
            configuration.Races.Add(new Race()
            {
                Name = "Human",
                Lifespan = new Distribution(75, 10),
                MinChildBearingAge = new Distribution(18, 2),
                MaxChildBearingAge = new Distribution(42, 3),
                FertilityChance = 5
            });

            World world = new World(configuration);

            WorldService.Initialize(world);
            return world;
        }

        private void SimulateWorld(World world)
        {
            var timer = new Stopwatch();
            timer.Start();
            WorldService.SimulateYears(world, 400);

            timer.Stop();

            lblCount.Text = string.Format("# Living: {0}, # dead: {1}", world.LivingPeople.Count, world.DeadPeople.Count);
            lblTime.Text = string.Format("{0} years in {1}", world.Date.Ticks, timer.Elapsed);

            lblCount.Visible = lblTime.Visible = true;
        }
    }
}
