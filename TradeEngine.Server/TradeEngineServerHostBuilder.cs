using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEngine.Core.Interfaces;
using TradeEngine.Infrastructure.Extensions;

namespace TradeEngine.Server
{
    public sealed class TradingEngineServerHostBuilder
    {
        public static IHost BuildTradingEngineServer()
            => Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //Bind configuration section
                    services.AddOptions();
                    services.Configure<TradeEngineServerConfiguration>(
                        context.Configuration.GetSection(nameof(TradeEngineServerConfiguration)));

                    //Register Infrastructure (queues, etc.)
                    services.AddInfrastructure();

                    ///Register  trading engine 
                    services.AddSingleton<ITradeEgineServer, ITradeEgineServer>();


                    // Run TradeEngine Server as hosted service
                    services.AddHostedService<ITradeEgineServer>();


                })
                .Build();
    }

}
