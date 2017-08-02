using FitMeet.Models;
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
        private const string searchMemberUri = "Users/advance_search/page:{0}.json";
        private const string getActivityDatarUri = "activities/index.json";

        public FitMeetRestService()
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUri) };
        }

        public async Task<ResponseMessage<ActivityData>> GetActivityDataAsync()
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" }
            };
            return await ApiPost<ResponseMessage<ActivityData>>(getActivityDatarUri, param);

        }

        public async Task<ResponseMessage<List<Member>>> GetMembersAsync(int pageId)
        {
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "lat", "-33.8704391" },
                { "lng", "151.1921796" }
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
            var param = new Dictionary<string, string>
            {
                { "token", "4fmr0pw0kee6h3kccbli" },
                { "lat", "-33.8704391" },
                { "lng", "151.1921796" },
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
