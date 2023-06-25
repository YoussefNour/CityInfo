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

        public CitiesController(ICityInfoRepository citiesDataStore, IMapper mapper)
        {
            _cityInfoRepository =
                citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities));
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
