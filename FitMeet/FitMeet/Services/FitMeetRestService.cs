using FitMeet.Models;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitMeet.Services

{
    public class FitMeetRestService:RestApiClient, IFitMeetRestService
    {
        private const string baseUri = "http://52.64.164.241/";
        private const string getNewsUri = "News/index.json";
        private const string getPageUri = "contentpages/getpage.json";
        private const string getNewsDetailUri = "News/view.json";
        private const string getMemberUri = "users/index/page:{0}.json";
        private const string getFriendUri = "friends/index/page:{0}.json";
        private const string getProfileUri = "Users/my_profile.json";
        private const string addFriendUri = "Friends/add.json";
        private const string getTrainPlacesUri = "Locations/listTrainPlaces.json";
        private const string getMemberDetailUri = "/users/view.json";
        private const string searchMemberUri = "Users/advance_search/page:{0}.json";
        private const string getActivityDataUri = "activities/index.json";
        private const string UpdateProfileUri = "Users/edit_profile.json";
        private const string UnfriendUri = "friends/unfriend.json";
        private const string ManualLoginUri = "Users/login.json";
        private const string CheckTokenUri = "users/check_token.json";
        private const string BlockFriendUri = "Users/block.json";
        private const string FBSignUpUri = "Users/signup_facebook.json";
        private const string LogOutUri = "users/logout.json";
        private const string GetMessagesUri = "Messages/msg_paginate.json";
        private const string GetMessageUri = "Messages/get_msg.json";
        private const string SendMessageUri = "Messages/save_msg.json";
        private const string CheckMessageUri = "Users/count_msg.json";
        private const string CheckRequestUri = "users/check_request.json";
        private const string ResponseFriendUri = "Friends/respond_friend_request.json";
        private const string EmailSignUpUri = "users/signup_email.json";
        private const string SignUpStep2Uri = "Users/signup_step2.json";
        private const string SignUpStep3Uri = "Users/signup_step3.json";
        private const string VerifyUri = "Users/signup_step3.json";



        private readonly IPageDialogService _dialogService;
        private readonly IGeoLocationService _geoService;

        private string _token = "";


        public FitMeetRestService(IPageDialogService dialogService,IGeoLocationService geoService)
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUri) };
            _dialogService = dialogService;
            _geoService = geoService;
        }

        public async Task<bool> AddFriendsAsync(string friendId)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",friendId }
            };
            var result = await ApiPost<ResponseMessage<string>>(addFriendUri,param);
            return result?.Output?.Status == 1;
        }

        public async Task<bool> BlockfriendAsync(string id,string message = null)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",id }
            };
            if(!String.IsNullOrEmpty(message))
            {
                param.Add("message",message);
            }
            var result = await ApiPost<ResponseMessage<string>>(addFriendUri,param);
            return result?.Output?.Status == 1;
        }

        public async Task<int> CheckMessage()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token }
            };
            var result = await ApiPost<ResponseMessage<int>>(CheckMessageUri,param);
            int count = 0;
            if(result?.Output != null)
            {
                count = result.Output.Response;
            }

            return count;
        }

        public async Task<ResponseMessage<RequestModel>> CheckRequestAsync()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "date",DateTime.Now.ToString("yyyy-MM-dd")}
            };
            return await ApiPost<ResponseMessage<RequestModel>>(CheckRequestUri,param);
        }

        public async Task<bool> CheckTokenAsync(string token)
        {
            var param = new Dictionary<string,string>
            {
                { "token",token }
            };
            var result = await ApiPost<ResponseMessage<string>>(CheckTokenUri,param);
            return result?.Output?.Status == 1;
        }

        public async Task<ResponseMessage<SignUpResponse>> EmailSignUpAsync(string email,string password,bool isSubscibleNews,bool isShareInfo)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string,string>
            {
                { "email",email },
                { "password",password },
                { "newsletter",isSubscibleNews?"1":"0" },
                { "share_info",isShareInfo?"1":"0"},
                { "device_type","I"},
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<SignUpResponse>>(EmailSignUpUri,param);
        }

        public async Task<ResponseMessage<SignUpResponse>> FacebookLoginAsync(FacebookProfile profile)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string,string>
            {
                { "fbid",profile.Id },
                { "email",profile.Email },
                { "fname",profile.FirstName },
                { "lname",profile.LastName },
                { "picture",profile.Picture?.Data?.Url},
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<SignUpResponse>>(FBSignUpUri,param);
        }

        public async Task<ResponseMessage<ActivityData>> GetActivityDataAsync()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token }
            };
            return await ApiPost<ResponseMessage<ActivityData>>(getActivityDataUri,param);

        }

        public async Task<ResponseMessage<List<Member>>> GetFriendsAsync(int page)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(getFriendUri,page),param);
        }

        public async Task<ResponseMessage<MemberDetail>> GetMemberDetailAsync(string id)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "id", id }
            };
            return await ApiPost<ResponseMessage<MemberDetail>>(getMemberDetailUri,param);
        }

        public async Task<ResponseMessage<List<Member>>> GetMembersAsync(int pageId)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(getMemberUri,pageId),param);
        }

        public async Task<ResponseMessage<List<MessageModel>>> GetMessageAsync(string friendId,string messageId)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",friendId },
                { "id",messageId }
            };
            return await ApiPost<ResponseMessage<List<MessageModel>>>(GetMessageUri,param);
        }

        public async Task<ResponseMessage<List<MessageModel>>> GetMessagesAsync(string id)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",id }
            };
            return await ApiPost<ResponseMessage<List<MessageModel>>>(GetMessagesUri,param);
        }

        /// <summary>
        /// Get the list of news
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseMessage<List<News>>> GetNewsAsync()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token }
            };
            return await ApiPost<ResponseMessage<List<News>>>(getNewsUri,param);
        }

        public async Task<ResponseMessage<NewsDetail>> GetNewsDetailAsync(string id)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "id", id }
            };
            return await ApiPost<ResponseMessage<NewsDetail>>(getNewsDetailUri,param);
        }

        public async Task<ResponseMessage<WebPageInfo>> GetPageDetailAsync(string pageName)
        {
            var param = new Dictionary<string,string>
            {
                { "slug", pageName }
            };
            return await ApiPost<ResponseMessage<WebPageInfo>>(getPageUri,param);
        }

        public async Task<ResponseMessage<List<Place>>> GetTrainingLocationAsync()
        {
            var param = new Dictionary<string,string>();

            return await ApiPost<ResponseMessage<List<Place>>>(getTrainPlacesUri,param);
        }

        public async Task<ResponseMessage<UserProfile>> GetUserProfileAsync()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token }
            };
            return await ApiPost<ResponseMessage<UserProfile>>(getProfileUri,param);
        }

        public async void LogOut()
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token }
            };
            await ApiPost<ResponseMessage<string>>(LogOutUri,param);
        }

        public async Task<ResponseMessage<LoginModel>> ManualLoginAsync(string id,string password)
        {
            var param = new Dictionary<string,string>
            {
                { "device_token","APA91bGQORESHDWSf2cG6dPpcwrxm4ViL1a8u0bDMqBjkfKBGEVcy-k67YnWaMsX7rcVTpGQrSnVeOn5VEOlIjIeHXDV0f9a3QykLHHXK_6q2TlsO2o81qscqHMXbpGdH6fLzLXo08TD" },
                { "device_type","I" },
                { "password",password },
                { "email", id }
            };
            return await ApiPost<ResponseMessage<LoginModel>>(ManualLoginUri,param);
        }

        public async Task<bool> ResponseToFriendAsync(string friendId,string status)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",friendId },
                { "status", status }
            };
            var result = await ApiPost<ResponseMessage<string>>(ResponseFriendUri,param);
            return result?.Output?.Status == 1;
        }

        public async Task<ResponseMessage<List<Member>>> SearchMembersAsync(int page,int distance,string gender,List<int> activities)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() },
                { "gender", gender },
                { "distance", distance.ToString()}
            };
            for(int i = 0 ; i < activities.Count ; i++)
            {
                param.Add(String.Format("activity[{0}]",i),activities[i].ToString());
            }
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(searchMemberUri,page),param);

        }

        public async Task<bool> SendMessageAsync(string message,string friendId)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "friend",friendId },
                { "message", message }
            };
            var result = await ApiPost<ResponseMessage<string>>(SendMessageUri,param);
            return result?.Output?.Status == 1;
        }

        public void SetToken(string token)
        {
            this._token = token;
        }

        public async Task<bool> SignUpStep2Async(string address,string description,string fName,string lName,string dob,bool gender)
        {
            var position = await _geoService.GetPosition();
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "fname", fName },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() },
                { "gender", gender?"male":"female" },
                { "lname",lName },
                { "address", address },
                { "description", description },
                { "dob", dob }
            };
            var result = await ApiPost<ResponseMessage<string>>(SignUpStep2Uri,param);
            return (result != null && result.Output?.Status == 1);
        }

        public async Task<ResponseMessage<string>> SignUpStep3Async(List<ActivityKey> activities,List<Place> places,Goal goal)
        {
            var param = new Dictionary<string,string>
            {
                {"token", _token},
                {"goal_id", goal.Id}
            };
            for(int i = 0 ; i < activities.Count ; i++)
            {
                param.Add(String.Format("activity[{0}][skill_level_id]",i),activities[i].Level.Id);
                param.Add(String.Format("activity[{0}][activity_id]",i),activities[i].Activity.Id);
            }
            for(int i = 0 ; i < places.Count ; i++)
            {
                param.Add(String.Format("placesToTrain[{0}][location_id]",i),places[i].LocationId);
            }
            if(goal.Id == "6")
            {
                param.Add("goal_text",goal.Name);
            }
            return await ApiPost<ResponseMessage<string>>(SignUpStep3Uri,param);
        }

        public async Task<ResponseMessage<string>> UnfriendAsync(string id)
        {
            var param = new Dictionary<string,string>
            {
                {"token", _token},
                {"friend", id}
            };
            return await ApiPost<ResponseMessage<string>>(UnfriendUri,param);
        }

        public async Task<ResponseMessage<string>> UpdateProfileAsync(string fName,string lName,
            string gender,string goalId,string goalText,string picture,
            string address,string description,string dob,List<string> activitesID,
            List<string> ids,List<string> skillLevelIds,List<string> placeIds,List<string> locationIds)
        {
            var param = new Dictionary<string,string>
            {
                { "token",_token },
                { "fname", fName },
                { "gender", gender },
                { "goal_id", goalId },
                { "goal_text", goalText},
                { "lname",lName },
                { "picture", picture },
                { "address", address },
                { "description", description },
                { "dob", dob },
                { "edit", "1" }
            };

            for(int i = 0 ; i < activitesID.Count ; i++)
            {
                param.Add(String.Format("activity[{0}][skill_level_id]",i),skillLevelIds[i]);
                param.Add(String.Format("activity[{0}][activity_id]",i),activitesID[i]);
            }
            for(int i = 0 ; i < ids.Count ; i++)
            {
                param.Add(String.Format("activity[{0}][id]",i),ids[i]);
                if(i >= activitesID.Count)
                {
                    param.Add(String.Format("activity[{0}][delete]",i),"1");
                }
            }
            for(int i = 0 ; i < placeIds.Count ; i++)
            {
                param.Add(String.Format("placesToTrain[{0}][id]",i),placeIds[i]);
                if(i >= locationIds.Count)
                {
                    param.Add(String.Format("placesToTrain[{0}][delete]",i),"1");
                }
            }
            for(int i = 0 ; i < locationIds.Count ; i++)
            {
                param.Add(String.Format("placesToTrain[{0}][location_id]",i),locationIds[i]);
            }
            return await ApiPost<ResponseMessage<string>>(UpdateProfileUri,param);
        }

        public async Task<bool> VerifyAsync()
        {
            var param = new Dictionary<string,string>
            {
                {"token", _token}
            };
            var result = await ApiPost<ResponseMessage<string>>(VerifyUri,param);
            return (result != null && result.Output?.Status == 1);

        }
    }
}
