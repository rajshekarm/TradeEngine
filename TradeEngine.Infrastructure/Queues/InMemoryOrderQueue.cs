using System.Collections.Concurrent;
using TradeEngine.Core.Interfaces;
using TradeEngine.Core.Models;

namespace TradeEngine.Infrastructure.Queues
{
    public class InMemoryOrderQueue : IOrderQueue
    {
        private readonly ConcurrentQueue<Order> _queue = new();

        public Task EnqueueAsync(Order order, CancellationToken cancellationToken = default)
        {
            _queue.Enqueue(order);
            return Task.CompletedTask;
        }

        public Task<Order?> DequeueAsync(CancellationToken cancellationToken = default)
        {
            _queue.TryDequeue(out var order);
            return Task.FromResult(order);
        }
    }
}
