using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightSearchValidator
    {
        bool IsValid(FlightSearch flight);
    }
}
