using CityInfo.API.models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Cairo",
                    Description = "Capital of Egypt",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Pyramids",
                            Description = "great pyramids there are 3 of them"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Hawawshy Al Rabi3",
                            Description = "Sells best hawawshy and kebda"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Alexandria",
                    Description = "largest shore country of Egypt",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Library of Alexandria",
                            Description = "Library of Alexandria"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Kebdet El Falah",
                            Description = "Sells best kebda"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Aswan",
                    Description = "South most city of Egypt"
                }
            };
        }
    }
}
