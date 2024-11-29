using Repository.Model;
using Repository.Repositories;

namespace Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        bool UpdateCustomerInfo(Customer customer);
    }
}
