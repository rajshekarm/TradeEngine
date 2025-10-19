using Microsoft.Extensions.DependencyInjection;
using TradeEngine.Core.Interfaces;
using TradeEngine.Infrastructure.Queues;

namespace TradeEngine.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register the in-memory queue as a singleton
            // services.AddSingleton<IOrderQueue, InMemoryOrderQueue>();


            /*Registering RabbitMQ AS messaging Queue*/
            //var queue = RabbitMqOrderQueue.CreateAsync().Result;
            //services.AddSingleton<IOrderQueue>(queue);
            services.AddSingleton<IOrderQueue>(sp => RabbitMqOrderQueue.CreateAsync().GetAwaiter().GetResult());

            







            // Later, you can also register:
            // services.AddLogging();
            // services.AddDatabase();
            // services.AddMessageBroker();

            return services;
        }
    }
}
