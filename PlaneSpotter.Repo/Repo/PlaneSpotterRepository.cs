using PlaneSpotter.Repo.Repo;
using System.Data.Entity;

namespace PlaneSpotter.Repo
{
    public class PlaneSpotterRepository<T> : GenericRepository<T>, IPlaneSpotterRepository<T> where T : class
    {
        private readonly PlaneSpotterContext context;
        private readonly DbSet<T> entities;

        public PlaneSpotterRepository(PlaneSpotterContext context) : base(context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

    }
}
