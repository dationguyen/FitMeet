using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class RestApiClient
    {
        protected HttpClient httpClient;

        public async Task<T> ApiGet<T>(string requestUri)
        {
            T result;
            try
            {
                var response = await httpClient.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                result = default(T);
            }

            return result;
        }

        public async Task<T> ApiGet<T>(string requestUri, Dictionary<string, string> param)
        {
            var newUri = AddQueryString(requestUri, param);
            return await ApiGet<T>(newUri);
        }

        public async Task<T> ApiPost<T>(string requestUri, Dictionary<string, string> param)
        {
            
            if (param == null) param = new Dictionary<string, string>();
            
            var  encodedContent = new FormUrlEncodedContent(param);

            T result;
            try
            {
                var response = await httpClient.PostAsync(requestUri, encodedContent);
                var responseContent = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                result = default(T);
            }

            return result;

        }

        private string AddQueryString(string requestUri, Dictionary<string, string> param)
        {
            string result = requestUri;
            result += "?";
            result += string.Join("&", param.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            return result;
        }
    }
}
