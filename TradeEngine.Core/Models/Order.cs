using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEngine.Core.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Symbol { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Side { get; set; } = "Buy"; // or "Sell"

        public int Price { get; set; }
    }
}

