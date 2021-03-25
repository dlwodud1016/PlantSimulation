using MathNet.Numerics.Distributions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulation.BlazorApp.Data;
using PlantSimulation.BlazorApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.HostedServices.Simulation
{

    public class QueuedHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<QueuedHostedService> _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
            ILogger<QueuedHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            TaskQueue = taskQueue;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Queued Hosted Service is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background queue.{Environment.NewLine}");

            
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem =
                    await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    if(workItem == BackGroundTaskType.Simulation)
                    {
                        ProcessSimulation();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }

        private double GetPowerGeneration(PowerPlant plant, Double normalValue)
        {
            double powerGeneration = plant.Capacity * normalValue;
            powerGeneration = (powerGeneration / plant.InclinationAngleEfficiency) * 100;
            powerGeneration = (powerGeneration / plant.DirectionAngleEfficiency) * 100;

            return powerGeneration;
        }
        private void ProcessSimulation()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var simulationContext = scope.ServiceProvider.GetService<SimulationContext>();

                // 현재날짜 부터 ~ +10년
                DateTime now = DateTime.Now;
                DateTime end = now.AddYears(5);
                var powerPlants = simulationContext.GetPowerPlants();

                // 월평균 일조량 정규분포
                var normal = new Normal(183.4058333, 29.38058967);

                simulationContext.initializeSimulationLineCharts();

                while (now.Year < end.Year)
                {
                    List<SimulationLineChart> simulationLineCharts = new List<SimulationLineChart>();

                    foreach (var plant in powerPlants)
                    {
                        // 발전량 = 용량 x 월평균 일조량(지역, 월) x 경사각에 의한 효율 x 방위각에 의한 발전효율
                        var powerGeneration = GetPowerGeneration(plant, normal.Sample());

                        SimulationLineChart simulationLineChart = new SimulationLineChart()
                        {
                            Date = now.ToString("yyyy/MM"),
                            Type = plant.Name,
                            Value = Math.Truncate(powerGeneration * 10) / 10
                        };

                        simulationLineCharts.Add(simulationLineChart);
                    }

                    simulationContext.AddRangeSimulationLineCharts(simulationLineCharts);

                    now = now.AddMonths(1);

                    Thread.Sleep(5000);
                }

                
            }
        }
    }

}
