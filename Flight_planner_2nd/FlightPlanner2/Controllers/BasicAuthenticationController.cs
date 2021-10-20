using FlightPlanner2.Authentication;
using System.Web.Http;

namespace BasicAuthentication.Controllers
{
    public class ValuesController : ApiController
    {
        [BasicAuthenticationAtribute]
        public string Get()
        {
            return "WebAPI Method Called";
        }
    }
}
