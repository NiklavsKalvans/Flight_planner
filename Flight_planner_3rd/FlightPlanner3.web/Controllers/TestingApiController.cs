using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using System.Web.Http;

namespace FlightPlanner3.web.Controllers
{
    public class TestingApiController : ApiController
    {
        private readonly IDbServiceExtended _service;
        public TestingApiController(IDbServiceExtended service)
        {
            _service = service;
        }
        [Route("testing-api/clear")]
        public IHttpActionResult Clear()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();

            return Ok();
        }
    }
}
