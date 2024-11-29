using Repository;
using Repository.Model;
using Repository.Repositories;

namespace WebShop.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork
    {
        // Repository för produkter
        // Sparar förändringar (om du använder en databas)
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }

        

        void Save();
    }
}

