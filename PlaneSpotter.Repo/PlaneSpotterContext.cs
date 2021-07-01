using PlaneSpotter.Repo.Entities;
using PlaneSpotter.Repo.Initializer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.Repo
{
    public class PlaneSpotterContext : DbContext
    { 
        public PlaneSpotterContext() : base("name=PlaneSpotterContext")
        {
            
        }

        public DbSet<AircraftEntity> AircraftInfos { get; set; }
        public DbSet<SpotEntity> SpotInfos { get; set; }
        public DbSet<SpotImageEntity> SpotImageInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<SpotEntity>().Property(x => x.SpotId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            
        }

    }
}
