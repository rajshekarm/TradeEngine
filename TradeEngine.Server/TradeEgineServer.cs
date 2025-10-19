using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEngine.Core.Interfaces;
using TradeEngine.Core.Models;

namespace TradeEngine.Server
{
    class TradeEgineServer : BackgroundService, ITradeEngineServer
    {
        private readonly ILogger<ITradeEngineServer> _logger;
        private readonly TradeEngineServerConfiguration _config;
        private readonly IOrderQueue _orderQueue;  //

        public TradeEgineServer(ILogger<ITradeEngineServer> logger, IOptions<TradeEngineServerConfiguration> config, IOrderQueue orderQueue) {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
            _orderQueue = orderQueue ?? throw new ArgumentNullException(nameof(orderQueue));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("-> Trade Engine '{Name}' started.", _config.EngineName);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var order = await _orderQueue.DequeueAsync();

                    if (order != null)
                    {
                        _logger.LogInformation("-> Processing order: {Symbol} x{Qty}", order.Symbol, order.Quantity);
                        await ProcessOrderAsync(order, stoppingToken);
                    }
                    else
                    {
                        _logger.LogInformation("⏳ No orders in queue. Waiting...");
                        await Task.Delay(_config.RefreshIntervalMs, stoppingToken);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // expected on shutdown, ignore
                _logger.LogInformation("🧩 Trade Engine shutdown requested.");
            }
            finally
            {
                _logger.LogInformation(":Trade Engine stopped.");
            }
        }


        private Task ProcessOrderAsync(Order order, CancellationToken token)
        {
            // Example of processing logic (this is where core logic goes)
            _logger.LogInformation("💰 Executed trade for {Symbol} at {Price}", order.Symbol, order.Price);
            return Task.CompletedTask;
        }
        public Task Run(CancellationToken cancellationToken) => ExecuteAsync(cancellationToken);

        //we need to override to actually use the background service, we cannot call this when we want to stop the server
       
    }
}
