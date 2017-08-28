using Newtonsoft.Json;

namespace FitMeet.Models
{
    public class MessageModel
    {
        public bool IsClient { get; set; }

        [JsonProperty("messageId")]
        public int MessageId { get; set; }

        [JsonProperty("senderId")]
        public string SenderId { get; set; }

        [JsonProperty("senderName")]
        public string SenderName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("senderImage")]
        public string SenderImage { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

}
