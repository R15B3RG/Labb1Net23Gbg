using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Model;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OrderController(MyDbContext context)
        {

        _context = context; 
        
        }

        [HttpGet]

        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems) 
                .ThenInclude(oi => oi.Product) 
                .ToList();
            return Ok(orders);
        }

        [HttpGet("{id}")]

        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public ActionResult AddOrder([FromBody] Order order)
        {
            if (order == null) return BadRequest("Order data can't be null.");

            // Kontrollera att kunden existerar
            var customer = _context.Customers.FirstOrDefault(c => c.Id == order.CustomerId);
            if (customer == null) return BadRequest("Customer does not exist.");

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            order.OrderDate = updatedOrder.OrderDate;
            order.CustomerId = updatedOrder.CustomerId;

            order.OrderItems = updatedOrder.OrderItems;

            _context.Orders.Update(order);
            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok(order);
        }
    }
}
