using Newtonsoft.Json;
using System;

namespace FitMeet.Models
{
    public class UserProfile : MemberDetail
    {
        public bool IsMale => Gender.Equals("male" , StringComparison.OrdinalIgnoreCase);

        [JsonProperty("address")]
        public string Address { get; set; }       

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("isVerified")]
        public string IsVerified { get; set; }      

        [JsonProperty("goalId")]
        public string GoalId { get; set; }      

        [JsonProperty("hasFacebook")]
        public string HasFacebook { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public class Request
    {
        [JsonProperty("message")]
        public int Message { get; set; }

        [JsonProperty("friend")]
        public int Friend { get; set; }
    }

}



