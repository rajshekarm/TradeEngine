using System;

namespace TradeEngine.Server
{
    public class TradeEngineServerConfiguration
    {
        /// <summary>
        /// A friendly name for this engine instance (shown in logs)
        /// </summary>
        public string EngineName { get; set; } = "DefaultEngine";

        /// <summary>
        /// How frequently the engine checks for new orders (milliseconds)
        /// </summary>
        public int RefreshIntervalMs { get; set; } = 2000;

        /// <summary>
        /// Future config example: maximum batch size, connection strings, etc.
        /// </summary>
        public int MaxBatchSize { get; set; } = 100;
    }
}
