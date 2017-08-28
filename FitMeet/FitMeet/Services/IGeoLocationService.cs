using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IGeoLocationService
    {
        /// <summary>
        /// get current position
        /// </summary>
        /// <returns></returns>
        Task<Position> GetPosition();
    }
}
