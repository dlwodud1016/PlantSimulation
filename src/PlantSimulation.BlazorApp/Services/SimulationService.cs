using PlantSimulation.BlazorApp.Data;
using PlantSimulation.BlazorApp.HostedServices;
using PlantSimulation.BlazorApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Services
{
    public class SimulationService
    {
        public enum SimulationType
        {
            Avg, Sum
        }
        private readonly SimulationContext _simulationContext;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        public SimulationService(SimulationContext simulationContext, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _simulationContext = simulationContext;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        public void StartSimulation()
        {
            _backgroundTaskQueue.QueueBackgroundWorkItem(BackGroundTaskType.Simulation);
        }

        public IEnumerable<SimulationLineChart> GetSimulationLineCharts(SimulationType type)
        {
            if(type == SimulationType.Avg)
            {
                return _simulationContext.GetSimulationAvgLineCharts();
            }
            else
            {
                return _simulationContext.GetSimulationSumLineCharts();
            }
            
        }
    }
}
