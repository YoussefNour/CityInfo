using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using CityInfo.API.models;
using CityInfo.API.Services;
using AutoMapper;
using CityInfo.API.entities;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfIntrestController : ControllerBase
    {
        private readonly ILogger<PointOfIntrestController> _Logger;
        private readonly IMailService _MailService;
        private readonly ICityInfoRepository _CityInfoRepository;
        private readonly IMapper _Mapper;

        public PointOfIntrestController(
            ILogger<PointOfIntrestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper
        )
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _MailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _CityInfoRepository =
                cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetCityPointsOfInterests(
            int cityid
        )
        {
            try
            {
                var cityExist = await _CityInfoRepository.CityExistsAsync(cityid);
                if (!cityExist)
                {
                    _Logger.LogInformation(
                        $"City with id {cityid} wasn't found when accessing points of interest."
                    );
                    return NotFound();
                }
                var pointOfInterest = await _CityInfoRepository.GetPointsOfInterestForCityAsync(
                    cityid
                );
                return Ok(_Mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterest));
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
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityid,
            int pointofInterestID
        )
        {
            try
            {
                var cityExist = await _CityInfoRepository.CityExistsAsync(cityid);
                if (!cityExist)
                {
                    _Logger.LogInformation(
                        $"City with id {cityid} wasn't found when accessing points of interest."
                    );
                    return NotFound();
                }
                var pointOfInterest = await _CityInfoRepository.GetPointOfInterestForCityAsync(
                    cityid,
                    pointofInterestID
                );
                if (pointOfInterest == null)
                {
                    return NotFound();
                }
                return Ok(_Mapper.Map<PointOfInterestDto>(pointOfInterest));
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

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto poi)
        {
            if (!await _CityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _Mapper.Map<PointOfInterest>(poi);

            await _CityInfoRepository.AddPointsOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _CityInfoRepository.SaveChangesAsync();

            var createdPointOfInterest = _Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointofinterestId = createdPointOfInterest.Id
            },
            createdPointOfInterest);
        }

        [HttpPut("{poiid}")]
        public async Task<ActionResult> updatepointofinterest(
            int cityid,
            int poiid,
            PointOfInterestForUpdateDto pointofinterest
        )
        {
            if (!await _CityInfoRepository.CityExistsAsync(cityid))
            {
                return NotFound();
            }

            var pointofinterestfromdatabase = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityid,poiid);
            if (pointofinterestfromdatabase == null)
            {
                return NotFound();
            }
            _Mapper.Map(pointofinterest, pointofinterestfromdatabase);
            await _CityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        //     [HttpPatch("{pointofinterestId}")]
        //     public ActionResult PartialUpdatePointOfInterest(
        //         int cityId,
        //         int pointofinterestId,
        //         JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
        //     )
        //     {
        //         var city = _PointOfInterestRepository.Cities.Find(c => c.Id == cityId);
        //         if (city == null)
        //         {
        //             return NotFound();
        //         }

        //         var pointOfInterestFromDataStore = city.PointsOfInterest.Find(
        //             p => p.Id == pointofinterestId
        //         );
        //         if (pointOfInterestFromDataStore == null)
        //         {
        //             return NotFound();
        //         }

        //         var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        //         {
        //             Name = pointOfInterestFromDataStore.Name,
        //             Description = pointOfInterestFromDataStore.Description
        //         };

        //         patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }
        //         //since the input model is jsonpatchdocument not pointofinterestforupdate the previous check will pass some checks such ass removing setting name to null thus we should also try validate the model after applying the patch doc
        //         if (!TryValidateModel(pointOfInterestToPatch))
        //         {
        //             return BadRequest(ModelState);
        //         }

        //         pointOfInterestFromDataStore.Name = pointOfInterestToPatch.Name;
        //         pointOfInterestFromDataStore.Description = pointOfInterestToPatch.Description;

        //         return NoContent();
        //     }

        //     [HttpDelete("{pointofinterestId}")]
        //     public ActionResult DeletePointOfInterest(int cityId, int pointofinterestId)
        //     {
        //         var city = _PointOfInterestRepository.Cities.Find(c => c.Id == cityId);
        //         if (city == null)
        //         {
        //             return NotFound();
        //         }

        //         var pointOfInterestFromDataStore = city.PointsOfInterest.Find(
        //             p => p.Id == pointofinterestId
        //         );
        //         if (pointOfInterestFromDataStore == null)
        //         {
        //             return NotFound();
        //         }

        //         city.PointsOfInterest.Remove(pointOfInterestFromDataStore);
        //         _MailService.Send(
        //             "Point of interest deleted",
        //             $"Point of interest {pointOfInterestFromDataStore.Name} with id {pointOfInterestFromDataStore.Id} was deleted"
        //         );
        //         return NoContent();
        //     }
        // }
    }
}
