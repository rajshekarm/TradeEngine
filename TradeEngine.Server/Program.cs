
using Microsoft.Extensions.Hosting;
using TradeEngine.Server;
using TradeEngine.Core;

using var engine = TradingEngineServerHostBuilder.BuildTradingEngineServer();
TradingEngineServerServiceProvider.ServiceProvider = engine.Services;

//using var scope = TradingEngineServerServiceProvider.ServiceProvider.CreateScope();
await engine.RunAsync();
