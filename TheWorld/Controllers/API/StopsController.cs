using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.Repository;
using TheWorld.Services.GeoCodesService;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.API
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly IGeoCoordsService _geoCoordsService;

        public StopsController(IWorldRepository repository, IGeoCoordsService geoCoordsService)
        {
            _repository = repository;
            _geoCoordsService = geoCoordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var user = User.Identity.Name;
                var trip = _repository.GetUserTrip(tripName, user);
                if (trip != null)
                {
                    return Ok(Mapper.Map<ICollection<StopViewModel>>(trip.Stops));
                }
                return BadRequest("Trip not exist in BBDD");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Retrieving Trip");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel stopViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var stopEntity = Mapper.Map<Stop>(stopViewModel);

                    var geoCoords = await _geoCoordsService.GetCoordsAsync(stopEntity.Name);
                    if (geoCoords.Success)
                    {
                        stopEntity.Latitude = geoCoords.Latitude;
                        stopEntity.Longitude = geoCoords.Longitude;


                        _repository.AddStopToUserTrip(tripName, stopEntity, User.Identity.Name);
                        if (await _repository.SaveContext() > 0)
                        {
                            return Created($"/api/trips/{stopViewModel.Name}/stops",
                                Mapper.Map<StopViewModel>(stopEntity));
                        }
                        return BadRequest("Failed to save changes to BBDD");
                    }
                    return BadRequest(geoCoords.Message);
                }
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                return BadRequest("Error Saving Stop to Trip");       
            }
        }
    }
}
