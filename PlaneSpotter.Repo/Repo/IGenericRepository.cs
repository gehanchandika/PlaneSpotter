using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Delete(long id);
        void Update(T entity);
    }
}
