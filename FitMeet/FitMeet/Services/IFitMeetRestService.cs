using FitMeet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IFitMeetRestService
    {
        /// <summary>
        /// Get the list of news
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<List<NewsInfomation>>> GetNewsAsync();

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
        Task<ResponseMessage<List<Member>>> SearchMembersAsync(int page, int distance, string gender, List<int> activities);

        /// <summary>
        /// advance search member
        /// </summary>
        /// <returns></returns>
        Task<ResponseMessage<ActivityData>> GetActivityDataAsync();

    }
}
