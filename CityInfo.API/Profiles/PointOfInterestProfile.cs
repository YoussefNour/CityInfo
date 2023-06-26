using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<entities.PointOfInterest, models.PointOfInterestDto>();
            CreateMap<models.PointOfInterestForCreationDto, entities.PointOfInterest>();
            CreateMap<models.PointOfInterestForUpdateDto, entities.PointOfInterest>();
            CreateMap<entities.PointOfInterest, models.PointOfInterestForUpdateDto>();
        }
    }
}
