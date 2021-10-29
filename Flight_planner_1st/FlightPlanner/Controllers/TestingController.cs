using FlightPlanner.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightPlanner.Controllers
{
    public class TestingController : ApiController
    {
        [HttpPost]
        [Route("testing-api/clear")]
        public IHttpActionResult Clear()
        {
            FlightStorage.ClearFlights();
            return Ok();
        }
    }
}
