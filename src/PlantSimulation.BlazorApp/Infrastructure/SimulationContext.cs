using PlantSimulation.BlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Infrastructure
{
    public class SimulationContext
    {
        private List<PowerPlant> _powerPlants = new List<PowerPlant>();
        private List<SimulationLineChart> _simulationAvgLineCharts = new List<SimulationLineChart>();
        private List<SimulationLineChart> _simulationSumLineCharts = new List<SimulationLineChart>();
        private Efficiency _efficiency = new Efficiency();

        public SimulationContext()
        {
            _powerPlants.AddRange(new List<PowerPlant>()
            {
                new PowerPlant("테스트 발전기 1", 100),
                new PowerPlant("테스트 발전기 2", 120),
                new PowerPlant("테스트 발전기 3", 150),
                //new PowerPlant("테스트 발전기 4", 400),
                //new PowerPlant("테스트 발전기 5", 500)
            });

            //DateTime now = DateTime.Now;
            //Random rd = new Random();
            //for(int i = 0; i<5; i++)
            //{
            //    foreach (var plant in _powerPlants)
            //    {
            //        _simulationLineCharts.AddRange(new List<SimulationLineChart>()
            //        {
            //            new SimulationLineChart(now.ToString("yyyy/MM"), rd.Next(0, 100), plant.Name),
            //        });
            //    }
            //    now = now.AddMonths(1);
            //}

            
        }
        public IEnumerable<PowerPlant> GetPowerPlants()
        {
            return _powerPlants;
        }
    
        public bool AddPowerPlant(PowerPlant plant)
        {
            _powerPlants.Add(plant);

            return true;
        }

        public void initializeSimulationLineCharts()
        {
            _simulationAvgLineCharts.Clear();
            _simulationSumLineCharts.Clear();
        }
        public void AddRangeSimulationLineCharts(List<SimulationLineChart> simulationLineCharts)
        {
            _simulationAvgLineCharts.AddRange(simulationLineCharts);

            AddRangeSimulationSumLineCharts(simulationLineCharts);
        }

        private void AddRangeSimulationSumLineCharts(List<SimulationLineChart> simulationLineCharts)
        {
            foreach (var data in simulationLineCharts)
            {
                var last = _simulationSumLineCharts.Where(x=> x.Type == data.Type).LastOrDefault();
                double value = data.Value;

                if (last != null)
                {
                    value += last.Value;
                }

                _simulationSumLineCharts.Add(new SimulationLineChart(data.Date, value, data.Type));
            }
        }

        public IEnumerable<SimulationLineChart> GetSimulationAvgLineCharts()
        {
            return _simulationAvgLineCharts;
        }

        public IEnumerable<SimulationLineChart> GetSimulationSumLineCharts()
        {
            return _simulationSumLineCharts;
        }
    }
}
