using PlantSimulation.BlazorApp.Data;
using PlantSimulation.BlazorApp.HostedServices;
using PlantSimulation.BlazorApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.Services
{
    public class PowerPlantService
    {
        private readonly SimulationContext _simulationContext;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        public PowerPlantService(SimulationContext simulationContext, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _simulationContext = simulationContext;
            _backgroundTaskQueue = backgroundTaskQueue;
        }
      
        public IEnumerable<PowerPlant> GetPowerPlants()
        {
            return _simulationContext.GetPowerPlants();
        }

        public bool AddPowerPlant(PowerPlant powerPlant)
        {
            var result = _simulationContext.AddPowerPlant(powerPlant);

            _backgroundTaskQueue.QueueBackgroundWorkItem(BackGroundTaskType.Simulation);

            return result;
        }
    }
}
