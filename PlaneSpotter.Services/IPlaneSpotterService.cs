using PlaneSpotter.Models;
using System.Collections.Generic;

namespace PlaneSpotter.Services
{
    public interface IPlaneSpotterService
    {
        IEnumerable<AircraftModel> GetAllSpottedAircraftInfo();

        IEnumerable<SpottingModel> GetAllSpottingInfo();

        void SavePlaneSpotInfo(SpottingModel model);

        void UpdatePlaneSpotInfo(SpottingModel model);

        void DeleteSelectedSpottedPlanes(long id);

        long SaveAircraftInfo(SpottingModel model);
    }
}
