using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public interface IUnitofWork : IDisposable
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        IPlaneSpotterRepository<T> PlaneSpotterRepository<T>() where T : class;
        IAircraftRepository<T> AircraftRepository<T>() where T : class;
        void SaveChanges();
    }
}
