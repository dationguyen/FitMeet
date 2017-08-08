using Newtonsoft.Json;

namespace FitMeet.Models
{
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
        public T Response { get; set; }
    }



}
