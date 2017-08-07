using FitMeet.Models;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitMeet.Services

{
    public class FitMeetRestService : RestApiClient, IFitMeetRestService
    {
        private const string baseUri = "http://52.64.164.241/";
        private const string getNewsUri = "News/index.json";
        private const string getPageUri = "contentpages/getpage.json";
        private const string getNewsDetailUri = "News/view.json";
        private const string getMemberUri = "users/index/page:{0}.json";
        private const string getFriendUri = "friends/index/page:{0}.json";
        private const string addFriendUri = "Friends/add.json";
        private const string getMemberDetailUri = "/users/view.json";
        private const string searchMemberUri = "Users/advance_search/page:{0}.json";
        private const string getActivityDatarUri = "activities/index.json";

        private readonly IPageDialogService _dialogService;
        private readonly IGeolocationServices _geoService;

        public FitMeetRestService(IPageDialogService dialogService, IGeolocationServices geoService)
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUri) };
            _dialogService = dialogService;
            _geoService = geoService;
        }

        public async Task<bool> AddFriendsAsync(int friendId)
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "friend", friendId.ToString() }
            };
            var result = await ApiPost<ResponseMessage<string>>(getActivityDatarUri, param);
            return result?.Output?.Status == 1;
        }

        public async Task<ResponseMessage<ActivityData>> GetActivityDataAsync()
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" }
            };
            return await ApiPost<ResponseMessage<ActivityData>>(getActivityDatarUri, param);

        }

        public async Task<ResponseMessage<List<Member>>> GetFriendsAsync(int page)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(getFriendUri, page), param);
        }

        public async Task<ResponseMessage<MemberDetail>> GetMemberDetailAsync(string id)
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "id", id }
            };
            return await ApiPost<ResponseMessage<MemberDetail>>(getMemberDetailUri, param);
        }

        public async Task<ResponseMessage<List<Member>>> GetMembersAsync(int pageId)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() }
            };
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(getMemberUri, pageId), param);
        }

        /// <summary>
        /// Get the list of news
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseMessage<List<NewsInfomation>>> GetNewsAsync()
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" }
            };
            return await ApiPost<ResponseMessage<List<NewsInfomation>>>(getNewsUri, param);
        }

        public async Task<ResponseMessage<NewsDetail>> GetNewsDetailAsync(string id)
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "id", id }
            };
            return await ApiPost<ResponseMessage<NewsDetail>>(getNewsDetailUri, param);
        }

        public async Task<ResponseMessage<WebPageInfo>> GetPageDetailAsync(string pageName)
        {
            var param = new Dictionary<string, string>
            {
                { "slug", pageName }
            };
            return await ApiPost<ResponseMessage<WebPageInfo>>(getPageUri, param);
        }

        public async Task<ResponseMessage<List<Member>>> SearchMembersAsync(int page, int distance, string gender, List<int> activities)
        {
            var position = await _geoService.GetPosition();

            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "lat", position?.Latitude.ToString() },
                { "lng", position?.Longitude.ToString() },
                { "gender", gender },
                { "distance", distance.ToString()}
            };
            for (int i = 0; i < activities.Count; i++)
            {
                param.Add(String.Format("activity[{0}]", i), activities[i].ToString());
            }
            return await ApiPost<ResponseMessage<List<Member>>>(String.Format(searchMemberUri, page), param);

        }
    }
}
