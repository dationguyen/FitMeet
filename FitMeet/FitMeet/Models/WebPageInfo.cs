using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class WebPageInfo
    {
        [JsonProperty("Pagetitle")]
        public string PageTitle { get; set; }

        [JsonProperty("Pagedescp")]
        public string PageDescp { get; set; }
    }

}
