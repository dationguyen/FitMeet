using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class ActivityModel
    {
        [JsonProperty("activityUserId")]
        public string ActivityUserId { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("activityIcon")]
        public string ActivityIcon { get; set; }

        [JsonProperty("activityTitle")]
        public string ActivityTitle { get; set; }
    }

}
