using Repository.Data;
using Repository.Model;
using Repository.Repositories;

namespace Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }

        public bool UpdateProductStock(Product product, int quantity)
        {
            if (product == null) return false;

            product.Stock += quantity; // Uppdatera lagersaldo
            _dbSet.Update(product);
            _context.SaveChanges();
            return true;
        }
    }
}
