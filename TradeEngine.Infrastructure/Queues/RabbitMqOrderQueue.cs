using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TradeEngine.Core.Interfaces;
using TradeEngine.Core.Models;

namespace TradeEngine.Infrastructure.Queues
{
    public class RabbitMqOrderQueue : IOrderQueue, IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private const string QueueName = "trade_orders";

        private RabbitMqOrderQueue(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public static async Task<RabbitMqOrderQueue> CreateAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            var connection = await factory.CreateConnectionAsync("TradeEngineConnection");
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return new RabbitMqOrderQueue(connection, channel);
        }

        public async Task EnqueueAsync(Order order, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(order); 
            var body = Encoding.UTF8.GetBytes(json);
            var props = new BasicProperties { Persistent = true };
            await _channel.BasicPublishAsync<BasicProperties>(exchange: "", routingKey: QueueName, mandatory: false, basicProperties: null, body: body);
        }

        public async Task<Order?> DequeueAsync(CancellationToken cancellationToken = default)
        {
            var result = await _channel.BasicGetAsync(QueueName, autoAck: true, cancellationToken);
            if (result is null)
                return null;

            var json = Encoding.UTF8.GetString(result.Body.ToArray());
            return JsonSerializer.Deserialize<Order>(json);
        }

        public async ValueTask DisposeAsync()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}
