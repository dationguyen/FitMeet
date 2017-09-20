using FitMeet.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class ImageUploadHelper
    {
        private static string baseUrl = "https://api.cloudinary.com/v1_1/djnq5vsmp/image/upload";
        public static async Task<string> UploadImage(byte[] data)
        {
            using(var httpClient = new HttpClient())
            {
                //var param = new Dictionary<string,string>
                //{
                //    //{ "file",Convert.ToBase64String(file) },
                //    { "upload_preset","gcxogvcl"}
                //};

                ByteArrayContent fileContent = new ByteArrayContent(data);
                StringContent stringContent = new StringContent("gcxogvcl");

                stringContent.Headers.Add("Content-Disposition","form-data; name=\"upload_preset\"");

                //fileContent.Headers.Add("Content-Disposition","form-data; name=\"file\"");
                //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                using(MultipartFormDataContent multipartContent = new MultipartFormDataContent())
                {
                    multipartContent.Add(fileContent,"file","upload.jpg");
                    multipartContent.Add(stringContent);

                    var result = await ApiPost<CloudaryResponseModel>(httpClient,baseUrl,multipartContent);
                    return result?.url;
                }

            }

        }

        private static async Task<T> ApiPost<T>(HttpClient httpClient,string requestUri,HttpContent parram)
        {

            //if(param == null)
            //    param = new Dictionary<string,string>();

            //var encodedContent = new FormUrlEncodedContent(param);

            T result;
            try
            {
                var response = await httpClient.PostAsync(requestUri,parram);
                var responseContent = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                result = default(T);
            }

            return result;

        }
    }
}
