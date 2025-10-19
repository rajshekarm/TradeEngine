using Microsoft.AspNetCore.Mvc;
using TradeEngine.Core.Interfaces;
using TradeEngine.Core.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TradeEngine.APIHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderQueue _orderQueue;

        public OrderController(IOrderQueue orderQueue)
        {
            _orderQueue = orderQueue;
            Console.WriteLine(_orderQueue);
        }
      
        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> SubmitOrder([FromBody] Order order)
        {
            Console.WriteLine("request reached");
            Console.WriteLine($"✅ Order {order.Symbol} submitted successfully." );

            await _orderQueue.EnqueueAsync(order);
            return Accepted(new { message = $"✅ Order {order.Symbol} submitted successfully." });
        }

    }
}
