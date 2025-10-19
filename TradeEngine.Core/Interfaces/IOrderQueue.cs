using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEngine.Core.Models;

namespace TradeEngine.Core.Interfaces
{
    public interface IOrderQueue
    {
        void Enqueue(Order order);
        Order? Dequeue();
    }
}
