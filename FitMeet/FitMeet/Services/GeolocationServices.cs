﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

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
                var locator = CrossGeolocator.Current;
                _position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            }

            return _position;
        }
    }
}