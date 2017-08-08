using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class Activity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon_white")]
        public string Icon_white { get; set; }
    }
}
