using CityInfo.API.models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase //not controller because controller has helper functions that help return views but controller base is smaller and fits apis more
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository citiesDataStore)
        {
            _cityInfoRepository = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            var citiesDto = cities.Select(c => new CityWithoutPointOfInterestDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
            return Ok(citiesDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity([FromRoute] int id)
        {
            // var city = _cityInfoRepository.Cities.Find(c => c.Id == id);
            // if (city == null)
            // {
            //     return NotFound();
            // }
            // else
            // {
            //     return Ok(city);
            // }
            return Ok();
        }
    }
}
