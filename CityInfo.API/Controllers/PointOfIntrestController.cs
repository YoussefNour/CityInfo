using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using CityInfo.API.models;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfIntrestController : ControllerBase
    {
        public ILogger<PointOfIntrestController> _Logger;
        public IMailService _MailService;
        public PointOfIntrestController(ILogger<PointOfIntrestController> logger, IMailService mailService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _MailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetCityPointsOfInterests(int cityid)
        {
            try
            {
                // throw new Exception("Could not load point of interests");
                var city = CitiesDataStore.current.Cities.Find(c => c.id == cityid);

                if (city == null)
                {
                    _Logger.LogInformation(
                        $"City with id {cityid} was not found when accessing point of interest"
                    );
                    return NotFound();
                }
                else
                {
                    return Ok(city.PointsOfInterest);
                }
            }
            catch (Exception e)
            {
                _Logger.LogCritical(
                    $"Execption while getting points of interest with id {cityid}.",
                    e
                );
                return StatusCode(500, "A problem happened while handeling request.");
            }
        }

        [HttpGet("{pointofInterestID}", Name = "getPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            int cityid,
            int pointofInterestID
        )
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.id == cityid);

            var poi = city?.PointsOfInterest.Find(p => p.Id == pointofInterestID);

            if (poi == null || city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poi);
            }
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto poi
        )
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var nextId = CitiesDataStore.current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);
            var newPoi = new PointOfInterestDto()
            {
                Id = ++nextId,
                Name = poi.Name,
                Description = poi.Description
            };

            city.PointsOfInterest.Add(newPoi);
            return CreatedAtRoute(
                "getPointOfInterest",
                new { cityId, pointOfInterestId = newPoi.Id },
                newPoi
            );
        }

        [HttpPut("{poiId}")]
        public ActionResult UpdatePointOfInterest(
            int cityId,
            int poiId,
            PointOfInterestForUpdateDto pointOfInterest
        )
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromDataStore = city.PointsOfInterest.Find(p => p.Id == poiId);
            if (pointOfInterestFromDataStore == null)
            {
                return NotFound();
            }
            pointOfInterestFromDataStore.Name = pointOfInterest.Name;
            pointOfInterestFromDataStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofinterestId}")]
        public ActionResult PartialUpdatePointOfInterest(
            int cityId,
            int pointofinterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
        )
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromDataStore = city.PointsOfInterest.Find(
                p => p.Id == pointofinterestId
            );
            if (pointOfInterestFromDataStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromDataStore.Name,
                Description = pointOfInterestFromDataStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //since the input model is jsonpatchdocument not pointofinterestforupdate the previous check will pass some checks such ass removing setting name to null thus we should also try validate the model after applying the patch doc
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromDataStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromDataStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointofinterestId)
        {
            var city = CitiesDataStore.current.Cities.Find(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromDataStore = city.PointsOfInterest.Find(
                p => p.Id == pointofinterestId
            );
            if (pointOfInterestFromDataStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromDataStore);

            return NoContent();
        }
    }
}
