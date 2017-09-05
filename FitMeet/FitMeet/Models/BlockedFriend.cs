using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class BlockedFriend
    {
        public string FullName
        {
            get => string.Format("{0} {1}",MemberFirstName,MemberLastName);

        }

        public string GenderLogoUrl
        {
            get
            {
                string url = "";
                if(Gender != null)
                {
                    if(Gender.ToLower() == "male")
                        url = "male.png";
                    else
                    {
                        url = "female.png";
                    }
                }
                return url;
            }
        }

        [JsonProperty("memberId")]
        public string MemberId { get; set; }

        [JsonProperty("memberFirstName")]
        public string MemberFirstName { get; set; }

        [JsonProperty("memberLastName")]
        public string MemberLastName { get; set; }

        [JsonProperty("memberEmail")]
        public string MemberEmail { get; set; }

        [JsonProperty("memberPhoto")]
        public string MemberPhoto { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}
