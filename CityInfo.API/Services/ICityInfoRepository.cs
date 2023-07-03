using CityInfo.API.entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery,int pageNumber, int pageSize);

        Task<City> GetCityAsync(int cityId, bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);

        // void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        // void UpdatePointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        // void DeletePointOfInterest(PointOfInterest pointOfInterest);

        Task<bool> SaveChangesAsync();

        Task<bool> CityExistsAsync(int cityId);

        // Task<bool> PointOfInterestExistsAsync(int pointOfInterestId);

        // Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
        //     int cityId,
        //     IEnumerable<int> pointOfInterestIds
        // );

        Task AddPointsOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
