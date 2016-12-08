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
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var trips = _repository.GetAllTrips();
                return Ok(trips);
            }
            catch (Exception ex)
            {            
               return BadRequest("Bad Request");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] TripViewModel tripViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tripEntity = Mapper.Map<Trip>(tripViewModel);
                    _repository.AddTrip(tripEntity);
                    if (await _repository.SaveContext() > 0)
                    {
                        return Created($"/api/trips/{tripViewModel.Name}", Mapper.Map<TripViewModel>(tripEntity));
                    }
                    return BadRequest("Failed to save changes to BBDD");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest("Bad Request");
            }
        }
    }
}
