using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class Level
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
