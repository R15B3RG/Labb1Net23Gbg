using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Model;
using Repository.Repositories;

namespace Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MyDbContext context) : base(context)
        {
        }

        public bool UpdateOrderStatus(Order order)
        {
            if (order == null) return false;

            // Uppdatera orderstatus
            _dbSet.Update(order); // Markerar ordern som modifierad
            _context.SaveChanges(); // Sparar ändringarna i databasen
            return true;
        }

        public IEnumerable<Order> GetSubOrders(int parentOrderId)
        {
            return _dbSet.Where(o => o.ParentOrderId == parentOrderId).ToList();
        }

        public Order GetOrderWithDetails(int orderId)
        {
            return _dbSet
                .Include(o => o.SubOrders)     
                .Include(o => o.OrderItems)    
                .ThenInclude(oi => oi.Product) 
                .FirstOrDefault(o => o.OrderId == orderId) ?? new Order();

        }
    }

}
