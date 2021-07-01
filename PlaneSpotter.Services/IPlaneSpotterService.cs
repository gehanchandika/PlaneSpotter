using PlaneSpotter.Models;
using System.Collections.Generic;

namespace PlaneSpotter.Services
{
    public interface IPlaneSpotterService
    {
        IEnumerable<AircraftModel> GetAllSpottedAircraftInfo();

        IEnumerable<SpotInfoModel> GetAllSpottingInfo();

        void SavePlaneSpotInfo(SpotInfoRichModel model);

        void UpdatePlaneSpotInfo(SpotInfoRichModel model);

        void DeleteSelectedSpottedPlanes(long id);

        long SaveAircraftInfo(SpotInfoRichModel model);

        SpotInfoImageModel GetSpotInfoImage(long id);

        long SaveSpottedAircraftImage(SpotInfoImageModel model);
    }
}
