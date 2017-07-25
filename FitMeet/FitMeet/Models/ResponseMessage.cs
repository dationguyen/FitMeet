using FitMeet.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class ResponseMessage<T>
    {
        [JsonProperty("output")]
        public Output<T> Output { get; set; }
    }

    public class Output<T>
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("pagecount")]
        public int Pagecount { get; set; }

        [JsonProperty("currentpage")]
        public int Currentpage { get; set; }

        [JsonProperty("validation")]
        public string Validation { get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("response")]
        [JsonConverter(typeof(SingleOrArrayJsonConverter<>))]
        public List<T> Response { get; set; }
    }



}
