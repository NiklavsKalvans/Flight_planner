using FlightPlanner2.Models;
using FlightPlanner2.Storage;
using System.Web.Http;

namespace FlightPlanner2.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("api/airports")]
        public IHttpActionResult GetAirport(string search)
        {
            var searchResult = FlightStorage.SearchAirport(search);
            return Ok(searchResult);
        }

        [HttpPost]
        [Route("api/flights/search")]
        public IHttpActionResult SearchFlight(FlightSearch flight)
        {
            if (flight == null)
                return BadRequest();
            if (flight.from == flight.to)
                return BadRequest();
            var result = FlightStorage.SearchFlight(flight);
                return Ok(result);
        }

        [HttpGet]
        [Route("api/flights/{id}")]
        public IHttpActionResult GetFlightById(int id)
        {
            var flight = FlightStorage.GetById(id);
            if (FlightStorage.Exists(flight))
                return Ok(flight);
            return NotFound();
        }
    }
}
