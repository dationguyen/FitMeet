using FitMeet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IFitMeetRestService
    {
        /// <summary>
        /// check current request
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<RequestModel>> CheckRequestAsync();

        /// <summary>
        /// Get the list of news
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<News>>> GetNewsAsync();

        /// <summary>
        /// Get the news detail
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<NewsDetail>> GetNewsDetailAsync(string id);

        /// <summary>
        /// Get the news detail
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<WebPageInfo>> GetPageDetailAsync(string id);

        /// <summary>
        /// Get the members list
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<Member>>> GetMembersAsync(int page);

        /// <summary>
        /// advance search member
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<Member>>> SearchMembersAsync(int page,int distance,string gender,List<int> activities);

        /// <summary>
        /// advance search member
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<ActivityData>> GetActivityDataAsync();

        /// <summary>
        /// get member Detail
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<MemberDetail>> GetMemberDetailAsync(string id);

        /// <summary>
        /// Get the friends list
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<Member>>> GetFriendsAsync(int page);

        /// <summary>
        /// Add Friend
        /// </summary>
        /// <returns></returns>
        Task<bool> AddFriendsAsync(string friendId);

        /// <summary>
        /// Add Friend
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<UserProfile>> GetUserProfileAsync();

        /// <summary>
        /// Add Friend
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<Place>>> GetTrainingLocationAsync();

        /// <summary>
        /// Edit Profile
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<string>> UpdateProfileAsync(string fName,string lName,string gender,string goalId,string goalText,string picture,string address,string desciption,string dob,List<string> activitesID,List<string> ids,List<string> skillLevelIds,List<string> placeIds,List<string> locationIds);

        /// <summary>
        /// Unfriend
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<string>> UnfriendAsync(string id);

        /// <summary>
        /// Block
        /// </summary>
        /// <returns></returns>
        Task<bool> BlockfriendAsync(string id,string message = null);

        /// <summary>
        /// Manual Login
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<LoginModel>> ManualLoginAsync(string id,string password);


        /// <summary>
        /// Check token
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckTokenAsync(string token);

        /// <summary>
        /// Get a list of message 
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<MessageModel>>> GetMessagesAsync(string id);

        /// <summary>
        /// Get a specific message
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<MessageModel>>> GetMessageAsync(string friendId,string messageId);

        /// <summary>
        /// Facebook Login
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<SignUpResponse>> FacebookLoginAsync(FacebookProfile profile);

        /// <summary>
        /// Facebook Login
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<SignUpResponse>> EmailSignUpAsync(string email,string password,bool isSubscibleNews,bool isShareInfo);

        /// <summary>
        /// Sign Up Step 2 Async
        /// </summary>
        Task<bool> SignUpStep2Async(string address,string description,string fname,string lname,string dob,bool gender);

        /// <summary>
        /// Sign Up Step 3 Async
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<string>> SignUpStep3Async(List<ActivityKey> activities,List<Place> places,Goal goal);


        /// <summary>
        /// Send Message
        /// </summary>
        Task<int> CheckMessage();

        /// <summary>
        /// Send Message
        /// </summary>
        Task<bool> SendMessageAsync(string message,string friendId);

        /// <summary>
        /// Send Message
        /// </summary>
        Task<bool> ResponseToFriendAsync(string friendId,string status);

        /// <summary>
        /// Verify account
        /// </summary>
        Task<bool> VerifyAsync();

        /// <summary>
        /// Set current token
        /// </summary>
        /// <param name="token"></param>
        void SetToken(string token);

        /// <summary>
        /// Set current token
        /// </summary>
        /// <param name="token"></param>
        void LogOut();
    }
}
