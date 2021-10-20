using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FlightPlanner2.Models
{
    public class Airport
    {
        [Key, JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonProperty("airport")]
        public string AirportCode { get; set; }
    }
}