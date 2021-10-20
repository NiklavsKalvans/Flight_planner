using System.Collections.Generic;

namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int page { get; set; }
        public int totalItems => items.Count;
        public List<Flight> items { get; set; }
    }
}