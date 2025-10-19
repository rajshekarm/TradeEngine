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
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> SubmitOrder([FromBody] Order order)
        {
            await _orderQueue.EnqueueAsync(order);
            return Accepted(new { message = $"✅ Order {order.Symbol} submitted successfully." });
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
