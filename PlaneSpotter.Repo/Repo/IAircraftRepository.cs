using PlaneSpotter.Repo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public interface IAircraftRepository<T> : IGenericRepository<T> where T : class
    {
        AircraftEntity GetByRegistration(string regNo);
    }
}
