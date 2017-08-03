using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IGeolocationServices
    {
        /// <summary>
        /// get current position
        /// </summary>
        /// <returns></returns>
        Task<Position> GetPosition();
    }
}
