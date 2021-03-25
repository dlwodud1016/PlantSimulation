using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Data
{
    public class PowerPlant
    {
        public String Uid { get; set; } = Guid.NewGuid().ToString();

        public String Name { get; set; }

        public double Capacity { get; set; }

        public double InclinationAngleEfficiency { get; set; }

        public double DirectionAngleEfficiency { get; set; }

        public PowerPlant()
        {

        }
        public PowerPlant(String name, UInt64 capacity, double inclinationAngleEfficiency = 100, double directionAngleEfficiency = 100)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.InclinationAngleEfficiency = inclinationAngleEfficiency;
            this.DirectionAngleEfficiency = directionAngleEfficiency;
        }
    }
}
