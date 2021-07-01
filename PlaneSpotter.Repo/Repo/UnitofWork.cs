using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public class UnitofWork : IUnitofWork, IDisposable
    {
        private readonly PlaneSpotterContext _context = new PlaneSpotterContext();
        private bool _disposed = true;
        private Hashtable _repositories;

        public UnitofWork()
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
        }

        public IAircraftRepository<T> AircraftRepository<T>() where T : class
        {
            return CreateRepositoryInstance<AircraftRepository<T>, IAircraftRepository<T>>();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return CreateRepositoryInstance<GenericRepository<T>, IGenericRepository<T>>();
        }

        public IPlaneSpotterRepository<T> PlaneSpotterRepository<T>() where T : class
        {
            return CreateRepositoryInstance<PlaneSpotterRepository<T>, IPlaneSpotterRepository<T>>();
        }

        public IImageRepository<T> ImageRepository<T>() where T : class
        {
            return CreateRepositoryInstance<ImageRepository<T>, IImageRepository<T>>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        private U CreateRepositoryInstance<T, U>()
        {
            var model = typeof(T).GenericTypeArguments.FirstOrDefault();
            if (_repositories.ContainsKey(model.Name))
            {
                return (U)_repositories[model.Name];
            }
            var repositoryType = typeof(T).GetGenericTypeDefinition();
            _repositories.Add(model.Name, Activator.CreateInstance(repositoryType.MakeGenericType(model), _context));
            return (U)_repositories[model.Name];
        }
    }
}
