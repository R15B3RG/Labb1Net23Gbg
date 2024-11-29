using Repository;
using Repository.Data;
using Repository.Model;
using Repository.Repositories;
using WebShop.Notifications;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        private readonly ProductSubject _productSubject;

        public IProductRepository Products { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(MyDbContext context, ProductSubject productSubject)
        {
            _context = context;

            Products = new ProductRepository(_context);
            Customers = new CustomerRepository(_context);
            Orders = new OrderRepository(_context);

            _productSubject = productSubject;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        // Metod för att notifiera när en produkt har lagts till
        public void NotifyProductAdded(Product product)
        {
            // Här kan man lägga till logik för att hantera notifiering när en produkt har lagts till
            _productSubject.Notify(product);
        }
    }
}

