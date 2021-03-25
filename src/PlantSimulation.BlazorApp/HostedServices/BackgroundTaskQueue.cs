using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulation.BlazorApp.HostedServices
{
    public enum BackGroundTaskType
    {
        None, Simulation
    }

    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(BackGroundTaskType workItem);

        Task<BackGroundTaskType> DequeueAsync(
            CancellationToken cancellationToken);
    }
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<BackGroundTaskType> _workItems =
       new ConcurrentQueue<BackGroundTaskType>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(
            BackGroundTaskType workItem)
        {
            //if (workItem == BackGroundTaskType.None)
            //{
            //    throw new ArgumentNullException(nameof(workItem));
            //}

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<BackGroundTaskType> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
