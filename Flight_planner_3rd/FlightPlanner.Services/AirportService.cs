using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FLightPlanner.Data;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {

        }

        public Airport[] SearchAirport(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            var airportResult = _context.Flights.Select(f => f.From);
            var searchResult = airportResult.Where(f => f.AirportCode.ToLower().StartsWith(keyword) || f.City.ToLower().StartsWith(keyword) ||
            f.Country.ToLower().StartsWith(keyword)).ToArray();
            return searchResult;
        }

        public PageResult SearchFlight(FlightSearch fs)
        {
            if (_context.Flights.Any())
            {
                var flight = _context.Flights.Include("To").Include("From").First(f => f.From.AirportCode == fs.from && f.To.AirportCode == fs.to &&
                f.DepartureTime.Substring(0, 10) == fs.departureDate);
                PageResult result = new PageResult();
                result.items = new List<Flight>();
                if (flight != null)
                    result.items.Add(flight);
                return result;
            }
            PageResult resultIfDbisEmpty = new PageResult();
            resultIfDbisEmpty.items = new List<Flight>();
            return resultIfDbisEmpty;
        }
    }
}
