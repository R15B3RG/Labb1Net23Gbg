using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Repository.Model;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CustomerController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }


        [HttpGet("{id}")]

        public ActionResult<Customer> GetCustomer(int id)
        {
            var getCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (getCustomer == null) return NotFound();

            return Ok(getCustomer);
        }

        [HttpPost]

        public ActionResult AddCustomer(Customer customer)
        {
            if (customer == null) return BadRequest("Customer data can't be null");

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new {id = customer.Id}, customer);
        }

        [HttpPut]

        public ActionResult UpdateCustomer(Customer customer, int id)
        {
            var updateCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (updateCustomer == null) return NotFound();

            updateCustomer.Name = customer.Name;
            updateCustomer.Email = customer.Email;
            updateCustomer.Address = customer.Address;
            updateCustomer.Id = id;
            updateCustomer.Orders = customer.Orders;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]

        public ActionResult DeleteCustomer(int id)
        {
            var deleteCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (deleteCustomer == null) return NotFound();

            _context.Customers.Remove(deleteCustomer);
            _context.SaveChanges();

            return NoContent();
        }
        
    }
}
