using PlaneSpotter.Repo.Entities;
using System.Data.Entity;
using System.Linq;

namespace PlaneSpotter.Repo.Repo
{
    public class ImageRepository<T> : GenericRepository<T>, IImageRepository<T> where T : class
    {
        private readonly PlaneSpotterContext context;
        private readonly DbSet<T> entities;

        public ImageRepository(PlaneSpotterContext context) : base(context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public SpotImageEntity GetImageBySpotId(long spotId)
        {
            return context.SpotImageInfos.FirstOrDefault(x => x.SpotId == spotId);
        }
    }
}
