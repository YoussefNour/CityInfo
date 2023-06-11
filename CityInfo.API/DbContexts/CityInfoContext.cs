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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<City>()
                .HasData(
                    new City("Cairo")
                    {
                        Id = 1,
                        Name = "Cairo",
                        Description = "Capital of Egypt"
                    },
                    new City("Alexandria")
                    {
                        Id = 2,
                        Description = "largest shore country of Egypt",
                    },
                    new City("Aswan") { Id = 3, Description = "South most city of Egypt" }
                );

            modelBuilder
                .Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Pyramids")
                    {
                        Id = 1,
                        Description = "great pyramids there are 3 of them",
                        CityId = 1
                    },
                    new PointOfInterest("Hawawshy Al Rabi3")
                    {
                        Id = 2,
                        Description = "Sells best hawawshy and kebda",
                        CityId = 1
                    },
                    new PointOfInterest("Library of Alexandria")
                    {
                        Id = 3,
                        Description = "Library of Alexandria",
                        CityId = 2
                    },
                    new PointOfInterest("Kebdet El Falah")
                    {
                        Id = 4,
                        Description = "Sells best kebda",
                        CityId = 2
                    }
                );
            base.OnModelCreating(modelBuilder);
        }

        // public override void OnConfiguring(dbcontextoptionsbuilder optionsbuilder)
        // {
        //     optionsbuilder.useSqlite("connectionstring");
        //     base.OnConfiguring(optionsbuilder);
        // }
    }
}
