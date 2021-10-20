using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using System.Web.Http;

namespace FlightPlanner3.web.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IFlightSearchValidator _validator;
        public CustomerController(IAirportService airportService, IFlightService flightService, IMapper mapper, IFlightSearchValidator validator)
        {
            _airportService = airportService;
            _mapper = mapper;
            _flightService = flightService;
            _validator = validator;
        }
        [HttpGet]
        [Route("api/airports")]
        public IHttpActionResult GetAirport(string search)
        {
            var searchResult = _airportService.SearchAirport(search);
            var result = _mapper.Map<AirportResponse[]>(searchResult);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/flights/search")]
        public IHttpActionResult SearchFlight(FlightSearch flight)
        {
            if (!_validator.IsValid(flight))
                return BadRequest();
            
            var result = _airportService.SearchFlight(flight);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/flights/{id}")]
        public IHttpActionResult GetFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight != null)
            {
                var result = _mapper.Map<FlightResponse>(flight);
                return Ok(result);
            }
                
            return NotFound();
        }
    }
}
