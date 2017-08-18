using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class LoginModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("profile_status")]
        public string ProfileStatus { get; set; }
    }

}
