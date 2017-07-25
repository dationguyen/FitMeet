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

        public FitMeetRestService()
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseUri) };
        }

        public async Task<ResponseMessage<List<NewsInfomation>>> GetNewsAsync()
        {
            var param = new Dictionary<string, string>
            {
                { "token", "7r8r2hi14bhfvk31g3zj" }
            };
            return await ApiPost<ResponseMessage<List<NewsInfomation>>>(getNewsUri, param);
        }
    }
}
