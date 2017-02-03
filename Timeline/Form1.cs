using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            
            WorldConfiguration configuration = new WorldConfiguration(new Random().Next());
            configuration.Races.Add(new Race() {
                Name = "Human",
                Lifespan = new Distribution(75, 10),
                MinChildBearingAge = new Distribution(18, 2),
                MaxChildBearingAge = new Distribution(42, 3),
                FertilityRate = 0.2
            });
            
            World world = new World(configuration);

            WorldService.Initialize(world);
            WorldService.SimulateYears(world, 200);
        }
    }
}
