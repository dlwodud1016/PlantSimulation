using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Data
{
    public class Efficiency
    {
        // 경사각
        public Dictionary<int, double> InclinationAngleDict { get; set; } = new Dictionary<int, double>();

        // 방위각
        public Dictionary<int, double> DirectionAngleDict { get; set; } = new Dictionary<int, double>();

        public Efficiency()
        {
            InclinationAngleDict.Add(0, 88.8);
            InclinationAngleDict.Add(15, 97.1);
            InclinationAngleDict.Add(30, 100);
            InclinationAngleDict.Add(45, 97.6);
            InclinationAngleDict.Add(60, 89.8);
            InclinationAngleDict.Add(75, 77.3);
            InclinationAngleDict.Add(90, 50.7);

            DirectionAngleDict.Add(0, 100);
            DirectionAngleDict.Add(15, 98.9);
            DirectionAngleDict.Add(30, 96.5);
            DirectionAngleDict.Add(45, 93.2);
            DirectionAngleDict.Add(60, 89.1);
            DirectionAngleDict.Add(75, 84.5);
            DirectionAngleDict.Add(90, 79.7);
        }

    }
}
