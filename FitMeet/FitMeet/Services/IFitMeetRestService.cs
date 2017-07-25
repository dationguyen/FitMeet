using FitMeet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IFitMeetRestService
    {
        Task<ResponseMessage<List<NewsInfomation>>> GetNewsAsync();
    }
}
