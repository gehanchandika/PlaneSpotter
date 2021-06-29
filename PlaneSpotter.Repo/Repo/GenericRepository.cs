using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly PlaneSpotterContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(PlaneSpotterContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public T Get(long id)
        {
            T entity = _dbSet.Find(id);
            return entity;
        }

        public void Delete(long id)
        {
            T entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }
    }
}
