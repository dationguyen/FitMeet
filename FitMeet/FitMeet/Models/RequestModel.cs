using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FitMeet.Models
{
    public class RequestModel
    {
        [JsonProperty("countfriend")]
        public int Countfriend { get; set; }

        [JsonProperty("friend")]
        public List<Friend> Friend { get; set; }

        [JsonProperty("countmsg")]
        public int Countmsg { get; set; }

        [JsonProperty("msg")]
        public List<Msg> Msg { get; set; }
    }

    public interface IRequestItem
    {
        string UserId { get; set; }
        string UserPhoto { get; set; }
        string UserName { get; }
        string Message { get; }
        int Count { get; }

        bool Equals(object obj);
    }

    public class Friend:IRequestItem
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserFirstName { get; set; }


        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty("userPhoto")]
        public string UserPhoto { get; set; }

        public string UserName
        {
            get => "You have a friend request";
        }
        public string Message
        {
            get => String.Format("from {0} ",UserFirstName);
        }

        public int Count
        {
            get => 1;
        }

        public override bool Equals(object obj)
        {
            var requestItem = (IRequestItem)obj;
            return (requestItem.Count == Count && requestItem.UserId == UserId);
        }

        public override int GetHashCode()
        {
            var hashCode = 1835195239;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            return hashCode;
        }
    }

    public class Msg:IRequestItem
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("messageDate")]
        public string MessageDate { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userPhoto")]
        public string UserPhoto { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("is_last_sender")]
        public int IsLastSender { get; set; }



        [JsonProperty("count")]
        public int Count { get; set; }

        public override bool Equals(object obj)
        {
            var requestItem = (IRequestItem)obj;
            return (requestItem.Count == Count && requestItem.UserId == UserId && requestItem.Message == Message);
        }

        public override int GetHashCode()
        {
            var hashCode = 219605979;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MessageId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MessageDate);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserPhoto);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            hashCode = hashCode * -1521134295 + IsLastSender.GetHashCode();
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            return hashCode;
        }
    }

}
