using CityInfo.API.entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

        // void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        // void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        // void DeletePointOfInterest(PointOfInterest pointOfInterest);

        // Task<bool> SaveChangesAsync();

        // Task<bool> CityExistsAsync(int cityId);

        // Task<bool> PointOfInterestExistsAsync(int pointOfInterestId);

        // Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
        //     int cityId,
        //     IEnumerable<int> pointOfInterestIds
        // );

        // void AddPointsOfInterestForCity(int cityId, IEnumerable<PointOfInterest> pointOfInterest);
    }
}
