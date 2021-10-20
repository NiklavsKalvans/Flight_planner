using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FLightPlanner.Data;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {

        }
        public Flight GetFullFlightById(int id)
        {
            return _context.Flights.Include("To").Include("From").SingleOrDefault(f=> f.Id == id);
        }

        public void DeleteFlightById(int id)
        {
            var itemToRemove = _context.Flights.Include("To").Include("From").SingleOrDefault(f => f.Id == id);
            if (itemToRemove != null)
            {
                _context.Airports.Remove(itemToRemove.To);
                _context.Airports.Remove(itemToRemove.From);
                _context.Flights.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Include("To").Include("From").Any(f => f.DepartureTime == flight.DepartureTime &&
                        f.ArrivalTime == flight.ArrivalTime &&
                        f.Carrier == flight.Carrier &&
                        f.From.AirportCode == flight.From.AirportCode && f.To.AirportCode == flight.To.AirportCode);
        }
    }
}
