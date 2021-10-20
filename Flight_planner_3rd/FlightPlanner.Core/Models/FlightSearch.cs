using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Models
{
    public class FlightSearch
    {
        [Required]
        public string from { get; set; }
        [Required]
        public string to { get; set; }
        [Required]
        public string departureDate { get; set; }
    }
}
