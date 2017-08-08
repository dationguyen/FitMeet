using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class ResponseMessage<T>
    {
        [JsonProperty("output")]
        public Output<T> Output { get; set; }
    }
}
