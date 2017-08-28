using FitMeet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class GoogleLocationService:RestApiClient, IGoogleLocationService
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1,1);
        private int _waitingTask = 0;

        private const string baseAdress = "https://maps.googleapis.com/maps/api/place/";
        private const string autoCompleteUri = "autocomplete/json";
        private const string apiKey = "AIzaSyBOuhc3yhf5fzRPfPBcvvZmKWfO5p3_Vok";



        public GoogleLocationService()
        {
            this.httpClient = new HttpClient() { BaseAddress = new Uri(baseAdress) };
        }

        public async Task<List<string>> AutoComplete(string location)
        {
            _waitingTask++;
            await _semaphoreSlim.WaitAsync();
            var result = new List<string>();

            try
            {
                if(_waitingTask <= 1)
                {
                    var param = new Dictionary<string,string>
                    {
                        { "key",apiKey },
                        { "language","en-AU" },
                        { "input",location }
                    };

                    var response = await ApiGet<GoogleMapResponse>(autoCompleteUri,param);
                    if(response != null && response.status == "OK")
                    {
                        var d = from res in response.predictions select res.description;
                        result = d.Take(5).ToList();
                    }
                }
                else
                {
                    Debug.WriteLine("cancel" + _waitingTask);

                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                _waitingTask--;
                _semaphoreSlim.Release();
            }
            return result;
        }
    }
}
