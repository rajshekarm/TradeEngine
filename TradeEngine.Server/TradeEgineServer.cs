using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEngine.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace TradeEngine.Server
{
    class ITradeEgineServer : BackgroundService, ITradeEngineServer
    {
        private readonly ILogger<ITradeEgineServer> _logger;
        private readonly TradeEngineServerConfiguration _config;
        private readonly IOrderQueue _orderQueue;  //

        public ITradeEgineServer(ILogger<ITradeEgineServer> logger, IOptions<TradeEngineServerConfiguration> config, IOrderQueue orderQueue) { 
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _orderQueue = orderQueue;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("✅ Trade Engine '{Name}' started.", _config.EngineName);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var order = _orderQueue.Dequeue();

                    if (order != null)
                    {
                        _logger.LogInformation("⚙️ Processing order: {Symbol} x{Qty}", order.Symbol, order.Quantity);
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
            }
            finally
            {
                _logger.LogInformation("🛑 Trade Engine stopped.");
            }
        }
        public Task Run(CancellationToken cancellationToken) => ExecuteAsync(cancellationToken);

        //we need to override to actually use the background service, we cannot call this when we want to stop the server
       
    }
}
