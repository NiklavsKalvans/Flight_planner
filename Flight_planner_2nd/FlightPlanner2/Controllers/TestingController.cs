using FlightPlanner2.Db;
using System.Web.Http;

namespace FlightPlanner2.Controllers
{
    public class TestingController : ApiController
    {
        [HttpPost]
        [Route("testing-api/clear")]
        public IHttpActionResult Clear()
        {
            using (var context = new FlightPlannerDbContext())
            {
                foreach (var entity in context.Flights.Include("To").Include("From"))
                {
                    if (entity.To != null)
                        context.Airports.Remove(entity?.To);
                    if (entity.From != null)
                        context.Airports.Remove(entity?.From);
                    context.Flights.Remove(entity);
                }
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
