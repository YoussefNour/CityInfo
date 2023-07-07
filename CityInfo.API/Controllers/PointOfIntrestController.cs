using AutoMapper;
using CityInfo.API.entities;
using CityInfo.API.models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
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

            var pointofinterestfromdatabase = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityid, poiid);
            if (pointofinterestfromdatabase == null)
            {
                return NotFound();
            }
            _Mapper.Map(pointofinterest, pointofinterestfromdatabase);
            await _CityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{pointofinterestId}")]
        public async Task<ActionResult> PartialUpdatePointOfInterest(
            int cityId,
            int pointofinterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
        )
        {
            if (!await _CityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = _Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

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

            _Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await _CityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointofinterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointofinterestId)
        {
            if (!await _CityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _CityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            await _CityInfoRepository.SaveChangesAsync();

            _MailService.Send(
                "Point of interest deleted",
                $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted"
            );
            return NoContent();
        }
    }
}
