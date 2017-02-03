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

            var seed = new Random().Next();
            WorldConfiguration configuration = new WorldConfiguration(seed);
            configuration.Races.Add(new Race() {
                Name = "Human",
                Lifespan = new Distribution(75, 10),
                MinChildBearingAge = new Distribution(18, 2),
                MaxChildBearingAge = new Distribution(42, 3),
                FertilityRate = 0.2
            });
            
            World world = new World(configuration);

            WorldService.Initialize(world);

            var timer = new Stopwatch();
            timer.Start();
            WorldService.SimulateYears(world, 350);

            timer.Stop();

            lblCount.Text = string.Format("# Living: {0}, # dead: {1}", world.LivingPeople.Count, world.DeadPeople.Count);
            lblCount.Text = string.Format("{0} years in {1}", world.Date.Ticks, timer.Elapsed);

            lblCount.Visible = true;
        }
    }
}
