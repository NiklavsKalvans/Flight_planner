using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validations
{
    public class FlightSearchValidator : IFlightSearchValidator
    {
        public bool IsValid(FlightSearch flight)
        {
            return flight?.to != flight?.from;
        }
    }
}
