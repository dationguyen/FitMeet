using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class ResponseMessage<T>
    {
        [JsonProperty("output")]
        public Output<T> Output { get; set; }
    }
}
