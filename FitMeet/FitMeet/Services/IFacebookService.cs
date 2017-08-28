using FitMeet.Models;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IFacebookService
    {
        Task<FacebookProfile> GetFacebookProfileAsync(string token);
    }
}
