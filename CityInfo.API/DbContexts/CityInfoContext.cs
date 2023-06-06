using CityInfo.API.entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
            // Database.Migrate();
        }

        // public override void OnConfiguring(dbcontextoptionsbuilder optionsbuilder)
        // {
        //     optionsbuilder.useSqlite("connectionstring");
        //     base.OnConfiguring(optionsbuilder);
        // }
    }
}
