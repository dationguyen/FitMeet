using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class FacebookProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }
}
