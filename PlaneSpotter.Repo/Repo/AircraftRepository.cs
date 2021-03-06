using PlaneSpotter.Repo.Entities;
using System.Data.Entity;
using System.Linq;

namespace PlaneSpotter.Repo.Repo
{
    public class AircraftRepository<T> : GenericRepository<T>, IAircraftRepository<T> where T : class
    {
        private readonly PlaneSpotterContext context;
        private readonly DbSet<T> entities;

        public AircraftRepository(PlaneSpotterContext context) : base(context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public AircraftEntity GetByRegistration(string regNo)
        {
            return context.AircraftInfos.FirstOrDefault(x => x.AircraftRegistration == regNo);
        }
    }
}
