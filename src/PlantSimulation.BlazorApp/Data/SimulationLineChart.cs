using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Data
{
    public class SimulationLineChart
    {
        public String Date { get; set; }

        public Double Value { get; set; }

        public String Type { get; set; }

        public SimulationLineChart() { }

        public SimulationLineChart(String date, Double value, String type)
        {
            this.Date = date;
            this.Value = value;
            this.Type = type;
        }
    }
}
