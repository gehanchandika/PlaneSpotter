using PlaneSpotter.Repo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo.Initializer
{
    public class PlaneSpotterInitializer : DropCreateDatabaseIfModelChanges<PlaneSpotterContext>
    {
        protected override void Seed(PlaneSpotterContext context)
        {
            var airCrafts = new List<AircraftEntity>
            {
                new AircraftEntity { AircraftId=1, AircraftMake = "Boeing", AircraftModel="737 800", AircraftRegistration="ZK-ZQG" },
                new AircraftEntity { AircraftId=2, AircraftMake = "Airbus", AircraftModel="A340-311", AircraftRegistration="4R-ADA" }
            };
            airCrafts.ForEach(s => context.AircraftInfos.Add(s));
            context.SaveChanges();

            var airCraftSpots = new List<SpotEntity>
            {
                new SpotEntity { SpotId=1, Location="Colombo", SpottedDateTime=DateTime.Now, AircraftId = 1, IsDeleted=false  },
                new SpotEntity { SpotId=2, Location="Narita", SpottedDateTime=DateTime.Now, AircraftId = 2, IsDeleted=false  }
            };
            airCraftSpots.ForEach(x => context.SpotInfos.Add(x));
            context.SaveChanges();
        }
    }
}
