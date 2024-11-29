using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        // Consume db Context
        protected readonly MyDbContext _context;
        // use dbSet
        protected readonly DbSet<T> _dbSet;
        public Repository(MyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
    }
}
