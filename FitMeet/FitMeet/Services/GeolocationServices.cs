using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class GeolocationServices : IGeolocationServices
    {
        private Position _position;
        private TimeSpan _timeOut = TimeSpan.FromSeconds(20);
        private DateTime lastUpdateTime;

        public GeolocationServices()
        {
            lastUpdateTime = DateTime.Now;
        }

        public async Task<Position> GetPosition()
        {
            var period = DateTime.Now - lastUpdateTime;           
            
            if (_position == null || period >= _timeOut)
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    _position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
                }
                catch (Exception e)
                {
                    Debug.WriteLine( e.ToString());
                    _position = new Position() { Latitude = -33.870452, Longitude = 151.192191 };
                }
               
            }
            return _position;
        }
    }
}
