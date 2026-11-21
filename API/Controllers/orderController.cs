using API.Database;
using API.Models;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Pickup orders placed by buyers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenPolicy")]
    public class orderController : ControllerBase
    {
        // GET: api/order
        [HttpGet]
        public List<OrderForm> Get()
        {
            IGetAllOrders reader = new ReadOrderData();
            return reader.GetAllOrders();
        }

        // GET: api/order/5
        [HttpGet("{id}", Name = "GetOrderForms")]
        public OrderForm Get(int id)
        {
            IGetOrder reader = new ReadOrderData();
            return reader.GetOrder(id);
        }

        // POST: api/order
        [HttpPost]
        public void Post([FromBody] OrderForm value)
        {
            IPostOrder writer = new PostOrder();
            writer.PostOrder(value);
        }
    }
}
