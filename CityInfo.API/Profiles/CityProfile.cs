using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<entities.City, models.CityWithoutPointOfInterestDto>();
            CreateMap<entities.City, models.CityDto>();
        }
    }
}
