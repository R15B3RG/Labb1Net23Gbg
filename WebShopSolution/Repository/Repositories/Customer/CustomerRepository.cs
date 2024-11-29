using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Model;
using Repository.Repositories;


namespace Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context)
        {
        }

        public bool UpdateCustomerInfo(Customer customer)
        {
            var existingCustomer = _dbSet.Find(customer.Id);
            if (existingCustomer == null)
            {
                return false; // Kunden existerar inte
            }

            // Uppdatera egenskaper
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.Orders = customer.Orders;

            // Fler fält om det behövs

            _context.SaveChanges();
            return true;
        }
    }
}
