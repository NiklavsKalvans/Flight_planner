using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner3.web.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FlightPlanner3.web.Controllers
{
    [BasicAuthenticationAtribute]
    public class AdminController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IValidator> _validators;
        public AdminController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(_mapper.Map<FlightResponse>(flight));
        }

        [HttpPut]
        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight(FlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);

            if (!_validators.All(a=>a.IsValid(request)))
                return BadRequest();

            if (_flightService.Exists(flight))
                return Conflict();
            
            _flightService.Create(flight);

            return Created("", _mapper.Map<FlightResponse>(flight));
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        public IHttpActionResult DeleteFlight(int id)
        {
            _flightService.DeleteFlightById(id);
            return Ok();
        }
    }
}
