using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
