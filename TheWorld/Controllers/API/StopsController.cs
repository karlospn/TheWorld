using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.Repository;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.API
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private readonly IWorldRepository _repository;

        public StopsController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTrip(tripName);
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
                    _repository.AddStopToTrip(tripName, stopEntity);
                    if (await _repository.SaveContext() > 0)
                    {
                        return Created($"/api/trips/{stopViewModel.Name}/stops", Mapper.Map<StopViewModel>(stopEntity));
                    }
                    return BadRequest("Failed to save changes to BBDD");
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
