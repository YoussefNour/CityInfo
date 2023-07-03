using CityInfo.API.models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase //not controller because controller has helper functions that help return views but controller base is smaller and fits apis more
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository citiesDataStore, IMapper mapper)
        {
            _cityInfoRepository =
                citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities(
            [FromQuery] string? name,
            string? searchQuery,
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            if (pageSize > maxCitiesPageSize)
                return BadRequest($"Page size cannot be greater than {maxCitiesPageSize}");
            var cities = await _cityInfoRepository.GetCitiesAsync(
                name,
                searchQuery,
                pageNumber,
                pageSize
            );
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(
            [FromRoute] int id,
            bool includePointsOfInterest = false
        )
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
                return NotFound();
            if (includePointsOfInterest)
                return Ok(_mapper.Map<CityDto>(city));
            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
        }
    }
}
