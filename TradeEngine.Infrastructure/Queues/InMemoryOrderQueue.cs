using System.Collections.Concurrent;
using TradeEngine.Core.Interfaces;
using TradeEngine.Core.Models;

namespace TradeEngine.Infrastructure.Queues
{
    public class InMemoryOrderQueue : IOrderQueue
    {
        private readonly ConcurrentQueue<Order> _queue = new();

        public void Enqueue(Order order)
        {
            _queue.Enqueue(order);
        }

        public Order? Dequeue()
        {
            _queue.TryDequeue(out var order);
            return order;
        }
    }
}
