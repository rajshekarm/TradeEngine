using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEngine.Core.Interfaces
{
    public interface ITradeEngineServer
    {
        Task Run(CancellationToken cancellationToken);
    }
}
