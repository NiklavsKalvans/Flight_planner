using FlightPlanner2.Db;
using FlightPlanner2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner2.Storage
{
    public static class FlightStorage
    {
        private static readonly object flightLock = new object();

        public static Flight GetById(int id)
        {
            using (var context = new FlightPlannerDbContext())
            {
                if (!context.Flights.Any())
                {
                    return null;
                }
                return context.Flights.Include("To").Include("From").SingleOrDefault(f => f.Id == id);
            }
        }

        public static Flight AddFLight(Flight flight)
        {
            lock (flightLock)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    context.Flights.Add(flight);
                    context.SaveChanges();
                    return flight;
                }
            }
        }

        public static bool Exists(Flight flight)
        {
            lock (flightLock)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    if (context.Flights.Any())
                    {
                        return context.Flights.Include("To").Include("From").Any(f => f.DepartureTime == flight.DepartureTime &&
                        f.ArrivalTime == flight.ArrivalTime &&
                        f.Carrier == flight.Carrier &&
                        f.From.AirportCode == flight.From.AirportCode && f.To.AirportCode == flight.To.AirportCode);
                    }
                    return false;
                }
            }
        }

        public static void DeleteById(int id)
        {
            lock (flightLock)
            {
                using (var context = new FlightPlannerDbContext())
                {
                    var itemToRemove = context.Flights.Include("To").Include("From").SingleOrDefault(f => f.Id == id);
                    if (itemToRemove != null)
                    {
                        context.Airports.Remove(itemToRemove.To);
                        context.Airports.Remove(itemToRemove.From);
                        context.Flights.Remove(itemToRemove);
                        context.SaveChanges();
                    }
                }
            }
        }

        public static Airport[] SearchAirport(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            using (var context = new FlightPlannerDbContext())
            {
                var airportResult = context.Flights.Select(f => f.From);
                var searchResult = airportResult.Where(f => f.AirportCode.ToLower().StartsWith(keyword) || f.City.ToLower().StartsWith(keyword) ||
                f.Country.ToLower().StartsWith(keyword)).ToArray();
                return searchResult;
            }
        }

        public static PageResult SearchFlight(FlightSearch fs)
        {
            using (var context = new FlightPlannerDbContext())
            {
                if (context.Flights.Any())
                {
                    var flight = context.Flights.Include("To").Include("From").First(f => f.From.AirportCode == fs.from && f.To.AirportCode == fs.to &&
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

        public static bool IsValidFlight(Flight flight)
        {
            if (string.IsNullOrEmpty(flight?.Carrier) || string.IsNullOrEmpty(flight?.ArrivalTime) || string.IsNullOrEmpty(flight?.DepartureTime))
                return false;

            if (string.IsNullOrEmpty(flight.From?.AirportCode) || string.IsNullOrEmpty(flight.From?.City) || string.IsNullOrEmpty(flight.From?.Country))
                return false;

            if (string.IsNullOrEmpty(flight.To?.AirportCode) || string.IsNullOrEmpty(flight.To?.City) || string.IsNullOrEmpty(flight.To?.Country))
                return false;

            if (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower())
                return false;

            if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
                return false;

            return true;
        }
    }
}