using FlightPlanner.Authentication;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using System.Web.Http;

namespace FlightPlanner.Controllers
{
    [BasicAuthenticationAtribute]
    public class AdminController : ApiController
    {
        [HttpGet]
        [Route("admin-api/flights/{id}")]
        public IHttpActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
            if(flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [HttpPut]
        [Route("admin-api/flights")]
        public IHttpActionResult PutFlight(Flight flight)
        {
            if (!FlightStorage.IsValidFlight(flight))
                return BadRequest();

            if (FlightStorage.Exists(flight))
                return Conflict();

            FlightStorage.AddFLight(flight);
                return Created("", flight);
        }

        [HttpDelete]
        [Route("admin-api/flights/{id}")]
        public IHttpActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteById(id);
            return Ok();
        }
    }
}
