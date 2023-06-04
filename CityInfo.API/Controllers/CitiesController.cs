using CityInfo.API.models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase //not controller because controller has helper functions that help return views but controller base is smaller and fits apis more
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity([FromRoute] int id)
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city);
            }
        }
    }
}
