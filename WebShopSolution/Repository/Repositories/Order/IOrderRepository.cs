using Repository.Model;
using Repository.Repositories;

namespace Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        bool UpdateOrderStatus(Order order);

        IEnumerable<Order> GetSubOrders(int parentOrderId);

        Order GetOrderWithDetails(int orderId);
    }
}
