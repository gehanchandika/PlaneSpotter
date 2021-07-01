using PlaneSpotter.Repo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Repo
{
    public interface IImageRepository<T> : IGenericRepository<T> where T : class
    {
        SpotImageEntity GetImageBySpotId(long spotId);
    }
}
