using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class Place
    {
        [JsonProperty("locationAddress")]
        public string LocationAddress { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; }

        [JsonProperty("locationTitle")]
        public string LocationTitle { get; set; }
    }
}