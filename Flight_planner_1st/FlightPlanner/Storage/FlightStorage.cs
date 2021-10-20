using FlightPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        private static readonly object flightLock = new object();
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;

        public static Flight GetById(int id)
        {
            if (_flights == null)
            {
                return null;
            }
            return _flights.SingleOrDefault(f => f.Id == id);
        }
        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static Flight AddFLight(Flight flight)
        {
            lock (flightLock)
            {
                _flights.Add(flight);
                flight.Id = ++_id;
                return flight;
            }
        }

        public static bool Exists(Flight flight)
        {
            lock (flightLock)
            {
                return _flights.Any(f => f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime &&
                f.Carrier == flight.Carrier &&
                f.From.AirportCode == flight.From.AirportCode && f.To.AirportCode == flight.To.AirportCode);
            }
        }

        public static void DeleteById(int id)
        {
            lock (flightLock)
            {
                var itemToRemove = _flights.SingleOrDefault(f => f.Id == id);
                if (itemToRemove != null)
                    _flights.Remove(itemToRemove);
            }
        }

        public static Airport[] SearchAirport(string keyword)
        {
            keyword = keyword.Trim().ToLower();
            var airportResult = _flights.Select(f => f.From);
            var searchResult = airportResult.Where(f => f.AirportCode.ToLower().StartsWith(keyword) || f.City.ToLower().StartsWith(keyword) ||
            f.Country.ToLower().StartsWith(keyword)).ToArray();
            return searchResult;
        }

        public static PageResult SearchFlight(FlightSearch fs)
        {
            var flight = _flights.Find(f => f.From.AirportCode == fs.from && f.To.AirportCode == fs.to &&
            f.DepartureTime.Substring(0, 10) == fs.departureDate);
            PageResult result = new PageResult();
            result.items = new List<Flight>();
            if (flight != null)
                result.items.Add(flight);
            return result;
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