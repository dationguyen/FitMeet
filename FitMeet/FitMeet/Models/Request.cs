using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class Request
    {
        [JsonProperty("message")]
        public int Message { get; set; }

        [JsonProperty("friend")]
        public int Friend { get; set; }
    }
}